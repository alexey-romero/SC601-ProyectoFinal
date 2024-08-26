using RepositoryLayer;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IRequestService
    {
        Task<List<Request>> GetAllRequests();
        Task<List<Request>> GetAllUnassignedRequests();
        Task<List<RequestType>> GetRequestTypes();
        Task<List<RequestStatus>> GetRequestStatuses();
        Task<List<Request>> GetRequestsByUserId(int userId);
    }

    public class RequestService(IRequestRepository repository) : IRequestService
    {
        private readonly IRequestRepository _requestRepository = repository;

        public async Task<List<Request>> GetAllRequests()
        {
            return await _requestRepository.GetAllRequestsAsync();
        }

        public async Task<List<Request>> GetAllUnassignedRequests()
        {
            return await _requestRepository.GetAllUnassignedRequestsAsync();
        }

        public async Task<List<RequestType>> GetRequestTypes()
        {
            return await _requestRepository.GetRequestTypesAsync();
        }

        public async Task<List<RequestStatus>> GetRequestStatuses()
        {
            return await _requestRepository.GetRequestStatusesAsync();
        }

        public async Task<List<Request>> GetRequestsByUserId(int userId)
        {
            return await _requestRepository.GetRequestsByUserIdAsync(userId);
        }
    }
}
