using InformationExchange.Services.Interfaces;

namespace InformationExchange.Services
{
    public class DocumentService : IDocumentService
    {
        public Task<bool> RequestDocument(int patientId, string request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UploadDocuments(int patientId, List<IFormFile> documents)
        {
            throw new NotImplementedException();
        }
    }
}
