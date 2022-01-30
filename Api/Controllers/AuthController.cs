using Api.Configuration;
using Api.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        ITokenHandlerService service;

        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService service)
        {
            this.userManager = userManager;
            this.service = service;
        }

        [HttpPost]
        [Route("register", Name = "register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager
                    .FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest("Usuario ya existente");
                }
                var userCreated = await userManager
                    .CreateAsync(new IdentityUser() 
                { 
                    UserName = user.UserName, Email = user.Email 
                },
                user.Password);   
                if (userCreated.Succeeded)
                {
                    return Ok(userCreated);
                }

                return BadRequest(userCreated
                    .Errors
                    .Select(x => x.Description));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto login)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(login.Email);
                if (existingUser == null)
                {
                    return BadRequest(new UserLoginResponseDto()
                    {
                        Login = false,
                        Error = new()
                        {
                            "Usuario o contrasena incorrectos"
                        }
                    });
                }

                var isCorrect = await userManager.CheckPasswordAsync(existingUser, login.Password);
                if (isCorrect)
                {
                    var pars = new TokenParameters()
                    {
                        Id = existingUser.Id,
                        PasswordHash = existingUser.PasswordHash,
                        UserName = existingUser.UserName
                    };
                    var jwtToken = service.GenerateJwtToken(pars);

                    return Ok(new UserLoginResponseDto()
                    {
                        Login = true,
                        Token = jwtToken
                    });
                }

                return BadRequest(new UserLoginResponseDto()
                {
                    Login = false,
                    Error = new()
                    {
                        "Usuario o contrasena incorrectos"
                    }
                });
            }

            return BadRequest(new UserLoginResponseDto()
            {
                Login = false,
                Error = new()
                {
                    "Usuario o contrasena incorrectos"
                }
            });
        }

    }
}
