using MediStoS.Database.Models;

namespace MediStoS.Database.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);
        Task<bool> DeleteUser(User user);
        Task<User?> GetUser(int id, bool tracking = true);
        Task<User?> GetUser(string email, bool tracking = true);
        Task<List<User>> GetUsers();
        Task<bool> UpdateUser(User user);
    }
}