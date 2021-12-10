using Api.Data;
using Api.DTO;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Repositories
{
    public class FoldertaskRepository : IFoldertask
    {
        private readonly ApplicationDbContext _context;
        public FoldertaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FoldertaskDto>> Get()
        {
            var fold = await _context.Foldertasks.ToListAsync();
            var tasks = await _context.Todotasks.ToListAsync();
            var tasksDto = tasks.Select(task => new TodotaskDto 
            { 
                Id = task.Id, 
                Check = task.Check, 
                Title = task.Title
            }).ToList();
            var dto = fold.Select(x => new FoldertaskDto
            {
                Id = x.Id,
                Name = x.Name,
                Todotasks = from n in tasks 
                            where n.FolderId == x.Id 
                            select new TodotaskDto 
                            { 
                                Id = n.Id, 
                                Title = n.Title, 
                                Check = n.Check 
                            }
            })
            .ToList();
            
            return dto;
        }

        public async Task<FoldertaskDto?> Get(int id)
        {
            var fold = await _context.Foldertasks.FirstOrDefaultAsync(x => x.Id == id);
            if (fold == null) return null;
            var tasks = await _context.Todotasks.ToListAsync();
            var tasksDto = tasks.Select(task => new TodotaskDto
            {
                Id = task.Id,
                Check = task.Check,
                Title = task.Title
            })
            .ToList();
            var dto = new FoldertaskDto
            {
                Id = fold.Id,
                Name = fold.Name,
                Todotasks = from n in tasks
                            where n.FolderId == fold.Id
                            select new TodotaskDto { Id = n.Id, Title = n.Title, Check = n.Check }
            };
            return dto;
        }

        public async Task<FoldertaskDto> Post(CreateFoldertaskDto taskDto)
        {
            var fold = new Foldertask
            {
                Name = taskDto.Name
            };
            await _context.Foldertasks.AddAsync(fold);
            await _context.SaveChangesAsync();
            var dto = new FoldertaskDto
            {
                Id = fold.Id,
                Name = taskDto.Name
            };
            return dto;
        }

        public async Task<FoldertaskDto?> Update(int id, CreateFoldertaskDto folderDto)
        {
            var folder = await _context.Foldertasks.FirstOrDefaultAsync(x => x.Id == id);
            if (folder == null) return null;
            folder.Name = folderDto.Name;
            _context.Entry(folder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new FoldertaskDto { Name = folderDto.Name };
        }
        public async Task<FoldertaskDto?> Delete(int id)
        {
            var tasks = await _context.Todotasks.ToListAsync();
            var tasksDeleted = tasks.Where(x => id == x.FolderId).ToList();
            foreach (var item in tasksDeleted)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            var folder = await _context.Foldertasks.FirstOrDefaultAsync(x => x.Id == id);
            if (folder == null) return null;
            _context.Entry(folder).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            var dto = new FoldertaskDto();
            return dto;
        }
    }
}
