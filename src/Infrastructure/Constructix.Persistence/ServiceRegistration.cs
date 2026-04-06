namespace Constructix.Persistence;
public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Öncə Interceptor-u qeydiyyatdan keçiririk
        services.AddScoped<AuditInterceptor>();

        // 2. DbContext konfiqurasiyası
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<AuditInterceptor>();

            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                   .AddInterceptors(interceptor);
        });

        // Gələcəkdə Repository-ləri də bura əlavə edəcəyik:
        // services.AddScoped<IBuildingRepository, BuildingRepository>();

        return services;
    }
}