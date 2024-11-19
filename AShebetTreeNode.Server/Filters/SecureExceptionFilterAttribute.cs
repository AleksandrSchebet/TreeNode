using AShebetTreeNode.Server.Database;
using AShebetTreeNode.Server.Database.Models.Logs;
using AShebetTreeNode.Server.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace AShebetTreeNode.Server.Filters
{
    public class SecureExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            Guid id = Guid.NewGuid();
            string type = exception is SecureException || exception is DbUpdateException ? "Secure" : exception.GetType().ToString();
            string message = exception.InnerException?.Message ?? exception.Message;

            ExceptionLog logItem = new() {
                EventId = id,
                Message = message,
                StackTrace = exception.StackTrace,
                Created = DateTime.UtcNow
            };

            var config = context.HttpContext.RequestServices.GetService<IConfiguration>();
            if (config != null)
            {
                using var dbContext = new AppDbFactory(config).CreateDbContext();
                dbContext.ExceptionLogs.Add(logItem);
                dbContext.SaveChanges();
            }
            
            var responseObject = new { type, id = id.ToString(), data = new { message } };
            context.Result = new ObjectResult(responseObject) { StatusCode = StatusCodes.Status500InternalServerError };
            context.ExceptionHandled = true;
        }
    }
}
