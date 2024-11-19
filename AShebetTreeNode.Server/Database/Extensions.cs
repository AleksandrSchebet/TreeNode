using AShebetTreeNode.Server.Database.Models.Users;
using AShebetTreeNode.Server.Exceptions.Users.Node;
using AShebetTreeNode.Server.MediatR.Users.Node;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AShebetTreeNode.Server.Database
{
    public static class Extensions
    {
        public static async Task<UserNode> FindByTreeNameAndNodeIdAsync(this AppDbContext dbContext, string treeName, int nodeId, CancellationToken cancellationToken = default)
        {
            return await dbContext.GetTreeNodeRecursively(treeName)
                .Include(u=>u.Children)
                .FirstOrDefaultAsync(u => u.Id == nodeId, cancellationToken) ?? throw new NotFoundUserNodeException($"with nodeId {nodeId}");
        }

        public static IQueryable<UserNode> GetTreeNodeRecursively(this AppDbContext dbContext, string nodeName)
        {
            return from relations in dbContext.UserNodeRelations 
                   join parents in dbContext.UserNodes on relations.ParentId equals parents.Id 
                   join childs in dbContext.UserNodes on relations.ChildId equals childs.Id
                   where parents.Name == nodeName 
                   select childs;
        }
    }
}
