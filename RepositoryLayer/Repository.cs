using RepositoryLayer.Models;

namespace RepositoryLayer;

public class Repository(AppDbContext context)
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
}
