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
        /// The value that determines whether to ignore fields or properties which does not contain "KeyAttribute".
        /// <para>
        /// If this value is 'false', fields or properties must contains "KeyAttribute" and generator will throw exception if they not.
        /// </para>
        /// <para>
        /// This parameter will not be used if the generator forced to use map serialization.
        /// </para>
        /// </summary>
        public bool IgnoreNotMarked;

        /// <summary>
        /// The value that determines whether to generate formatters for Enums.
        /// </summary>
        public bool GenerateEnumFormatters;

        /// <summary>
        /// The value that determines whether to generate formatters for union types.
        /// </summary>
        public bool GenerateUnionFormatters;
    }
}
