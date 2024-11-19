using MediatR;

namespace AShebetTreeNode.Server.MediatR.Users.Node.Queries.Get
{
    public sealed record GetUserNodeQuery(string Name) : IRequest<UserNodeModel>;
}
