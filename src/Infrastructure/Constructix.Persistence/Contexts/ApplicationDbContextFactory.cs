namespace Constructix.Persistence.Contexts;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // 1. Terminalın harada açılmasından asılı olmayaraq WebAPI qovluğunu tapırıq
        string currentDirectory = Directory.GetCurrentDirectory();

        // Əgər terminal ana (root) qovluqdadırsa:
        string basePath = Path.GetFullPath(Path.Combine(currentDirectory, "src/Presentation/Constructix.WebAPI"));

        // Əgər terminal Persistence qovluğundadırsa (ehtiyat variant):
        if (!Directory.Exists(basePath))
        {
            basePath = Path.GetFullPath(Path.Combine(currentDirectory, "../../Presentation/Constructix.WebAPI"));
        }

        // 2. Configuration-ı qururuq
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<ApplicationDbContextFactory>()
            .AddEnvironmentVariables()
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception($"Connection string tapılmadı! Axtarılan qovluq: {basePath}");
        }

        builder.UseNpgsql(connectionString);

        return new ApplicationDbContext(builder.Options);
    }
}