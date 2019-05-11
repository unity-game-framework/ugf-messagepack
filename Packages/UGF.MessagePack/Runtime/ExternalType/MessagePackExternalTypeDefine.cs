using System;
using System.Collections.Generic;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime.ExternalType
{
    public class MessagePackExternalTypeDefine : IMessagePackExternalTypeDefine
    {
        public Dictionary<Type, IMessagePackFormatter> Formatters { get; } = new Dictionary<Type, IMessagePackFormatter>();

        public void GetFormatters(IDictionary<Type, IMessagePackFormatter> formatters)
        {
            if (formatters == null) throw new ArgumentNullException(nameof(formatters));

            foreach (KeyValuePair<Type, IMessagePackFormatter> pair in Formatters)
            {
                formatters.Add(pair.Key, pair.Value);
            }
        }
    }
}
