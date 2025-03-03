using InformationExchange.Exceptions;
using InformationExchange.Models;
using InformationExchange.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InformationExchange.Services
{
    public class DocumentService : IDocumentService
    {
        private const string UploadFolderName = "Uploads";

        private readonly ILogger<DocumentService> _logger;
        private readonly InformationExchangeDbContext _dbContext;
        private readonly IPatientService _patientService;
        private readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), UploadFolderName);

        public DocumentService(ILogger<DocumentService> logger, InformationExchangeDbContext dbContext, IPatientService patientService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _patientService = patientService;

            Directory.CreateDirectory(_uploadDirectory);
        }
        public async Task RequestDocument(int patientId, string request)
        {
            Patient? patient = await _patientService.GetPatient(patientId);
            if (patient == null)
            {
                throw new EntityNotFoundException();
            }
            patientId = patient.Id;

            DocumentRequest documentRequest = new()
            {
                PatientId = patientId,
                Request = request
            };

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _dbContext.DocumentRequests.Add(documentRequest);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError("{Message}", ex.Message);
                }
            }
        }

        public async Task UploadDocuments(int patientId, List<IFormFile> files, int? requestId)
        {
            Patient? patient = await _patientService.GetPatient(patientId);
            if (patient == null)
            {
                throw new EntityNotFoundException();
            }
            patientId = patient.Id;

            string patientDirectory = Path.Combine(_uploadDirectory, $"{patientId}");
            Directory.CreateDirectory(patientDirectory);

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                List<string> createdFilePaths = [];
                try
                {
                    foreach (IFormFile file in files)
                    {
                        string filePath = Path.Combine(_uploadDirectory, $"{patientId}", file.FileName);
                        createdFilePaths.Add(filePath);
                        await UploadDocument(patientId, file, filePath, requestId);
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to upload document ({Message}).", ex.Message);
                    await transaction.RollbackAsync();
                    foreach(string filePath in createdFilePaths)
                    {
                        File.Delete(filePath);
                    }
                    throw;
                }
            }
        }

        private async Task<string> UploadDocument(int patientId, IFormFile file, string filePath, int? requestId)
        {

            using (FileStream stream = new(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Document document = new()
            {
                PatientId = patientId,
                FilePath = filePath,
                FileName = file.FileName,
                DateCreated = DateTime.UtcNow
            };

            _dbContext.Documents.Add(document);
            await _dbContext.SaveChangesAsync();
            await LinkDocumentToRequest(patientId, requestId, document);

            return filePath;
        }

        private async Task LinkDocumentToRequest(int patientId, int? requestId, Document document)
        {
            if (requestId == null)
            {
                return;
            }

            DocumentRequest documentRequest = await _dbContext.DocumentRequests.FirstOrDefaultAsync(x => x.Id == requestId) ?? throw new EntityNotFoundException();
            documentRequest.Documents.Add(document);
            await _dbContext.SaveChangesAsync();
        }
    }
}
