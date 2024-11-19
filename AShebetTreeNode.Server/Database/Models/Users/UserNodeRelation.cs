using System.ComponentModel.DataAnnotations.Schema;

namespace AShebetTreeNode.Server.Database.Models.Users
{
    public class UserNodeRelation
    {
        public int ChildId { get; set; }
        public int ParentId { get; set; }
    }
}
