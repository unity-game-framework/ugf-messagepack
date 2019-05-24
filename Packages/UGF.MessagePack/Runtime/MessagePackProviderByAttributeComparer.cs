using System.Collections.Generic;
using System.Reflection;

namespace UGF.MessagePack.Runtime
{
    public sealed class MessagePackProviderByAttributeComparer : IComparer<IMessagePackProvider>
    {
        public static IComparer<IMessagePackProvider> Default { get; } = new MessagePackProviderByAttributeComparer();

        public int Compare(IMessagePackProvider x, IMessagePackProvider y)
        {
            int xOrder = x?.GetType().GetCustomAttribute<MessagePackProviderAttribute>()?.Order ?? 0;
            int yOrder = y?.GetType().GetCustomAttribute<MessagePackProviderAttribute>()?.Order ?? 0;

            return xOrder.CompareTo(yOrder);
        }
    }
}
