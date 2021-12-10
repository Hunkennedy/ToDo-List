using Api.DTO;

namespace Api.Interfaces
{
    public interface IFoldertask
    {
        public Task<List<FoldertaskDto>> Get();
        public Task<FoldertaskDto?> Get(int id);
        public Task<FoldertaskDto> Post(CreateFoldertaskDto taskDto);
        public Task<FoldertaskDto?> Update(int id, CreateFoldertaskDto taskDto);
        public Task<FoldertaskDto?> Delete(int id);
    }
}
