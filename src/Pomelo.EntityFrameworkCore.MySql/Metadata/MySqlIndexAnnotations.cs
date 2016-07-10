using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class MySqlIndexAnnotations : RelationalIndexAnnotations, IMySqlIndexAnnotations
    {
        public MySqlIndexAnnotations([NotNull] IIndex index)
            : base(index, MySqlFullAnnotationNames.Instance)
        {
        }

        protected MySqlIndexAnnotations([NotNull] RelationalAnnotations annotations)
            : base(annotations, MySqlFullAnnotationNames.Instance)
        {
        }

        public string Method
        {
            get { return (string)Annotations.GetAnnotation(MySqlFullAnnotationNames.Instance.IndexMethod, null); }
            set { Annotations.SetAnnotation(MySqlFullAnnotationNames.Instance.IndexMethod, null, value); }
        }
    }
}
