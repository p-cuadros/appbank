using apibanca.application.Entities;
using Microsoft.EntityFrameworkCore;

namespace apibanca.application.Infrastructure.Data;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
                new User { IDUser = 1, UserName = "johndoe", Password = "123" }
            );
        modelBuilder.Entity<Account>().HasData(
                new Account { IDAccount = 1, IDUser = 1, Balance = 100, IsActive = true },
                new Account { IDAccount = 2, IDUser = 1, Balance = 999, IsActive = true },
                new Account { IDAccount = 3, IDUser = 1, Balance = 9999, IsActive = true }
            );
    }
}