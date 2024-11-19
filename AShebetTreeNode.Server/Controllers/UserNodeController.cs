using AShebetTreeNode.Server.Filters;
using AShebetTreeNode.Server.MediatR.Users.Node;
using AShebetTreeNode.Server.MediatR.Users.Node.Commands.Create;
using AShebetTreeNode.Server.MediatR.Users.Node.Commands.Delete;
using AShebetTreeNode.Server.MediatR.Users.Node.Commands.Rename;
using AShebetTreeNode.Server.MediatR.Users.Node.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AShebetTreeNode.Server.Controllers
{
    [ApiController]
    [Route("api.v1.user.node")]
    [SecureExceptionFilter]
    public class UserNodeController(IMediator mediator) : ControllerBase
    {
        [HttpGet("get/{name}")]
        public async Task<UserNodeModel> Get(string name, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(new GetUserNodeQuery(name), cancellationToken);
        }

        [HttpPost("create")]
        public async Task<UserNodeModel> Create([FromQuery] CreateUserNodeCommand command, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(command, cancellationToken);
        }

        [HttpPost("rename")]
        public async Task<Unit> Rename([FromQuery] RenameUserNodeCommand command, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(command, cancellationToken);
        }

        [HttpPost("delete")]
        public async Task<Unit> Delete([FromQuery] DeleteUserNodeCommand commang, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(commang, cancellationToken);
        }
    }
}
