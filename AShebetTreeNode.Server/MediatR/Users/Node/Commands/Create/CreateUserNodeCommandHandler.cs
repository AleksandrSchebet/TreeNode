using AShebetTreeNode.Server.Database;
using AShebetTreeNode.Server.Database.Models.Users;
using AShebetTreeNode.Server.Exceptions.Users.Node;
using MediatR;

namespace AShebetTreeNode.Server.MediatR.Users.Node.Commands.Create
{
    public sealed record CreateUserNodeCommandHandler(AppDbContext DbContext) : IRequestHandler<CreateUserNodeCommand, UserNodeModel>
    {
        public async Task<UserNodeModel> Handle(CreateUserNodeCommand request, CancellationToken cancellationToken)
        {
            var parent = await DbContext
                .FindByTreeNameAndNodeIdAsync(request.TreeName, request.ParentNodeId, cancellationToken);
            
            if (parent.Children?.Count > 0 && parent.Children.Any(p=>p.Name == request.NodeName))
            {
                throw new DuplicateUserNodeException();
            }

            var result = await DbContext.UserNodes.AddAsync(new UserNode() { Name = request.NodeName, ParentId = request.ParentNodeId }, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return new UserNodeModel(result.Entity.Id, result.Entity.Name, []);
        }
    }
}
