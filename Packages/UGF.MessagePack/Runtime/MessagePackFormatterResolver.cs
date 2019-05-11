using System;
using System.Collections.Generic;
using MessagePack;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime
{
    public class MessagePackFormatterResolver : IMessagePackFormatterResolver
    {
        public Dictionary<Type, IMessagePackFormatter> Formatters { get; } = new Dictionary<Type, IMessagePackFormatter>();
        public List<IFormatterResolver> Resolvers { get; } = new List<IFormatterResolver>();

        IReadOnlyDictionary<Type, IMessagePackFormatter> IMessagePackFormatterResolver.Formatters { get { return Formatters; } }
        IReadOnlyList<IFormatterResolver> IMessagePackFormatterResolver.Resolvers { get { return Resolvers; } }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            IMessagePackFormatter<T> formatter = MessagePackFormatterCache<T>.Formatter;

            if (formatter == null)
            {
                if (Formatters.TryGetValue(typeof(T), out IMessagePackFormatter formatterBase) && formatterBase is IMessagePackFormatter<T> formatterGeneric)
                {
                    formatter = formatterGeneric;

                    MessagePackFormatterCache<T>.Formatter = formatter;
                }
                else
                {
                    for (int i = 0; i < Resolvers.Count; i++)
                    {
                        formatter = Resolvers[i].GetFormatter<T>();

                        if (formatter != null)
                        {
                            MessagePackFormatterCache<T>.Formatter = formatter;
                            break;
                        }
                    }
                }
            }

            return formatter;
        }

        public void CacheFormatters()
        {
            foreach (KeyValuePair<Type, IMessagePackFormatter> pair in Formatters)
            {
                MessagePackUtility.SetFormatterCache(pair.Key, pair.Value);
            }
        }
    }
}
