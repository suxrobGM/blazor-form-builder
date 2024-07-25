using FormBuilder.Services;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace FormBuilder;

public static class Registrar
{
    public static IServiceCollection AddFormBuilder(this IServiceCollection services, Action<FormBuilderOptions> configure)
    {
        var options = new FormBuilderOptions();
        configure(options);
        return AddFormBuilder(services, options);
    }
    
    public static IServiceCollection AddFormBuilder(this IServiceCollection services, FormBuilderOptions options)
    {
        services.AddSingleton(options);
        services.AddSingleton<DragDropService>();
        services.AddScoped<FormService>();
        services.AddRadzenComponents();
        services.AddMemoryCache();
        return services;
    }
}
