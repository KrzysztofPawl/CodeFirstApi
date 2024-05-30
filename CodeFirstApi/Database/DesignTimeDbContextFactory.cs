using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFrameworkApi.Database;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PharmacyContext>
{
    public PharmacyContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<PharmacyContext>();
        var connectionString = configuration.GetConnectionString("PharmacyDB");

        optionsBuilder.UseSqlServer(connectionString);

        return new PharmacyContext(optionsBuilder.Options);
    }
}