using InformationExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationExchange
{
    public class InformationExchangeDbContext(DbContextOptions<InformationExchangeDbContext> contextOptions) : DbContext(contextOptions)
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentRequest> DocumentRequests { get; set; }
        public DbSet<Patient> Patients { get; set; }
    }
}
