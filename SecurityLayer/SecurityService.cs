using RepositoryLayer;
using RepositoryLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLayer;

public interface ISecurityService
{
    Task<bool> AuthUserAsync(User user);
    Task<bool> AuthUserByEmailAsync(User user);
    Task<bool> RegisterUserAsync(User user);

}

public class SecurityService(ISecurityRepository securityRepository) : ISecurityService
{
    private readonly ISecurityRepository _securityRepository = securityRepository;

    public async Task<bool> AuthUserAsync(User user)
    {
        return await _securityRepository.AuthenticateUserAsync(user);
    }
    public async Task<bool> AuthUserByEmailAsync(User user)
    {
        return await _securityRepository.AuthenticateUserAsync(user);
    }

    public async Task<bool> RegisterUserAsync(User user)
    {
        return await _securityRepository.RegisterUserAsync(user);
    }

}
