namespace AShebetTreeNode.Server.MediatR.Users.Node
{
    public record UserNodeModel(int Id, string Name, IEnumerable<UserNodeModel> Children);
}
