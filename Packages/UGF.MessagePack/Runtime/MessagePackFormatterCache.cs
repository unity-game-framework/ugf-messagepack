using JetBrains.Annotations;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime
{
    public static class MessagePackFormatterCache<T>
    {
        [CanBeNull] public static IMessagePackFormatter<T> Formatter;
    }
}
