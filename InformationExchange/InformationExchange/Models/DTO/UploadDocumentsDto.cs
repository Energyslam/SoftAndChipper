using System.ComponentModel.DataAnnotations;

namespace InformationExchange.Models.DTO
{
    public class UploadDocumentsDto
    {
        [Required]
        int PatientId { get; set; }
        [Required]
        List<IFormFile> Documents { get; set; }
    }
}
