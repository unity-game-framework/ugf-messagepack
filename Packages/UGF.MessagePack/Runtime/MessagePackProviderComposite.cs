using System;
using System.Collections.Generic;

namespace UGF.MessagePack.Runtime
{
    public class MessagePackProviderComposite : MessagePackProvider
    {
        public List<IMessagePackProvider> Providers { get; } = new List<IMessagePackProvider>();

        public override bool TryGet<T>(out IMessagePackFormatter<T> formatter)
        {
            if (!base.TryGet(out formatter))
            {
                for (int i = 0; i < Providers.Count; i++)
                {
                    if (Providers[i].TryGet(out formatter))
                    {
                        return true;
                    }
                }
            }

            return formatter != null;
        }

        public override bool TryGet(Type type, out IMessagePackFormatter formatter)
        {
            if (!base.TryGet(type, out formatter))
            {
                for (int i = 0; i < Providers.Count; i++)
                {
                    if (Providers[i].TryGet(type, out formatter))
                    {
                        return true;
                    }
                }
            }

            return formatter != null;
        }
    }
}
