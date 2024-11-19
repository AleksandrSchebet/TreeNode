using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AShebetTreeNode.Server.Database.Models.Users
{
    [Index(nameof(Name), nameof(ParentId), IsUnique = true)]
    public class UserNode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public virtual UserNode? Parent { get; set; }
        [InverseProperty(nameof(Parent))]
        public virtual ICollection<UserNode>? Children { get; set; } = [];
    }
}
