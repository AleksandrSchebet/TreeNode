using AShebetTreeNode.Server.Database.Configuration;
using AShebetTreeNode.Server.Database.Models.Logs;
using AShebetTreeNode.Server.Database.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace AShebetTreeNode.Server.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public virtual DbSet<UserNode> UserNodes { get; set; } = default!;
        public virtual DbSet<UserNodeRelation> UserNodeRelations { get; set; } = default!;
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserNodeEntityTypeConfiguration());
            modelBuilder
                .Entity<UserNodeRelation>()
                .ToView("UserNodeRelations")
                .HasKey(v => new {v.ParentId, v.ChildId});
        }
    }
}
