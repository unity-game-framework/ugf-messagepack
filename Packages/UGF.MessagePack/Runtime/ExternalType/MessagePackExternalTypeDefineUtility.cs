using System;
using System.Collections.Generic;
using System.Reflection;
using MessagePack.Formatters;
using UGF.Assemblies.Runtime;
using UGF.Types.Runtime;

namespace UGF.MessagePack.Runtime.ExternalType
{
    public static class MessagePackExternalTypeDefineUtility
    {
        public static void GetFormatters(IDictionary<Type, IMessagePackFormatter> formatters, Assembly assembly = null)
        {
            if (formatters == null) throw new ArgumentNullException(nameof(formatters));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<MessagePackExternalTypeDefineAttribute>(assembly))
            {
                if (TypesUtility.TryCreateType(type, out IMessagePackExternalTypeDefine define))
                {
                    define.GetFormatters(formatters);
                }
            }
        }

        public static void GetDefines(ICollection<IMessagePackExternalTypeDefine> defines, Assembly assembly = null)
        {
            if (defines == null) throw new ArgumentNullException(nameof(defines));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<MessagePackExternalTypeDefineAttribute>(assembly))
            {
                if (TypesUtility.TryCreateType(type, out IMessagePackExternalTypeDefine define))
                {
                    defines.Add(define);
                }
            }
        }
    }
}
