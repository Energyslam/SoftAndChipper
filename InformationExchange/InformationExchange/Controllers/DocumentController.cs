using InformationExchange.Exceptions;
using InformationExchange.Models.DTO;
using InformationExchange.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InformationExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    private readonly ILogger<DocumentController> _logger;
    private readonly IDocumentService _documentService;

    public DocumentController(ILogger<DocumentController> logger, IDocumentService documentService)
    {
        _logger = logger;
        _documentService = documentService;
    }

    [HttpPost("UploadDocuments")]
    public async Task<IActionResult> UploadDocuments(UploadDocumentsDto dto)
    {
        if (dto.PatientId == 0)
        {
            return BadRequest("Invalid patient ID.");
        }
        if (dto.Documents.Count == 0)
        {
            return BadRequest("No documents uploaded.");
        }

        try
        {
            await _documentService.UploadDocuments(dto.PatientId, dto.Documents, dto.RequestId);
        }
        catch(EntityNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        _logger.LogInformation("Created {DocumentCount} documents for patient {PatientId}.", dto.Documents.Count, dto.PatientId);
        return Ok();
    }

    [HttpPost("RequestDocuments")]
    public async Task<IActionResult> RequestDocuments(RequestDocumentsDto dto)
    {
        if (dto.PatientId == 0)
        {
            return BadRequest("Invalid patient ID.");
        }
        if (string.IsNullOrWhiteSpace(dto.Request))
        {
            return BadRequest("No request attached");
        }

        try
        {
            await _documentService.RequestDocument(dto.PatientId, dto.Request);
        }
        catch (EntityNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        _logger.LogInformation("Requested {request} for patient {PatientId}.", dto.Request, dto.PatientId);
        return Ok();
    }
}
