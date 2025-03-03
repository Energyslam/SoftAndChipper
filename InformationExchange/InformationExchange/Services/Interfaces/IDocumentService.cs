namespace InformationExchange.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<bool> UploadDocuments(int patientId, List<IFormFile> documents);
        Task<bool> RequestDocument(int patientId, string request);
    }
}
