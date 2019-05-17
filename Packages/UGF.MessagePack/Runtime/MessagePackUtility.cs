using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Assemblies.Runtime;
using UGF.Types.Runtime;

namespace UGF.MessagePack.Runtime
{
    public static class MessagePackUtility
    {
        public static IMessagePackProvider CreateProvider(Assembly assembly = null)
        {
            var provider = new MessagePackProviderComposite();

            GetFormatters(provider, provider.Formatters, assembly);
            GetProviders(provider.Providers, assembly);

            provider.Providers.Sort(MessagePackProviderByAttributeComparer.Default);

            return provider;
        }

        public static void GetFormatters(IMessagePackProvider provider, IDictionary<Type, IMessagePackFormatter> formatters, Assembly assembly = null)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (formatters == null) throw new ArgumentNullException(nameof(formatters));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<MessagePackFormatterAttribute>(assembly))
            {
                throw new NotImplementedException();
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
