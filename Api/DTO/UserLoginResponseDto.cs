namespace Api.DTO
{
    public class UserLoginResponseDto
    {
        public string? Token { get; set; }
        public bool Login { get; set; }
        public List<string>? Error { get; set; }

    }
}
