namespace Documentation.API.Entities
{
    public class Document
    {
        public required int DocumentId { get; set; }
        public required string Title { get; set; }
        public required byte[] Content { get; set; }
    }
}
