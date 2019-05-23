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

        public override bool TryGet<T>(out IMessagePackFormatter<T> formatter)
        {
            if (!base.TryGet(out formatter))
            {
                global::MessagePack.Formatters.IMessagePackFormatter<T> formatterInner = Resolver.GetFormatter<T>();

                if (formatterInner != null)
                {
                    formatter = new MessagePackFormatterWrapper<T>(this, Context, formatterInner, Resolver);

                    Formatters.Add(typeof(T), formatter);

                    return true;
                }
            }

            return formatter != null;
        }

        public override bool TryGet(Type type, out IMessagePackFormatter formatter)
        {
            if (!base.TryGet(type, out formatter))
            {
                throw new NotSupportedException($"Generate formatter wrapper in non-generic way not supported: '{type}'.");
            }

            return formatter != null;
        }
    }
}
