using Mapster;
using MapsterMapper;

namespace TaskManagement.Common;

public static class MapsterConfig
{
    public static void AddMapster(this IServiceCollection services)
    {
        // register mapster with Dependency Injection Container [services]
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();

    }
}
