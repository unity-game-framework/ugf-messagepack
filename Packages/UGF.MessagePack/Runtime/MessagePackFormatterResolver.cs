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

        IDictionary<Type, IMessagePackFormatter> IMessagePackFormatterResolver.Formatters { get { return Formatters; } }
        IList<IFormatterResolver> IMessagePackFormatterResolver.Resolvers { get { return Resolvers; } }

        public virtual IMessagePackFormatter<T> GetFormatter<T>()
        {
            IMessagePackFormatter<T> formatter = MessagePackFormatterCache<T>.Formatter;

            if (formatter == null)
            {
                if (TryGetFormatterFromFormatters(out formatter) || TryGetFormatterFromResolvers(out formatter))
                {
                    MessagePackFormatterCache<T>.Formatter = formatter;
                }
            }

            return formatter;
        }

        protected virtual bool TryGetFormatterFromFormatters<T>(out IMessagePackFormatter<T> formatter)
        {
            if (Formatters.TryGetValue(typeof(T), out IMessagePackFormatter formatterBase) && formatterBase is IMessagePackFormatter<T> formatterGeneric)
            {
                formatter = formatterGeneric;
                return true;
            }

            formatter = null;
            return false;
        }

        protected virtual bool TryGetFormatterFromResolvers<T>(out IMessagePackFormatter<T> formatter)
        {
            for (int i = 0; i < Resolvers.Count; i++)
            {
                formatter = Resolvers[i].GetFormatter<T>();

                if (formatter != null)
                {
                    return true;
                }
            }

            formatter = null;
            return false;
        }
    }
}
