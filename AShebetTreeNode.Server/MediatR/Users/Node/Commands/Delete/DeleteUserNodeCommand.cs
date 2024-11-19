using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AShebetTreeNode.Server.MediatR.Users.Node.Commands.Delete
{
    public sealed record DeleteUserNodeCommand([Required] string TreeName, [Required] int NodeId, [Required] string Name) : IRequest<Unit>;
}
