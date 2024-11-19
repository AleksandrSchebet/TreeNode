using AShebetTreeNode.Server.Database.Models.Logs;
using AShebetTreeNode.Server.MediatR.Logs.Queries.GetRange;
using AShebetTreeNode.Server.MediatR.Logs.Queries.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AShebetTreeNode.Server.Controllers
{
    [ApiController]
    [Route("api.v1.exception.logs")]
    public class ExceptionsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("getRange")]
        public async Task<IEnumerable<ExceptionLog>> GetRange([FromQuery] GetExceptionLogQuery query, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(query, cancellationToken);
        }

        [HttpPost("getSingle")]
        public async Task<ExceptionLog?> GetSingle([FromQuery] GetSingleExceptionLogQuery query, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(query, cancellationToken);
        }
    }
}
