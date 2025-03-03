using InformationExchange.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InformationExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    private readonly ILogger<DocumentController> _logger;

    public DocumentController(ILogger<DocumentController> logger)
    {
        _logger = logger;
    }

    [HttpPost("UploadDocuments")]
    public async Task<IActionResult> UploadDocuments(UploadDocumentsDto dto)
    {
        return Ok();
    }

    [HttpPost("RequestDocuments")]
    public async Task<IActionResult> RequestDocuments(RequestDocumentsDto dto)
    {
        return Ok();
    }
}
