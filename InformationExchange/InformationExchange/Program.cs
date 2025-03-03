using InformationExchange.Services;
using InformationExchange.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InformationExchange
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<InformationExchangeDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));

            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Version one");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
