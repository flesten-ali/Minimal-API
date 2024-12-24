using MinimalAPI.Modles;
namespace MinimalAPI.Interfaces;

public interface IUser
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetUser(string userName, string password);
}
