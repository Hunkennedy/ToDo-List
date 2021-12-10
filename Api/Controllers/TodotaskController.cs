using Api.Data;
using Api.DTO;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodotaskController: Controller
    {
        private readonly ITodoTask _todotask;
        public TodotaskController(ITodoTask todotask)
        {
            _todotask = todotask;
        }

        [HttpGet(Name = "GetTaskTodo")]
        public async Task<ActionResult<List<TodotaskDto>>> Get()
        {
            var tasks = await _todotask.Get();
            return Ok(tasks);
        }

        [HttpPost(Name = "PostTaskTodo")]
        public async Task<ActionResult> Post(CreateTodotaskDto task)
        {
            await _todotask.Post(task);
            return Ok();
        }

        [HttpGet("{id}", Name = "GetOneTask")]
        public async Task<ActionResult<TodotaskDto>> GetTask(int id)
        {
            var myTask = await _todotask.Get(id);
            if (myTask == null)
                return NotFound();
            return Ok(myTask);
        }

        [HttpPut(Name = "UpdateTask")]
        public async Task<ActionResult> UpdateTask(int id, TodotaskDto task)
        {
            var _task = await _todotask.Update(id, task);
            if (_task == null)
                return NotFound();
            return Ok(_task);
        }

        [HttpDelete(Name = "DeleteTask")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var myTask = await _todotask.Delete(id);
            if (myTask == null)
                return NotFound();
            return Ok();
        }

    }
}
