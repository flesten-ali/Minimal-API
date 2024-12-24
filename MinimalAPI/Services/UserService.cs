using Microsoft.EntityFrameworkCore;
using MinimalAPI.DbContexts;
using MinimalAPI.Interfaces;
using MinimalAPI.Modles;
namespace MinimalAPI.Services;

public class UserService : IUser
{
    private MinimalDbContext _context;

    public UserService(MinimalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUser(string userName, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(e => e.UserName == userName && e.Password == password);
    }
}