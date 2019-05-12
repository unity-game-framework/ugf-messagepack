using System;
using MessagePack;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime.Resolvers.Enums
{
    public class EnumResolver : IFormatterResolver
    {
        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            IMessagePackFormatter<T> formatter = MessagePackFormatterCache<T>.Formatter;

            if (formatter == null && typeof(T).IsEnum)
            {
                Type underlyingType = typeof(T).GetEnumUnderlyingType();
                TypeCode typeCode = Type.GetTypeCode(underlyingType);

                switch (typeCode)
                {
                    case TypeCode.Byte:
                    {
                        formatter = new EnumFormatterByte<T>();
                        break;
                    }
                    case TypeCode.Int16:
                    {
                        formatter = new EnumFormatterSByte<T>();
                        break;
                    }
                    case TypeCode.Int32:
                    {
                        formatter = new EnumFormatterInt32<T>();
                        break;
                    }
                    case TypeCode.Int64:
                    {
                        formatter = new EnumFormatterInt64<T>();
                        break;
                    }
                    case TypeCode.SByte:
                    {
                        formatter = new EnumFormatterSByte<T>();
                        break;
                    }
                    case TypeCode.UInt16:
                    {
                        formatter = new EnumFormatterUInt16<T>();
                        break;
                    }
                    case TypeCode.UInt32:
                    {
                        formatter = new EnumFormatterUInt16<T>();
                        break;
                    }
                    case TypeCode.UInt64:
                    {
                        formatter = new EnumFormatterUInt64<T>();
                        break;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(typeCode), "The specified underlying type not supported.");
                    }
                }

                MessagePackFormatterCache<T>.Formatter = formatter;
            }

            return formatter;
        }
    }
}
