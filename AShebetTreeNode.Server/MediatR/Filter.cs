namespace AShebetTreeNode.Server.MediatR
{
    public record Filter(DateTime? From, DateTime? To, string? Search);
}
