namespace InformationExchange.Services.Interfaces
{
    public interface IDocumentService
    {
        Task UploadDocuments(int patientId, List<IFormFile> documents, int? requestId);
        Task RequestDocument(int patientId, string request);
    }
}
