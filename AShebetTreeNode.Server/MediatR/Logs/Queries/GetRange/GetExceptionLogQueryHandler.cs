using AShebetTreeNode.Server.Database;
using AShebetTreeNode.Server.Database.Models.Logs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AShebetTreeNode.Server.MediatR.Logs.Queries.GetRange
{
    public class GetExceptionLogQueryHandler(AppDbContext DbContext) : IRequestHandler<GetExceptionLogQuery, IEnumerable<ExceptionLog>>
    {
        public async Task<IEnumerable<ExceptionLog>> Handle(GetExceptionLogQuery request, CancellationToken cancellationToken)
        {
            var query = DbContext.ExceptionLogs.AsQueryable();
            if (request.Filter != null)
            {
                if (request.Filter.From != null)
                {
                    _ = query.Where(e => e.Created >= request.Filter.From);
                }
                if (request.Filter.To != null)
                {
                    _ = query.Where(e => e.Created <= request.Filter.To);
                }
                if (!string.IsNullOrEmpty(request.Filter.Search))
                {
                    _ = query.Where(e => e.Message.Contains(request.Filter.Search, StringComparison.OrdinalIgnoreCase) || e.StackTrace != null && e.StackTrace.Contains(request.Filter.Search, StringComparison.OrdinalIgnoreCase));
                }
            }
            return await query
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync(cancellationToken);
        }
    }
}
