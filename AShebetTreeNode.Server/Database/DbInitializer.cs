using AShebetTreeNode.Server.Database.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace AShebetTreeNode.Server.Database
{
    public class DbInitializer
    {
        internal static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            var root = await dbContext.UserNodes.Take(1).FirstOrDefaultAsync();

            if (root == null)
            {
                string rootUserName = config.GetValue<string>("AppSettings:RootName") ?? "Root";
                dbContext.UserNodes.Add(new UserNode() { Name = rootUserName });
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
