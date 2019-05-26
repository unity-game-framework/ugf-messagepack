using System;
using System.Collections.Generic;
using System.Reflection;
using MessagePack.Resolvers;
using UGF.Assemblies.Runtime;
using UGF.MessagePack.Runtime.Formatter.Enums;
using UGF.Types.Runtime;

namespace UGF.MessagePack.Runtime
{
    public static class MessagePackUtility
    {
        public static IMessagePackProvider CreateProvider(IMessagePackContext context = null, MessagePackFormatterType type = MessagePackFormatterType.Unknown, Assembly assembly = null)
        {
            return CreateProvider(context ?? MessagePackContext.Empty, (int)type, assembly);
        }

        public static IMessagePackProvider CreateProvider(IMessagePackContext context, int type, Assembly assembly = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var provider = new MessagePackProviderComposite();

            GetFormatters(provider, context, provider.Formatters, type, assembly);
            GetProviders(provider.Providers, type, assembly);

            provider.Providers.Sort(MessagePackProviderByAttributeComparer.Default);

            provider.Providers.Add(new EnumProvider(context));
            provider.Providers.Add(new MessagePackProviderWrapper(context, BuiltinResolver.Instance));
            provider.Providers.Add(new MessagePackProviderWrapper(context, DynamicGenericResolver.Instance));

            provider.Initialize();

            return provider;
        }

        public static void GetFormatters(IMessagePackProvider provider, IMessagePackContext context, IDictionary<Type, IMessagePackFormatter> formatters, MessagePackFormatterType formatterType = MessagePackFormatterType.Unknown, Assembly assembly = null)
        {
            GetFormatters(provider, context ?? MessagePackContext.Empty, formatters, (int)formatterType, assembly);
        }

        public static void GetFormatters(IMessagePackProvider provider, IMessagePackContext context, IDictionary<Type, IMessagePackFormatter> formatters, int formatterType, Assembly assembly = null)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (formatters == null) throw new ArgumentNullException(nameof(formatters));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<MessagePackFormatterAttribute>(assembly))
            {
                var attribute = type.GetCustomAttribute<MessagePackFormatterAttribute>(false);

                if (attribute.Type == formatterType && TypesUtility.TryCreateType(type, new object[] { provider, context }, out IMessagePackFormatter formatter))
                {
                    formatters.Add(formatter.TargetType, formatter);
                }
            }
        }

        public static void GetProviders(ICollection<IMessagePackProvider> providers, MessagePackFormatterType formatterType = MessagePackFormatterType.Unknown, Assembly assembly = null)
        {
            GetProviders(providers, (int)formatterType, assembly);
        }

        public static void GetProviders(ICollection<IMessagePackProvider> providers, int formatterType, Assembly assembly = null)
        {
            if (providers == null) throw new ArgumentNullException(nameof(providers));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<MessagePackProviderAttribute>(assembly))
            {
                var attribute = type.GetCustomAttribute<MessagePackProviderAttribute>(false);

                if (attribute.Type == formatterType && TypesUtility.TryCreateType(type, out IMessagePackProvider provider))
                {
                    providers.Add(provider);
                }
            }
        }
    }
}
