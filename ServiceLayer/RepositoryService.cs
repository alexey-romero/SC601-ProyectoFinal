using RepositoryLayer;
using RepositoryLayer.Models;

namespace ServiceLayer
{
    public interface IRepositoryService
    {
        Task<User> GetUserByEmail(string email);
        Task<UserRole> GetUserRole(int userId);
       // Task<List<Request>> GetRequestsByUserId(int userId);
    }

    public class RepositoryService(IRepository repository): IRepositoryService
    {
        private readonly IRepository _repository = repository;

        public async Task<User> GetUserByEmail(string email)
        {
            return await _repository.GetUserByEmailAsync(email);
        }

        public async Task<UserRole> GetUserRole(int userId)
        {
            return await _repository.GetUserRoleAsync(userId);
        }
       /* public async Task<List<Request>> GetRequestsByUserId(int userId)
        {
            return await _repository.GetRequestsByUserIdAsync(userId);
        }*/

    }
}
