using AShebetTreeNode.Server.Database;
using AShebetTreeNode.Server.Exceptions.Users.Node;
using MediatR;


namespace AShebetTreeNode.Server.MediatR.Users.Node.Commands.Delete
{
    public sealed record DeleteUserNodeCommandHandler(AppDbContext DbContext) : IRequestHandler<DeleteUserNodeCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteUserNodeCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await DbContext
                .FindByTreeNameAndNodeIdAsync(request.TreeName, request.NodeId, cancellationToken);
            if (dbUser.Children?.Count > 0)
            {
                throw new NotEmptyUserNodeException();
            }
            if (!dbUser.ParentId.HasValue)
            {
                throw new UserNodeException("Rename of Root level node is not allowed");
            }
            DbContext.UserNodes.Remove(dbUser);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
