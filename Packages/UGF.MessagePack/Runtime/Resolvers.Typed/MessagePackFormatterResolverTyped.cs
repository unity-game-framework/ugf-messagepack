using System;
using MessagePack;
using MessagePack.Formatters;
using UGF.Types.Runtime;

namespace UGF.MessagePack.Runtime.Resolvers.Typed
{
    public class MessagePackFormatterResolverTyped : IFormatterResolver
    {
        public ITypeProvider<Guid> TypeProvider { get; }

        private static class FormatterCache<T>
        {
            public static IMessagePackFormatter<T> Formatter;
        }

        public MessagePackFormatterResolverTyped(ITypeProvider<Guid> typeProvider)
        {
            TypeProvider = typeProvider;
        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            IMessagePackFormatter<T> formatter = FormatterCache<T>.Formatter;

            if (formatter == null)
            {
                formatter = new MessagePackFormatterTypedGuid<T>(TypeProvider);

                FormatterCache<T>.Formatter = formatter;
            }

            return formatter;
        }
    }
}
