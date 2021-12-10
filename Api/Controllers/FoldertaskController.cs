using Api.DTO;
using Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldertaskController : ControllerBase
    {
        private readonly IFoldertask _foldertask;
        public FoldertaskController(IFoldertask foldertask)
        {
            _foldertask = foldertask;
        }

        [HttpGet(Name = "Get Folders")]
        public async Task<ActionResult<List<FoldertaskDto>>> Get()
        {
            var folders = await _foldertask.Get();
            return Ok(folders);
        }
        [HttpGet("{id}", Name = "Get Folder")]
        public async Task<ActionResult<FoldertaskDto>> GetOne(int id)
        {
            var folders = await _foldertask.Get(id);
            return Ok(folders);
        }

        [HttpPost(Name = "Create Folder")]
        public async Task<ActionResult> Post(CreateFoldertaskDto foldertaskDto)
        {
            var folder = await _foldertask.Post(foldertaskDto);
            return Ok(folder);
        }

        [HttpDelete(Name = "Delete Folder")]
        public async Task<ActionResult> Delete(int id)
        {
            var folder = await _foldertask.Delete(id);
            if (folder == null) return NotFound();
            return Ok(folder);

        }

        [HttpPut(Name = "Update Folder")]
        public async Task<ActionResult> Update(int id, CreateFoldertaskDto foldertaskDto)
        {
            var folder = await _foldertask.Update(id, foldertaskDto);
            if (folder == null) return NotFound();
            return Ok(folder);
        }
    }
}
