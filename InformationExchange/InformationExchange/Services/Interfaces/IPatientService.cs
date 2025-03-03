using InformationExchange.Models;

namespace InformationExchange.Services.Interfaces
{
    public interface IPatientService
    {
        Task<Patient?> GetPatient(int patientId);
    }
}
