using MinimalAPI.DbContexts;
using MinimalAPI.Modles;
namespace MinimalAPI.Test;

public static class UserSeeder
{
    public static void SeedUsers(MinimalDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User
                {
                    Id = 1,
                    UserName = "nour123",
                    LastName = "nour",
                    FirstName = "Nour",
                    Password = "123"

                }
                , new User
                {
                    Id = 2,
                    UserName = "ali123",
                    LastName = "ali",
                    FirstName = "ali",
                    Password = "123"
                }
            );
            context.SaveChanges();
        }
    }
}
