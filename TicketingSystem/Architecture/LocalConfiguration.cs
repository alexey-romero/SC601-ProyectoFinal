using TicketingSystem.Filters;

namespace TicketingSystem.Architecture;

internal static class LocalConfiguration
{
    internal static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<CustomAuthorizationFilter>();
    }
}
