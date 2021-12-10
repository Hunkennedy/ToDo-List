namespace Api.DTO
{
    public class CreateTodotaskDto
    {
        public string? Title { get; set; }
        public int FolderId { get; set; } = 0;
        public bool Check { get; set; }

    }
}
