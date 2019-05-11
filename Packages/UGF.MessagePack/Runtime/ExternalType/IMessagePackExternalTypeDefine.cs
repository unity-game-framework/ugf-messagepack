using System;
using System.Collections.Generic;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime.ExternalType
{
    public interface IMessagePackExternalTypeDefine
    {
        void GetFormatters(IDictionary<Type, IMessagePackFormatter> formatters);
    }
}
