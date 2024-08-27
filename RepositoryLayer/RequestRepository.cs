using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Models;

namespace RepositoryLayer
{
    public interface IRequestRepository
    {
        Task<List<Request>> GetAllRequestsAsync();
        Task<List<Request>> GetAllUnassignedRequestsAsync();
        Task<List<RequestType>> GetRequestTypesAsync();
        Task<List<RequestStatus>> GetRequestStatusesAsync();
        Task<List<Request>> GetRequestsByUserIdAsync(int userId);
        Task<bool> CreateRequestAsync(Request request);

    }

    public class RequestRepository(AppDbContext context) : Repository(context), IRequestRepository
    {
        public async Task<List<Request>> GetAllRequestsAsync()
        {
            return await Context.Requests.ToListAsync();
        }

        public async Task<List<Request>> GetAllUnassignedRequestsAsync()
        {
            return await Context.Requests.Where(r => r.IdAdmin == null).ToListAsync();
        }

        public async Task<List<RequestType>> GetRequestTypesAsync()
        {
            return await Context.RequestTypes.ToListAsync();
        }

        public async Task<List<RequestStatus>> GetRequestStatusesAsync()
        {
            return await Context.RequestStatus.ToListAsync();
        }

        public async Task<List<Request>> GetRequestsByUserIdAsync(int userId)
        {
            return await Context.Requests
                .Include(r => r.RequestStatusNavigation)
                .Include(r => r.RequestTypeNavigation)
                .Where(r => r.IdUser == userId)
                .ToListAsync();
        }

        public async Task<bool> CreateRequestAsync(Request request)
        {
            Context.Requests.Add(request);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
