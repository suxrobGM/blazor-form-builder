using FormBuilder.Services;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace FormBuilder;

public static class Registrar
{
    public static IServiceCollection AddFormBuilder(this IServiceCollection services)
    {
        services.AddRadzenComponents();
        services.AddSingleton<DragDropService>();
        return services;
    }
}
