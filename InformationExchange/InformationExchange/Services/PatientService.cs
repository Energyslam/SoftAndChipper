using InformationExchange.Models;
using InformationExchange.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InformationExchange.Services
{
    public class PatientService : IPatientService
    {
        private readonly InformationExchangeDbContext _dbContext;
        private readonly ILogger<PatientService> _logger;

        public PatientService(InformationExchangeDbContext dbContext, ILogger<PatientService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<Patient?> GetPatient(int patientId)
        {
            try
            {
                Patient? patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == patientId);
                if (patient == null)
                {
                    patient = await CreatePatient();
                }
                return patient;
            }
            catch(Exception)
            {
                _logger.LogError("Error while finding patient ({PatientId})", patientId);
                return null;
            }
        }

        private async Task<Patient> CreatePatient()
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    Patient patient = new();
                    _dbContext.Patients.Add(patient);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return patient;
                }
                catch(Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
