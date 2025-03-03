using System.ComponentModel.DataAnnotations;

namespace InformationExchange.Models.DTO
{
    public class RequestDocumentsDto
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public string Request { get; set; }
    }
}
