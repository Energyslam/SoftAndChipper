namespace InformationExchange.Models
{
    public class DocumentRequest
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Request { get; set; }
        public List<Document> Documents { get; set; } = [];
    }
}
