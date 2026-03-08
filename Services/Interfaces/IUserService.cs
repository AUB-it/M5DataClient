using DataClient.Models;

namespace DataClient.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User?> GetUserById(Guid guid);
    Task<bool> CreateUser(UserDTO user);
    Task<bool> UpdateUser(UserDTO user, Guid guid);
    Task<bool> DeleteUser(Guid guid);
}