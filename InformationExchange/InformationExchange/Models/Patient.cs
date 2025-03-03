namespace InformationExchange.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public List<Document> Documents { get; set; } = [];
        public List<DocumentRequest> DocumentRequests { get; set; } = [];
    }
}
