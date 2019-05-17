using System.Collections.Generic;
using System.Reflection;

namespace UGF.MessagePack.Runtime
{
    public sealed class MessagePackProviderByAttributeComparer : IComparer<IMessagePackProvider>
    {
        public static IComparer<IMessagePackProvider> Default { get; } = new MessagePackProviderByAttributeComparer();

        public int Compare(IMessagePackProvider x, IMessagePackProvider y)
        {
            int xPriority = x?.GetType().GetCustomAttribute<MessagePackProviderAttribute>()?.Priority ?? 0;
            int yPriority = y?.GetType().GetCustomAttribute<MessagePackProviderAttribute>()?.Priority ?? 0;

            return xPriority.CompareTo(yPriority);
        }
    }
}
