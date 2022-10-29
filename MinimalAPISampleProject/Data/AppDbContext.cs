using Microsoft.EntityFrameworkCore;
using MinimalAPISampleProject.Models;

namespace MinimalAPISampleProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)   
        {

        }

        public DbSet<Command> Commands => Set<Command>();
    }
}
