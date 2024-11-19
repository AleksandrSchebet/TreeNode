using AShebetTreeNode.Server.Database.Models.Logs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AShebetTreeNode.Server.MediatR.Logs.Queries.GetSingle
{
    public sealed record GetSingleExceptionLogQuery([Required] Guid EventId) : IRequest<ExceptionLog?>;
}
