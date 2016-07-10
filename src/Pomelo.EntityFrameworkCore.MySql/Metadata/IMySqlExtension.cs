namespace Microsoft.EntityFrameworkCore.Metadata
{
    public interface IMySqlExtension
    {
        IModel Model { get; }
        string Name { get; }
        string Schema { get; }
        string Version { get; }
    }
}