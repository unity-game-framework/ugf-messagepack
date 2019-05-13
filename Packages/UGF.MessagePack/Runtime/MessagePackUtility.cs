﻿using System;
using System.Collections.Generic;
using System.Reflection;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using MessagePack.Unity;
using MessagePack.Unity.Extension;
using UGF.Assemblies.Runtime;
using UGF.MessagePack.Runtime.ExternalType;
using UGF.MessagePack.Runtime.Resolvers.Enums;
using UGF.Types.Runtime;

namespace UGF.MessagePack.Runtime
{
    public static class MessagePackUtility
    {
        public static MessagePackFormatterResolver CreateDefaultResolver(bool includeFormatters = true, bool includeExternalDefines = true, bool includeResolvers = true, Assembly assembly = null)
        {
            var resolver = new MessagePackFormatterResolver();

            SetupDefaultResolver(resolver, includeFormatters, includeExternalDefines, includeResolvers, assembly);

            return resolver;
        }

        public static void SetupDefaultResolver(IMessagePackFormatterResolver resolver, bool includeFormatters = true, bool includeExternalDefines = true, bool includeResolvers = true, Assembly assembly = null)
        {
            if (includeFormatters)
            {
                GetFormatters(resolver.Formatters, assembly);
            }

            if (includeExternalDefines)
            {
                MessagePackExternalTypeDefineUtility.GetFormatters(resolver.Formatters, assembly);
            }

            if (includeResolvers)
            {
                resolver.Resolvers.Add(UnityBlitWithPrimitiveArrayResolver.Instance);
                resolver.Resolvers.Add(UnityResolver.Instance);
                resolver.Resolvers.Add(new EnumResolver());
                resolver.Resolvers.Add(BuiltinResolver.Instance);
            }
        }

        public static void GetFormatters(IDictionary<Type, IMessagePackFormatter> formatters, Assembly assembly = null)
        {
            if (formatters == null) throw new ArgumentNullException(nameof(formatters));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<MessagePackFormatterAttribute>(assembly))
            {
                var attribute = type.GetCustomAttribute<MessagePackFormatterAttribute>();

                if (attribute != null && TypesUtility.TryCreateType(type, out IMessagePackFormatter formatter))
                {
                    formatters.Add(attribute.TargetType, formatter);
                }
            }
        }

        public static void SetupFormatterCache(IDictionary<Type, IMessagePackFormatter> formatters)
        {
            foreach (KeyValuePair<Type, IMessagePackFormatter> pair in formatters)
            {
                SetFormatterCache(pair.Key, pair.Value);
            }
        }

        public static void ClearFormatterCache(IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                SetFormatterCache(type, null);
            }
        }

        public static void SetFormatterCache(Type targetType, IMessagePackFormatter formatter)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            Type type = typeof(MessagePackFormatterCache<>).MakeGenericType(targetType);
            FieldInfo field = type.GetField("Formatter");

            field.SetValue(null, formatter);
        }

        public static bool TryGetFormatterCache(Type targetType, out IMessagePackFormatter formatter)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            Type type = typeof(MessagePackFormatterCache<>).MakeGenericType(targetType);
            FieldInfo field = type.GetField("Formatter");

            formatter = field.GetValue(null) as IMessagePackFormatter;

            return formatter != null;
        }
    }
}
