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
        Task<List<RequestType>> GetRequestTypes();
        Task<List<RequestStatus>> GetRequestStatuses();
    }

    public class RequestService(IRequestRepository repository) : IRequestService
    {
        private readonly IRequestRepository _requestRepository = repository;

        public async Task<List<RequestType>> GetRequestTypes()
        {
            return await _requestRepository.GetRequestTypesAsync();
        }

        public async Task<List<RequestStatus>> GetRequestStatuses()
        {
            return await _requestRepository.GetRequestStatusesAsync();
        }
    }
}
