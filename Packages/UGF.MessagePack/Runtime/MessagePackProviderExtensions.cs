using System;

namespace UGF.MessagePack.Runtime
{
    public static class MessagePackProviderExtensions
    {
        public static IMessagePackFormatter<T> Get<T>(this IMessagePackProvider provider)
        {
            if (!provider.TryGet(out IMessagePackFormatter<T> formatter))
            {
                throw new ArgumentException($"The formatter for the specified target type not found: '{typeof(T)}'.");
            }

            return formatter;
        }

        public static IMessagePackFormatter Get(this IMessagePackProvider provider, Type type)
        {
            if (!provider.TryGet(type, out IMessagePackFormatter formatter))
            {
                throw new ArgumentException($"The formatter for the specified target type not found: '{type}'.");
            }

            return formatter;
        }
    }
}
