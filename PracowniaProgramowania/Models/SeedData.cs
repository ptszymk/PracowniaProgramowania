using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PracowniaProgramowania.Data;
using System;
using System.Linq;


namespace PracowniaProgramowania.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PracowniaProgramowaniaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PracowniaProgramowaniaContext>>()))
            {

                if (context.Users.Any() && context.Roles.Any())
                {
                    return;   // DB has been seeded
                }

                context.Users.AddRange(
                    new Users
                    {
                        Name = "admin",
                        Surname = "admin",
                        Login = "admin",
                        Password = "admin",
                        Roles = new Roles() { Name= "admin" }

                    },

                    new Users
                    {
                        Name = "Jan",
                        Surname = "Kowalski",
                        DateOfBirth = DateTime.Parse("1984-3-13"),
                        Login = "jk13",
                        Password = "123",
                        Roles = new Roles() { Name = "user"}
                    }

                );
                context.SaveChanges();
            }
        }
    }

}
