using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace FormBuilder;

public static class Registrar
{
    public static IServiceCollection AddFormBuilder(this IServiceCollection services)
    {
        services.AddScoped<FormBuilderComponents.ExampleJsInterop>();
        services.AddRadzenComponents();
        return services;
    }
}
