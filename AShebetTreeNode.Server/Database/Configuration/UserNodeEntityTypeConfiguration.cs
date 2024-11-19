using AShebetTreeNode.Server.Database.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AShebetTreeNode.Server.Database.Configuration
{
    public class UserNodeEntityTypeConfiguration : IEntityTypeConfiguration<UserNode>
    {
        public void Configure(EntityTypeBuilder<UserNode> builder)
        {
            builder.HasKey(u => u.Id)
                .HasAnnotation("PostgreSQL:IndexMethod", "btree");
        }
    }
}
