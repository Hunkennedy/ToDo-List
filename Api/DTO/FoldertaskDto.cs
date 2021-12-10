namespace Api.DTO
{
    public class FoldertaskDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<TodotaskDto?>? Todotasks { get; set; }

    }
}
