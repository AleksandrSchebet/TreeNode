using AShebetTreeNode.Server.Database;
using AShebetTreeNode.Server.Exceptions.Users.Node;
using MediatR;


namespace AShebetTreeNode.Server.MediatR.Users.Node.Commands.Rename
{
    public sealed record RenameUserNodeCommandHandler(AppDbContext DbContext) : IRequestHandler<RenameUserNodeCommand, Unit>
    {
        public async Task<Unit> Handle(RenameUserNodeCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await DbContext
                .FindByTreeNameAndNodeIdAsync(request.TreeName, request.NodeId, cancellationToken);
            if (dbUser.Name == request.NewNodeName)
            {
                throw new DuplicateUserNodeException();
            }
            dbUser.Name = request.NewNodeName;
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
