using System.Reflection;

using Constructix.Application.Behaviors;

using Microsoft.Extensions.DependencyInjection;

namespace Constructix.Application;
public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // 1. MediatR-ı qeydiyyatdan keçiririk
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assembly);

            // 2. ValidationBehavior-ı əlavə edirik (Bu çox vacibdir!)
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        // 3. Bütün Validator-ları (məs. CreateBuildingCommandValidator) avtomatik tap və qeyd et
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
