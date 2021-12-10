using Api.DTO;

namespace Api.Interfaces
{
    public interface ITodoTask
    {
        public Task<List<TodotaskDto>> Get();
        public Task<TodotaskDto?> Get(int id);
        public Task<TodotaskDto> Post(CreateTodotaskDto taskDto);
        public Task<TodotaskDto?> Update(int id, CreateTodotaskDto taskDto);
        public Task<TodotaskDto?> Delete(int id);

    }
}
