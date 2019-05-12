// THIS IS A GENERATED CODE. DO NOT EDIT.
// ReSharper disable all

using MessagePack;
using NUnit.Framework;
using UnityEngine;
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace UGF.MessagePack.Runtime.Tests.Formatters.UGF.MessagePack.Runtime.Tests
{
    using System;
	using System.Buffers;
    using MessagePack;


    [global::UGF.MessagePack.Runtime.MessagePackFormatterAttribute(typeof(global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target))]
    public sealed class TestSerialization_TargetFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target>
    {

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.WriteFixedArrayHeaderUnsafe(5);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Name, formatterResolver);
            writer.Write(value.BoolValue);
            writer.Write(value.FloatValue);
            writer.Write(value.IntValue);
            formatterResolver.GetFormatterWithVerify<global::UnityEngine.HideFlags>().Serialize(ref writer, value.Flags, formatterResolver);
        }

        public global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            var length = reader.ReadArrayHeader();

            var __Name__ = default(string);
            var __BoolValue__ = default(bool);
            var __FloatValue__ = default(float);
            var __IntValue__ = default(int);
            var __Flags__ = default(global::UnityEngine.HideFlags);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Name__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        __BoolValue__ = reader.ReadBoolean();
                        break;
                    case 2:
                        __FloatValue__ = reader.ReadSingle();
                        break;
                    case 3:
                        __IntValue__ = reader.ReadInt32();
                        break;
                    case 4:
                        __Flags__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.HideFlags>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            var ____result = new global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target();
            ____result.Name = __Name__;
            ____result.BoolValue = __BoolValue__;
            ____result.FloatValue = __FloatValue__;
            ____result.IntValue = __IntValue__;
            ____result.Flags = __Flags__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

