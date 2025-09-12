using Microsoft.EntityFrameworkCore;
using LearnMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LearnMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Add any custom model configurations here
        }
    }
}
