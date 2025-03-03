namespace InformationExchange.Models
{
    public class Document
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime DateCreated { get; set; }
        public DocumentRequest? DocumentRequest { get; set; }
    }
}
