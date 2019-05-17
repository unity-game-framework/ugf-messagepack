using System;
using System.Collections.Generic;

namespace UGF.MessagePack.Runtime
{
    public class MessagePackProviderComposite : MessagePackProvider
    {
        public List<IMessagePackProvider> Providers { get; } = new List<IMessagePackProvider>();

        public override IMessagePackFormatter Get(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!Formatters.TryGetValue(type, out IMessagePackFormatter formatter))
            {
                for (int i = 0; i < Providers.Count; i++)
                {
                    formatter = Providers[i].Get(type);

                    if (formatter != null)
                    {
                        return formatter;
                    }
                }
            }

            return formatter;
        }

        public override IMessagePackFormatter GetOrCreate(Type type, MessagePackFormatterCreateHandler handler)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            IMessagePackFormatter formatter = Get(type);

            if (formatter == null)
            {
                formatter = handler();

                Add(formatter);
            }

            return formatter;
        }
    }
}
