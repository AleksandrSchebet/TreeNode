using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AShebetTreeNode.Server.Database.Models.Logs
{
    public class ExceptionLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public Guid EventId { get; set; }
        public required string Message { get; set; }
        public string? StackTrace { get; set; }
        public DateTime Created { get; set; }
    }
}
