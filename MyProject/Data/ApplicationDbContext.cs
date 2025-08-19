using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyProject.Models.Fruits> Fruits { get; set; } = default!;

        public DbSet<MyProject.Models.FruitsSalescs> FruitsSalescs { get; set; } = default!;
    }
}
