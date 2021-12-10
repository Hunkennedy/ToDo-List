namespace Api.Models
{
    public class Todotask
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool Check { get; set; }
        public int FolderId { get; set; }
    }
}
