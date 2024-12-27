using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MediStoS.Database.Repository.UserRepository;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetUser(int id, bool tracking = true)
    {
        if (tracking == true)
        {
            return await context.Users.FirstOrDefaultAsync(a => a.Id == id);
        }
        else
        {
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
    }

    public async Task<User?> GetUser(string email, bool tracking = true)
    {
        if (tracking == true)
        {
            return await context.Users.FirstOrDefaultAsync(a => a.Email == email);
        }
        else
        {
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Email == email);
        }
    }

    public async Task<bool> AddUser(User user)
    {
        await context.Users.AddAsync(user);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteUser(User user)
    {
        context.Users.Remove(user);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> UpdateUser(User user)
    {
        context.Users.Update(user);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<List<User>> GetUsers()
    {
        List<User> users = await context.Users.ToListAsync();
        if (users == null) return new List<User>();
        return users;
    }
}
