using System;
using MessagePack;
using MessagePack.Formatters;
using UGF.Types.Runtime;

namespace UGF.MessagePack.Runtime.Resolvers.Typed
{
    public class MessagePackFormatterTypedGuid<T> : IMessagePackFormatter<T>
    {
        public ITypeProvider<Guid> TypeProvider { get; }

        public MessagePackFormatterTypedGuid(ITypeProvider<Guid> typeProvider)
        {
            TypeProvider = typeProvider;
        }

        public void Serialize(ref MessagePackWriter writer, T value, IFormatterResolver resolver)
        {
            if (value == null)
            {
                writer.WriteNil();
            }
            else
            {
                Type type = value.GetType();

                if (TypeProvider.TryGetIdentifier(type, out Guid identifier))
                {
                    if (TryGetFormatter(type, resolver, out IMessagePackFormatter<object> formatter))
                    {
                        resolver.GetFormatterWithVerify<Guid>().Serialize(ref writer, identifier, resolver);

                        formatter.Serialize(ref writer, value, resolver);
                    }
                    else
                    {
                        throw new Exception($"The formatter for the specified type not found: '{type}'.");
                    }
                }
                else
                {
                    throw new Exception($"The identifier for the specified type not found: '{type}'.");
                }
            }
        }

        public T Deserialize(ref MessagePackReader reader, IFormatterResolver resolver)
        {
            if (!reader.TryReadNil())
            {
                Guid identifier = resolver.GetFormatterWithVerify<Guid>().Deserialize(ref reader, resolver);

                if (TypeProvider.Types.TryGetValue(identifier, out Type type))
                {
                    if (TryGetFormatter(type, resolver, out IMessagePackFormatter<object> formatter))
                    {
                        return (T)formatter.Deserialize(ref reader, resolver);
                    }

                    throw new Exception($"The formatter for the specified type not found: '{type}'.");
                }

                throw new Exception($"The type for the specified identifier not found: '{identifier}'.");
            }

            return default;
        }

        private bool TryGetFormatter(Type type, IFormatterResolver resolver, out IMessagePackFormatter<object> formatter)
        {
            formatter = resolver.GetFormatterDynamic(type) as IMessagePackFormatter<object>;

            return formatter != null;
        }
    }
}
