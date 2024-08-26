using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Models;

namespace RepositoryLayer;

public interface IRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task<UserRole> GetUserRoleAsync(int userId);
   // Task<List<Request>> GetRequestsByUserIdAsync(int userId);
}

public class Repository(AppDbContext context): IRepository
{
    private readonly AppDbContext _context = context;
    protected AppDbContext Context => _context;

    public async Task<bool> ExistsAsync<T>(object id) where T : class
    {
        return await FindOneByIdAsync<T>(id) != null;
    }

    public async Task<T> FindOneByIdAsync<T>(object id) where T : class
    {
        var found = await _context.FindAsync<T>([id]);
        return found;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await Context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserRole> GetUserRoleAsync(int userId)
    {
        // Assuming _context is your DbContext and UsersRoles is your DbSet<UsersRole>
        var userRole = await _context.UsersRoles
            .Include(ur => ur.IdRoleNavigation) // Include the related UserRole entity
            .FirstOrDefaultAsync(ur => ur.IdUser == userId); // Find the first UsersRole entry for the given userId

        return userRole?.IdRoleNavigation; // Return the related UserRole, or null if not found
    }

  /*  public async Task<List<Request>> GetRequestsByUserIdAsync(int userId)
    {
        return await _context.Requests
            .Include(r => r.RequestStatusNavigation)
            .Include(r => r.RequestTypeNavigation)
            .Where(r => r.IdUser == userId)
            .ToListAsync();
    } */

}
