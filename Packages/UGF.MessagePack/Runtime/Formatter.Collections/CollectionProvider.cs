using System;
using System.Collections.Generic;

namespace UGF.MessagePack.Runtime.Formatter.Collections
{
    public class CollectionProvider : MessagePackProvider
    {
        public IMessagePackProvider Provider { get; }
        public IMessagePackContext Context { get; }

        public CollectionProvider(IMessagePackProvider provider, IMessagePackContext context)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override bool TryGet<T>(out IMessagePackFormatter<T> formatter)
        {
            if (TryGet(typeof(T), out IMessagePackFormatter formatterBase))
            {
                formatter = (IMessagePackFormatter<T>)formatterBase;
                return true;
            }

            formatter = null;
            return false;
        }

        public override bool TryGet(Type type, out IMessagePackFormatter formatter)
        {
            if (!base.TryGet(type, out formatter))
            {
                Type formatterType = null;

                switch (type)
                {
                    case Type collectionType when collectionType.IsArray:
                    {
                        formatterType = typeof(CollectionFormatterArray<>).MakeGenericType(collectionType.GetElementType());
                        break;
                    }
                    case Type collectionType when collectionType.IsConstructedGenericType && collectionType.GetGenericTypeDefinition() == typeof(List<>):
                    {
                        formatterType = typeof(CollectionFormatterList<>).MakeGenericType(collectionType.GenericTypeArguments[0]);
                        break;
                    }
                    case Type collectionType when collectionType.IsConstructedGenericType && collectionType.GetGenericTypeDefinition() == typeof(Dictionary<,>):
                    {
                        Type[] arguments = collectionType.GenericTypeArguments;

                        formatterType = typeof(CollectionFormatterDictionary<,>).MakeGenericType(arguments[0], arguments[1]);
                        break;
                    }
                }

                if (formatterType != null)
                {
                    formatter = (IMessagePackFormatter)Activator.CreateInstance(formatterType, Provider, Context);

                    Formatters.Add(type, formatter);
                }

                return formatter != null;
            }

            formatter = null;
            return false;
        }
    }
}
