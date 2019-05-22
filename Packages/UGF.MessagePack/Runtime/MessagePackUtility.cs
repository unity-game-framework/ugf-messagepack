using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Assemblies.Runtime;
using UGF.Types.Runtime;

namespace UGF.MessagePack.Runtime
{
    public static class MessagePackUtility
    {
        public static IMessagePackProvider CreateProvider(IMessagePackContext context, Assembly assembly = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var provider = new MessagePackProviderComposite();

            GetFormatters(provider, context, provider.Formatters, assembly);
            GetProviders(provider.Providers, assembly);

            provider.Providers.Sort(MessagePackProviderByAttributeComparer.Default);

            return provider;
        }

        public static void GetFormatters(IMessagePackProvider provider, IMessagePackContext context, IDictionary<Type, IMessagePackFormatter> formatters, Assembly assembly = null)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (formatters == null) throw new ArgumentNullException(nameof(formatters));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<MessagePackFormatterAttribute>(assembly))
            {
                if (TypesUtility.TryCreateType(type, new object[] { provider, context }, out IMessagePackFormatter formatter))
                {
                    formatters.Add(type, formatter);
                }
            }
        }

        public static void GetProviders(ICollection<IMessagePackProvider> providers, Assembly assembly = null)
        {
            if (providers == null) throw new ArgumentNullException(nameof(providers));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<MessagePackProviderAttribute>(assembly))
            {
                if (TypesUtility.TryCreateType(type, out IMessagePackProvider provider))
                {
                    providers.Add(provider);
                }
            }
        }
    }
}
