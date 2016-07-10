using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public interface IMySqlModelAnnotations : IRelationalModelAnnotations
    {
        IReadOnlyList<IMySqlExtension> MySqlExtensions { get; }
        string DatabaseTemplate { get; }
    }
}
