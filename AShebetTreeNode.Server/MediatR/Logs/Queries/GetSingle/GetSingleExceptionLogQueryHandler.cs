using AShebetTreeNode.Server.Database;
using AShebetTreeNode.Server.Database.Models.Logs;
using AShebetTreeNode.Server.MediatR.Logs.Queries.GetRange;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AShebetTreeNode.Server.MediatR.Logs.Queries.GetSingle
{
    public class GetSingleExceptionLogQueryHandler(AppDbContext DbContext) : IRequestHandler<GetSingleExceptionLogQuery, ExceptionLog?>
    {
        public async Task<ExceptionLog?> Handle(GetSingleExceptionLogQuery request, CancellationToken cancellationToken)
        {
            return await DbContext.ExceptionLogs.FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);
        }
    }
}
