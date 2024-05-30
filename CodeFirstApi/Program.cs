using EntityFrameworkApi.Database;
using EntityFrameworkApi.Interfaces;
using EntityFrameworkApi.Repositories;
using EntityFrameworkApi.Services;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<PharmacyContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PharmacyDB")));

        builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers().AddXmlSerializerFormatters();
        
        var app = builder.Build();

        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}