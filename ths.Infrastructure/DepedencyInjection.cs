using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ths.Infrastructure
{
    public static class DepedencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>();
            var dbContext = new ApplicationDbContext(configuration);

            var pendingMigrations = dbContext.Database.GetPendingMigrations();

            if (pendingMigrations.Count() > 0)
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
