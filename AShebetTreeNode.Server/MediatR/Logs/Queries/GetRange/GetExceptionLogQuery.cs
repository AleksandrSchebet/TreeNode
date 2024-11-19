using AShebetTreeNode.Server.Database.Models.Logs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AShebetTreeNode.Server.MediatR.Logs.Queries.GetRange
{
    public sealed record GetExceptionLogQuery([Required] int Skip, [Required] int Take, Filter? Filter) : IRequest<IEnumerable<ExceptionLog>>;
}
