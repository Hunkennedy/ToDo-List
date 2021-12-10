namespace Api.Models
{
    public class Todotask
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int FolderId { get; set; }
    }
}
