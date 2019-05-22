using System;
using MessagePack;

namespace UGF.MessagePack.Runtime
{
    public class MessagePackProviderWrapper : MessagePackProvider
    {
        public IMessagePackContext Context { get; }
        public IFormatterResolver Resolver { get; }

        public MessagePackProviderWrapper(IMessagePackContext context, IFormatterResolver resolver)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public override IMessagePackFormatter<T> Get<T>()
        {
            IMessagePackFormatter<T> formatter = base.Get<T>();

            if (formatter == null)
            {
                global::MessagePack.Formatters.IMessagePackFormatter<T> formatterInner = Resolver.GetFormatter<T>();

                if (formatterInner != null)
                {
                    formatter = new MessagePackFormatterWrapper<T>(this, Context, formatterInner, Resolver);

                    Formatters.Add(typeof(T), formatter);
                }
            }

            return formatter;
        }
    }
}
