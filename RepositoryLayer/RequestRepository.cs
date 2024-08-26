using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Models;

namespace RepositoryLayer
{
    public interface IRequestRepository
    {
        Task<List<RequestType>> GetRequestTypesAsync();
        Task<List<RequestStatus>> GetRequestStatusesAsync();
    }

    public class RequestRepository(AppDbContext context) : Repository(context), IRequestRepository
    {
        public async Task<List<RequestType>> GetRequestTypesAsync()
        {
            return await Context.RequestTypes.ToListAsync();
        }

        public async Task<List<RequestStatus>> GetRequestStatusesAsync()
        {
            return await Context.RequestStatus.ToListAsync();
        }
    }
}
