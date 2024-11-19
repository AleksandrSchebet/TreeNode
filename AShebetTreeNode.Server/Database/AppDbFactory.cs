using Microsoft.EntityFrameworkCore;

namespace AShebetTreeNode.Server.Database
{
    public class AppDbFactory(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public AppDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var config = Configuration.GetConnectionString("AppDbContext");
            optionsBuilder.UseNpgsql(config);
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
