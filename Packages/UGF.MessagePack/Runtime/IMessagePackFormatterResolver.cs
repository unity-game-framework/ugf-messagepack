using System;
using System.Collections.Generic;
using MessagePack;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackFormatterResolver : IFormatterResolver
    {
        IReadOnlyDictionary<Type, IMessagePackFormatter> Formatters { get; }
        IReadOnlyList<IFormatterResolver> Resolvers { get; }
    }
}
