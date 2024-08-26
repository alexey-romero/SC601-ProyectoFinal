using RepositoryLayer;
using SecurityLayer;
using ServiceLayer;

namespace TicketingSystem.Architecture;

internal static class ServicesConfiguration
{
    internal static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ISecurityService, SecurityService>();
        serviceCollection.AddScoped<IRepositoryService, RepositoryService>();
        serviceCollection.AddScoped<IRequestService, RequestService>();
    }
}
