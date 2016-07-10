using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public interface IMySqlIndexAnnotations : IRelationalIndexAnnotations
    {
        string Method { get; }
    }
}