using System;
using System.Collections.Generic;
using MessagePack;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime.Resolvers.Enums
{
    public class EnumResolver : IFormatterResolver
    {
        public Dictionary<Type, Type> UnderlyingTypeFormatterTypeDefinitions { get; } = new Dictionary<Type, Type>
        {
            { typeof(byte), typeof(EnumFormatterByte<>) },
            { typeof(sbyte), typeof(EnumFormatterSByte<>) },
            { typeof(short), typeof(EnumFormatterInt16<>) },
            { typeof(ushort), typeof(EnumFormatterUInt16<>) },
            { typeof(int), typeof(EnumFormatterInt32<>) },
            { typeof(uint), typeof(EnumFormatterUInt32<>) },
            { typeof(long), typeof(EnumFormatterInt64<>) },
            { typeof(ulong), typeof(EnumFormatterUInt64<>) }
        };

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            IMessagePackFormatter<T> formatter = MessagePackFormatterCache<T>.Formatter;

            if (formatter == null)
            {
                Type underlyingType = typeof(T).GetEnumUnderlyingType();

                if (!UnderlyingTypeFormatterTypeDefinitions.TryGetValue(underlyingType, out Type formatterTypeDefinition))
                {
                    throw new ArgumentException($"Enum formatter type definition for specified underlying type not found: '{underlyingType}'.");
                }

                Type formatterType = formatterTypeDefinition.MakeGenericType(typeof(T));

                formatter = Activator.CreateInstance(formatterType) as IMessagePackFormatter<T>;

                if (formatter == null)
                {
                    throw new ArgumentException($"Failed to create instance of the specified enum formatter: '{formatterType}'.");
                }

                MessagePackFormatterCache<T>.Formatter = formatter;
            }

            return formatter;
        }
    }
}
