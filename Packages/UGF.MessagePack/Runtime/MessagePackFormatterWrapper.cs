using System;
using MessagePack;

namespace UGF.MessagePack.Runtime
{
    public class MessagePackFormatterWrapper<T> : MessagePackFormatterBase<T>
    {
        public global::MessagePack.Formatters.IMessagePackFormatter<T> Formatter { get; }
        public IFormatterResolver Resolver { get; }

        public MessagePackFormatterWrapper(IMessagePackProvider provider, IMessagePackContext context, global::MessagePack.Formatters.IMessagePackFormatter<T> formatter, IFormatterResolver resolver) : base(provider, context)
        {
            Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public override void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.Position += Formatter.Serialize(ref writer.Buffer, writer.Position, value, Resolver);
        }

        public override T Deserialize(ref MessagePackReader reader)
        {
            T value = Formatter.Deserialize(reader.Buffer, reader.Position, Resolver, out int readSize);

            reader.Position += readSize;

            return value;
        }
    }
}
