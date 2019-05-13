using System;
using System.Collections.Generic;
using System.Reflection;
using MessagePack;
using MessagePack.Resolvers;
using MessagePack.Unity;
using MessagePack.Unity.Extension;
using UGF.Assemblies.Runtime;
using UGF.Types.Runtime;

namespace UGF.MessagePack.Runtime
{
    public static class MessagePackUtility
    {
        public static IComparer<IFormatterResolver> FormatterResolverComparer { get; } = new MessagePackFormatterResolverComparer();

        public static IFormatterResolver CreateDefaultResolver(Assembly assembly = null)
        {
            var resolver = new CompositeResolver();

            resolver.RegisterResolver(CreateResolver(assembly));
            resolver.RegisterResolver(UnityBlitWithPrimitiveArrayResolver.Instance);
            resolver.RegisterResolver(UnityResolver.Instance);
            resolver.RegisterResolver(BuiltinResolver.Instance);

            return resolver;
        }

        public static IFormatterResolver CreateResolver(Assembly assembly = null)
        {
            var resolver = new CompositeResolver();
            var resolvers = new List<IFormatterResolver>();

            GetResolvers(resolvers, assembly);

            resolvers.Sort(FormatterResolverComparer);

            for (int i = 0; i < resolvers.Count; i++)
            {
                resolver.RegisterResolver(resolvers[i]);
            }

            return resolver;
        }

        public static void GetResolvers(ICollection<IFormatterResolver> resolvers, Assembly assembly = null)
        {
            if (resolvers == null) throw new ArgumentNullException(nameof(resolvers));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<MessagePackFormatterResolverAttribute>(assembly))
            {
                if (TypesUtility.TryCreateType(type, out IFormatterResolver resolver))
                {
                    resolvers.Add(resolver);
                }
            }
        }
    }
}
