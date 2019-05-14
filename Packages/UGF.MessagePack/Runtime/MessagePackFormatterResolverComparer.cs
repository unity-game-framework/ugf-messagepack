using System.Collections.Generic;
using System.Reflection;
using MessagePack;

namespace UGF.MessagePack.Runtime
{
    public sealed class MessagePackFormatterResolverComparer : IComparer<IFormatterResolver>
    {
        public int Compare(IFormatterResolver x, IFormatterResolver y)
        {
            var xAttribute = x?.GetType().GetCustomAttribute<MessagePackFormatterResolverAttribute>();
            var yAttribute = y?.GetType().GetCustomAttribute<MessagePackFormatterResolverAttribute>();

            int xPriority = xAttribute?.Priority ?? 0;
            int yPriority = yAttribute?.Priority ?? 0;

            return xPriority.CompareTo(yPriority);
        }
    }
}
