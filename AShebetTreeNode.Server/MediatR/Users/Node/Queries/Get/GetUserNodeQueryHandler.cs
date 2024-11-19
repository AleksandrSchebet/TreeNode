using AShebetTreeNode.Server.Database;
using AShebetTreeNode.Server.Database.Models.Users;
using AShebetTreeNode.Server.Exceptions.Users.Node;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AShebetTreeNode.Server.MediatR.Users.Node.Queries.Get
{
    public sealed record GetUserNodeQueryHandler(AppDbContext DbContext) : IRequestHandler<GetUserNodeQuery, UserNodeModel>
    {
        public async Task<UserNodeModel> Handle(GetUserNodeQuery request, CancellationToken cancellationToken)
        {
            var nodes = await DbContext
                .GetTreeNodeRecursively(request.Name)
                .ToListAsync(cancellationToken);

            if (nodes?.Count == 0)
                throw new NotFoundUserNodeException(request.Name);

            return nodes!.AsQueryable().Select(ModelProjection()).First();
        }

        private static Expression<Func<UserNode, UserNodeModel>> ModelProjection()
        {
            Expression<Func<UserNode, UserNodeModel>> result = user =>
                new UserNodeModel(
                    user.Id,
                    user.Name,
                    user.Children == null || user.Children.Count == 0 ?
                    new List<UserNodeModel>() : user.Children.AsQueryable().Select(ModelProjection()).ToList());
            return result;
        }
    }
}
