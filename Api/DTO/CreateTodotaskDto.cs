namespace Api.DTO
{
    public class CreateTodotaskDto
    {
        public string? Title { get; set; }
        public int FolderId { get; set; }
        public int UserId { get; set; }
    }
}
