using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AShebetTreeNode.Server.MediatR.Users.Node.Commands.Create
{
    public sealed record CreateUserNodeCommand([Required] string TreeName, [Required] int ParentNodeId, [Required] string NodeName) : IRequest<UserNodeModel>;
}
