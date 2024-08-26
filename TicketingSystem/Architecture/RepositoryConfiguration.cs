using RepositoryLayer;
using RepositoryLayer.Models;
using TicketingSystem.Filters;
using Microsoft.EntityFrameworkCore;

namespace TicketingSystem.Architecture;

internal static class RepositoryConfiguration
{
    internal static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ISecurityRepository, SecurityRepository>();
        serviceCollection.AddScoped<IRepository, Repository>();
        serviceCollection.AddDbContext<AppDbContext>(options
            => options.UseSqlServer("Server=localhost;Database=TicketingSystem;Trusted_Connection=True;TrustServerCertificate=True;"));
    }
}
