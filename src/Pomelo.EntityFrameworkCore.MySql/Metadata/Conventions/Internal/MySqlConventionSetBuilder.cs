using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions
{
    public class MySqlConventionSetBuilder : RelationalConventionSetBuilder
    {
        public MySqlConventionSetBuilder(
            [NotNull] IRelationalTypeMapper typeMapper,
            [CanBeNull] ICurrentDbContext currentContext,
            [CanBeNull] IDbSetFinder setFinder)
            : base(typeMapper, currentContext, setFinder)
        {
        }
    }
}