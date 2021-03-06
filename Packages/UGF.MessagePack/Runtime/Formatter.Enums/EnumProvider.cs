using System;

namespace UGF.MessagePack.Runtime.Formatter.Enums
{
    public class EnumProvider : MessagePackProvider
    {
        public IMessagePackProvider Provider { get; }
        public IMessagePackContext Context { get; }

        public EnumProvider(IMessagePackProvider provider, IMessagePackContext context)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override bool TryGet<T>(out IMessagePackFormatter<T> formatter)
        {
            Type type = typeof(T);

            if (type.IsEnum)
            {
                if (!base.TryGet(out formatter))
                {
                    Type underlyingType = type.GetEnumUnderlyingType();
                    TypeCode typeCode = Type.GetTypeCode(underlyingType);

                    switch (typeCode)
                    {
                        case TypeCode.Byte:
                        {
                            formatter = new EnumFormatterByte<T>(Provider, Context);
                            break;
                        }
                        case TypeCode.Int16:
                        {
                            formatter = new EnumFormatterInt16<T>(Provider, Context);
                            break;
                        }
                        case TypeCode.Int32:
                        {
                            formatter = new EnumFormatterInt32<T>(Provider, Context);
                            break;
                        }
                        case TypeCode.Int64:
                        {
                            formatter = new EnumFormatterInt64<T>(Provider, Context);
                            break;
                        }
                        case TypeCode.SByte:
                        {
                            formatter = new EnumFormatterSByte<T>(Provider, Context);
                            break;
                        }
                        case TypeCode.UInt16:
                        {
                            formatter = new EnumFormatterUInt16<T>(Provider, Context);
                            break;
                        }
                        case TypeCode.UInt32:
                        {
                            formatter = new EnumFormatterUInt32<T>(Provider, Context);
                            break;
                        }
                        case TypeCode.UInt64:
                        {
                            formatter = new EnumFormatterUInt64<T>(Provider, Context);
                            break;
                        }
                        default: throw new ArgumentOutOfRangeException(nameof(type), $"The specified enum underlying type not supported: '{underlyingType}.'");
                    }

                    Formatters.Add(type, formatter);
                }

                return true;
            }

            formatter = null;
            return false;
        }

        public override bool TryGet(Type type, out IMessagePackFormatter formatter)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (type.IsEnum)
            {
                if (!base.TryGet(type, out formatter))
                {
                    Type underlyingType = type.GetEnumUnderlyingType();
                    TypeCode typeCode = Type.GetTypeCode(underlyingType);
                    Type formatterType;

                    switch (typeCode)
                    {
                        case TypeCode.Byte:
                        {
                            formatterType = typeof(EnumFormatterByte<>).MakeGenericType(type);
                            break;
                        }
                        case TypeCode.Int16:
                        {
                            formatterType = typeof(EnumFormatterInt16<>).MakeGenericType(type);
                            break;
                        }
                        case TypeCode.Int32:
                        {
                            formatterType = typeof(EnumFormatterInt32<>).MakeGenericType(type);
                            break;
                        }
                        case TypeCode.Int64:
                        {
                            formatterType = typeof(EnumFormatterInt64<>).MakeGenericType(type);
                            break;
                        }
                        case TypeCode.SByte:
                        {
                            formatterType = typeof(EnumFormatterSByte<>).MakeGenericType(type);
                            break;
                        }
                        case TypeCode.UInt16:
                        {
                            formatterType = typeof(EnumFormatterUInt16<>).MakeGenericType(type);
                            break;
                        }
                        case TypeCode.UInt32:
                        {
                            formatterType = typeof(EnumFormatterUInt32<>).MakeGenericType(type);
                            break;
                        }
                        case TypeCode.UInt64:
                        {
                            formatterType = typeof(EnumFormatterUInt64<>).MakeGenericType(type);
                            break;
                        }
                        default: throw new ArgumentOutOfRangeException(nameof(type), $"The specified enum underlying type not supported: '{underlyingType}.'");
                    }

                    formatter = (IMessagePackFormatter)Activator.CreateInstance(formatterType, Provider, Context);

                    Formatters.Add(type, formatter);
                }

                return true;
            }

            formatter = null;
            return false;
        }
    }
}
