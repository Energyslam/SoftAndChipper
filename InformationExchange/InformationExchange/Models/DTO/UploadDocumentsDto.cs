using System.ComponentModel.DataAnnotations;

namespace InformationExchange.Models.DTO
{
    public class UploadDocumentsDto
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public List<IFormFile> Documents { get; set; }
        public int? RequestId { get; set; }
    }
}
