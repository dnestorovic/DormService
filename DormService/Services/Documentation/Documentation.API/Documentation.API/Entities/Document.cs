namespace Documentation.API.Entities
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public byte[] Content { get; set; }
    }
}
