using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ths.Shared.Entities;

namespace ths.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Provider> Providers { get; set; }

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
            optionsBuilder.UseMySql(_configuration.GetConnectionString("ths"), serverVersion);
        }
    }
}
