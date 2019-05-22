using System;

namespace UGF.MessagePack.Runtime.Formatters.Enums
{
    public class EnumProvider : MessagePackProvider
    {
        public IMessagePackContext Context { get; }

        public EnumProvider(IMessagePackContext context)
        {
            Context = context;
        }

        public override IMessagePackFormatter<T> Get<T>()
        {
            Type type = typeof(T);

            if (type.IsEnum)
            {
                IMessagePackFormatter<T> formatter = base.Get<T>();

                if (formatter == null)
                {
                    Type underlyingType = type.GetEnumUnderlyingType();
                    TypeCode typeCode = Type.GetTypeCode(underlyingType);

                    switch (typeCode)
                    {
                        case TypeCode.Byte:
                        {
                            formatter = new EnumFormatterByte<T>(this, Context);
                            break;
                        }
                        case TypeCode.Int16:
                        {
                            formatter = new EnumFormatterInt16<T>(this, Context);
                            break;
                        }
                        case TypeCode.Int32:
                        {
                            formatter = new EnumFormatterInt32<T>(this, Context);
                            break;
                        }
                        case TypeCode.Int64:
                        {
                            formatter = new EnumFormatterInt64<T>(this, Context);
                            break;
                        }
                        case TypeCode.SByte:
                        {
                            formatter = new EnumFormatterSByte<T>(this, Context);
                            break;
                        }
                        case TypeCode.UInt16:
                        {
                            formatter = new EnumFormatterUInt16<T>(this, Context);
                            break;
                        }
                        case TypeCode.UInt32:
                        {
                            formatter = new EnumFormatterUInt32<T>(this, Context);
                            break;
                        }
                        case TypeCode.UInt64:
                        {
                            formatter = new EnumFormatterUInt64<T>(this, Context);
                            break;
                        }
                        default: throw new ArgumentOutOfRangeException(nameof(type), $"The specified enum underlying type not supported: '{underlyingType}.'");
                    }

                    Formatters.Add(type, formatter);
                }

                return formatter;
            }

            return null;
        }
    }
}
