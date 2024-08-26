using SecurityLayer;

namespace TicketingSystem.Architecture;

internal static class ServicesConfiguration
{
    internal static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ISecurityService, SecurityService>();
    }
}
