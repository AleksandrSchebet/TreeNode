namespace AShebetTreeNode.Server.Exceptions.Users.Node
{
    public class NotFoundUserNodeException(string identificator) : SecureException()
    {
        public override string Message => $"User {identificator} not found in the tree";
    }
}
