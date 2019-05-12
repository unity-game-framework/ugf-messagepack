using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack.CodeGenerator;
using MessagePack.CodeGenerator.Generator;

namespace MessagePack.UniversalCodeGenerator
{
    /// <summary>
    /// Provides utilities to work with MessagePack UniversalCodeGenerator.
    /// </summary>
    public static class MessagePackUniversalCodeGeneratorUtility
    {
        /// <summary>
        /// Generates resolver and formatters files and other arguments.
        /// </summary>
        /// <param name="inputFiles">The collection of input .cs files.</param>
        /// <param name="resolverName">The generated resolver name.</param>
        /// <param name="namespaceRoot">The root namespace for generated resolver and formatters.</param>
        /// <param name="arguments">The generate arguments to control additional generation behaviour.</param>
        public static string Generate(IEnumerable<string> inputFiles, string resolverName = "GeneratedResolver", string namespaceRoot = "MessagePack", MessagePackGenerateArguments arguments = default(MessagePackGenerateArguments))
        {
            return Generate(inputFiles, null, null, false, resolverName, namespaceRoot, arguments);
        }

        /// <summary>
        /// Generates resolver and formatters from the specified files and other arguments.
        /// </summary>
        /// <param name="inputFiles">The collection of input .cs files.</param>
        /// <param name="inputDirectories">The collection of directories with input .cs files.</param>
        /// <param name="conditionalSymbols">The collection of conditional compile symbols.</param>
        /// <param name="useMap">The value that determines whether to use map serialization.</param>
        /// <param name="resolverName">The generated resolver name.</param>
        /// <param name="namespaceRoot">The root namespace for generated resolver and formatters.</param>
        /// <param name="arguments">The generate arguments to control additional generation behaviour.</param>
        public static string Generate(IEnumerable<string> inputFiles, IEnumerable<string> inputDirectories = null, IEnumerable<string> conditionalSymbols = null, bool useMap = false, string resolverName = "GeneratedResolver", string namespaceRoot = "MessagePack", MessagePackGenerateArguments arguments = default(MessagePackGenerateArguments))
        {
            return InternalGenerate(InternalGetArguments(inputFiles, inputDirectories, conditionalSymbols, useMap, resolverName, namespaceRoot), arguments, true);
        }

        /// <summary>
        /// Generates formatters without resolver from the specified files and other arguments.
        /// </summary>
        /// <param name="inputFiles">The collection of input .cs files.</param>
        /// <param name="namespaceRoot">The root namespace for generated resolver and formatters.</param>
        /// <param name="arguments">The generate arguments to control additional generation behaviour.</param>
        public static string GenerateFormatters(IEnumerable<string> inputFiles, string namespaceRoot = "MessagePack", MessagePackGenerateArguments arguments = default(MessagePackGenerateArguments))
        {
            return GenerateFormatters(inputFiles, null, null, false, namespaceRoot, arguments);
        }

        /// <summary>
        /// Generates formatters without resolver from the specified files and other arguments.
        /// </summary>
        /// <param name="inputFiles">The collection of input .cs files.</param>
        /// <param name="inputDirectories">The collection of directories with input .cs files.</param>
        /// <param name="conditionalSymbols">The collection of conditional compile symbols.</param>
        /// <param name="useMap">The value that determines whether to use map serialization.</param>
        /// <param name="namespaceRoot">The root namespace for generated resolver and formatters.</param>
        /// <param name="arguments">The generate arguments to control additional generation behaviour.</param>
        public static string GenerateFormatters(IEnumerable<string> inputFiles, IEnumerable<string> inputDirectories = null, IEnumerable<string> conditionalSymbols = null, bool useMap = false, string namespaceRoot = "MessagePack", MessagePackGenerateArguments arguments = default(MessagePackGenerateArguments))
        {
            return InternalGenerate(InternalGetArguments(inputFiles, inputDirectories, conditionalSymbols, useMap, "GeneratedResolver", namespaceRoot), arguments, false);
        }

