using Microsoft.CodeAnalysis.CSharp;

namespace MessagePack.UniversalCodeGenerator
{
    /// <summary>
    /// Represents arguments used to generate MessagePack formatters.
    /// </summary>
    public struct MessagePackGenerateArguments
    {
        /// <summary>
        /// The value that determines whether to ignore read-only fields and properties.
        /// </summary>
        public bool IgnoreReadOnly;

        /// <summary>
        /// The value that determines whether target type must contains specific attribute.
        /// </summary>
        public bool IsTypeRequireAttribute;

        /// <summary>
        /// The short name of attribute that type must contains.
        /// </summary>
        public string TypeRequiredAttributeShortName;

        public CSharpCompilation Compilation;
    }
}
