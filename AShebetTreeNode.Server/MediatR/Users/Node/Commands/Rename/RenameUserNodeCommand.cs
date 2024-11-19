using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AShebetTreeNode.Server.MediatR.Users.Node.Commands.Rename
{
    public sealed record RenameUserNodeCommand([Required] string TreeName, [Required] int NodeId, [Required] string NewNodeName) : IRequest<Unit>;
}
