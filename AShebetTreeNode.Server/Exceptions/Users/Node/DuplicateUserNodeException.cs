namespace AShebetTreeNode.Server.Exceptions.Users.Node
{
    public class DuplicateUserNodeException() : SecureException()
    {
        public override string Message => "Duplicate name";
    }
}
