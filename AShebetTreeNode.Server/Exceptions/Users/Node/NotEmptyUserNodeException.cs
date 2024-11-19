namespace AShebetTreeNode.Server.Exceptions.Users.Node
{
    public class NotEmptyUserNodeException() : SecureException()
    {
        public override string Message => "You have to delete all children nodes first";
    }
}
