using Api.Data;
using Api.DTO;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class TodotaskRepository: ITodoTask
    {

        private readonly ApplicationDbContext _context;
        public TodotaskRepository(ApplicationDbContext context){ _context = context; }

        public async Task<List<TodotaskDto>> Get()
        {
            var todo = await _context.Todotasks.ToListAsync();
            var dto = todo.Select(x => new TodotaskDto() 
            { 
                Id = x.Id, 
                Title = x.Title, 
                FolderId = x.FolderId, 
                Check = x.Check 
            }).ToList();
            return dto;
        }

        public async Task<TodotaskDto?> Get(int id)
        {
            var todo = await _context.Todotasks.FirstOrDefaultAsync(x => x.Id == id);
            if(todo == null) return null;
            var dto = new TodotaskDto()
            {
                Id = todo.Id,
                Title = todo.Title,
                FolderId = todo.FolderId,
                Check = todo.Check
            };
            return dto;
        }

        public async Task<TodotaskDto> Post(CreateTodotaskDto taskDto)
        {
            var task = new Todotask()
            {
                Title = taskDto.Title,
                FolderId=taskDto.FolderId,
                Check = false
            };

            await _context.Todotasks.AddAsync(task);
            await _context.SaveChangesAsync();
            var dto = new TodotaskDto()
            {
                Id = task.Id,
                Title = task.Title,
                Check = task.Check,
                FolderId = task.FolderId
            };
            return dto;
        }

        public async Task<TodotaskDto?> Update(int id, CreateTodotaskDto taskDto)
        {
            var myTask = await _context.Todotasks.FirstOrDefaultAsync(x => x.Id == id);
            if (myTask == null) return null;
            myTask.Title = taskDto.Title;
            myTask.Check = taskDto.Check;
            _context.Entry(myTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var dto = new TodotaskDto
            {
                Id = myTask.Id,
                Title= myTask.Title,
                FolderId = myTask.FolderId,
                Check = myTask.Check
            };
            return dto;
        }

        public async Task<TodotaskDto?> Delete(int id)
        {
            var myTask = await _context.Todotasks.FirstOrDefaultAsync(x => x.Id == id);
            if (myTask == null) return null;
            _context.Entry(myTask).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            var dto = new TodotaskDto();
            return dto;
        }
    }
}
