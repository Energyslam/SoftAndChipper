using InformationExchange.Models;

namespace InformationExchange.Services.Interfaces
{
    public interface IPatientService
    {
        Patient? GetPatient(int patientId);
    }
}