        private static CommandlineArguments InternalGetArguments(IEnumerable<string> inputFiles, IEnumerable<string> inputDirectories = null, IEnumerable<string> conditionalSymbols = null, bool useMap = false, string resolverName = "GeneratedResolver", string namespaceRoot = "MessagePack")
        {
            if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
            if (string.IsNullOrEmpty(resolverName)) throw new ArgumentException("Resolver name must be specified.", nameof(resolverName));

            return new CommandlineArguments
            {
                InputFiles = new List<string>(inputFiles),
                InputDirectories = new List<string>(inputDirectories ?? Enumerable.Empty<string>()),
                ConditionalSymbols = new List<string>(conditionalSymbols ?? Enumerable.Empty<string>()),
                IsUseMap = useMap,
                ResolverName = resolverName,
                NamespaceRoot = namespaceRoot
            };
        }

        private static string InternalGenerate(CommandlineArguments arguments, MessagePackGenerateArguments arguments2, bool generateResolver)
        {
            var collector = new TypeCollector(arguments.InputFiles, arguments.InputDirectories, arguments.ConditionalSymbols, true, arguments.IsUseMap, arguments2);

            (ObjectSerializationInfo[] objectInfo, EnumSerializationInfo[] enumInfo, GenericSerializationInfo[] genericInfo, UnionSerializationInfo[] unionInfo) = collector.Collect();

            FormatterTemplate[] objectFormatterTemplates = objectInfo
                .GroupBy(x => x.Namespace)
                .Select(x => new FormatterTemplate
                {
                    Namespace = arguments.GetNamespaceDot() + "Formatters" + ((x.Key == null) ? "" : "." + x.Key),
                    objectSerializationInfos = x.ToArray(),
                })
                .ToArray();

            EnumTemplate[] enumFormatterTemplates = enumInfo
                .GroupBy(x => x.Namespace)
                .Select(x => new EnumTemplate
                {
                    Namespace = arguments.GetNamespaceDot() + "Formatters" + ((x.Key == null) ? "" : "." + x.Key),
                    enumSerializationInfos = x.ToArray()
                })
                .ToArray();

            UnionTemplate[] unionFormatterTemplates = unionInfo
                .GroupBy(x => x.Namespace)
                .Select(x => new UnionTemplate
                {
                    Namespace = arguments.GetNamespaceDot() + "Formatters" + ((x.Key == null) ? "" : "." + x.Key),
                    unionSerializationInfos = x.ToArray()
                })
                .ToArray();

            var resolverTemplate = new ResolverTemplate
            {
                Namespace = arguments.GetNamespaceDot() + "Resolvers",
                FormatterNamespace = arguments.GetNamespaceDot() + "Formatters",
                ResolverName = arguments.ResolverName,
                registerInfos = genericInfo.Cast<IResolverRegisterInfo>().Concat(enumInfo).Concat(unionInfo).Concat(objectInfo).ToArray()
            };

            var builder = new StringBuilder();

            if (generateResolver)
            {
                builder.AppendLine(resolverTemplate.TransformText());
                builder.AppendLine();
            }

            if (arguments2.GenerateEnumFormatters)
            {
                foreach (EnumTemplate item in enumFormatterTemplates)
                {
                    string text = item.TransformText();

                    builder.AppendLine(text);
                }

                builder.AppendLine();
            }

            if (arguments2.GenerateUnionFormatters)
            {
                foreach (UnionTemplate item in unionFormatterTemplates)
                {
                    string text = item.TransformText();

                    builder.AppendLine(text);
                }

                builder.AppendLine();
            }

            foreach (FormatterTemplate item in objectFormatterTemplates)
            {
                string text = item.TransformText();

                builder.AppendLine(text);
            }

            return builder.ToString();
        }
    }
}
