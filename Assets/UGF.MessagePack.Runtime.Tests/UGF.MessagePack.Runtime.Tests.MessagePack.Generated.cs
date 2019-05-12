// THIS IS A GENERATED CODE. DO NOT EDIT.
// ReSharper disable all

using System.Collections.Generic;
using MessagePack;
using NUnit.Framework;
using UnityEngine;
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace UGF.MessagePack.Runtime.Tests.Resolvers
{
    using System;
    using System.Buffers;
    using MessagePack;

    public class Resolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new Resolver();

        Resolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = ResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class ResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static ResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(6)
            {
                {typeof(global::System.Collections.Generic.List<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target>), 0 },
                {typeof(global::UnityEngine.HideFlags), 1 },
                {typeof(global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion), 2 },
                {typeof(global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target), 3 },
                {typeof(global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target2), 4 },
                {typeof(global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target3), 5 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.ListFormatter<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target>();
                case 1: return new UGF.MessagePack.Runtime.Tests.Formatters.UnityEngine.HideFlagsFormatter();
                case 2: return new UGF.MessagePack.Runtime.Tests.Formatters.UGF.MessagePack.Runtime.Tests.ITargetUnionFormatter();
                case 3: return new UGF.MessagePack.Runtime.Tests.Formatters.UGF.MessagePack.Runtime.Tests.TestSerialization_TargetFormatter();
                case 4: return new UGF.MessagePack.Runtime.Tests.Formatters.UGF.MessagePack.Runtime.Tests.TestSerialization_Target2Formatter();
                case 5: return new UGF.MessagePack.Runtime.Tests.Formatters.UGF.MessagePack.Runtime.Tests.TestSerialization_Target3Formatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace UGF.MessagePack.Runtime.Tests.Formatters.UnityEngine
{
    using System;
	using System.Buffers;
    using MessagePack;

    [global::UGF.MessagePack.Runtime.MessagePackFormatterAttribute(typeof(global::UnityEngine.HideFlags))]
    public sealed class HideFlagsFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::UnityEngine.HideFlags>
    {
        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::UnityEngine.HideFlags value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            writer.Write((Int32)value);
        }
        
        public global::UnityEngine.HideFlags Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.IFormatterResolver formatterResolver)
        {
            return (global::UnityEngine.HideFlags)reader.ReadInt32();
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace UGF.MessagePack.Runtime.Tests.Formatters.UGF.MessagePack.Runtime.Tests
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using MessagePack;

    [global::UGF.MessagePack.Runtime.MessagePackFormatterAttribute(typeof(global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion))]
    public sealed class ITargetUnionFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion>
    {
        readonly Dictionary<RuntimeTypeHandle, KeyValuePair<int, int>> typeToKeyAndJumpMap;
        readonly Dictionary<int, int> keyToJumpMap;

        public ITargetUnionFormatter()
        {
            this.typeToKeyAndJumpMap = new Dictionary<RuntimeTypeHandle, KeyValuePair<int, int>>(2, global::MessagePack.Internal.RuntimeTypeHandleEqualityComparer.Default)
            {
                { typeof(global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target).TypeHandle, new KeyValuePair<int, int>(0, 0) },
                { typeof(global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target2).TypeHandle, new KeyValuePair<int, int>(1, 1) },
            };
            this.keyToJumpMap = new Dictionary<int, int>(2)
            {
                { 0, 0 },
                { 1, 1 },
            };
        }

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            KeyValuePair<int, int> keyValuePair;
            if (value != null && this.typeToKeyAndJumpMap.TryGetValue(value.GetType().TypeHandle, out keyValuePair))
            {
                writer.WriteFixedArrayHeaderUnsafe(2);
                writer.WriteInt32(keyValuePair.Key);
                switch (keyValuePair.Value)
                {
                    case 0:
                        formatterResolver.GetFormatterWithVerify<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target>().Serialize(ref writer, (global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target)value, formatterResolver);
                        break;
                    case 1:
                        formatterResolver.GetFormatterWithVerify<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target2>().Serialize(ref writer, (global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target2)value, formatterResolver);
                        break;
                    default:
                        break;
                }

                return;
            }

            writer.WriteNil();
        }

        public global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            if (reader.ReadArrayHeader() != 2)
            {
                throw new InvalidOperationException("Invalid Union data was detected. Type:global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion");
            }

            var key = reader.ReadInt32();

            if (!this.keyToJumpMap.TryGetValue(key, out key))
            {
                key = -1;
            }

            global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion result = null;
            switch (key)
            {
                case 0:
                    result = (global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion)formatterResolver.GetFormatterWithVerify<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target>().Deserialize(ref reader, formatterResolver);
                    break;
                case 1:
                    result = (global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion)formatterResolver.GetFormatterWithVerify<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target2>().Deserialize(ref reader, formatterResolver);
                    break;
                default:
                    reader.Skip();
                    break;
            }

            return result;
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612


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


    [global::UGF.MessagePack.Runtime.MessagePackFormatterAttribute(typeof(global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target2))]
    public sealed class TestSerialization_Target2Formatter : global::MessagePack.Formatters.IMessagePackFormatter<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target2>
    {

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target2 value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.WriteFixedArrayHeaderUnsafe(5);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Name, formatterResolver);
            formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector2>().Serialize(ref writer, value.Vector2, formatterResolver);
            formatterResolver.GetFormatterWithVerify<global::UnityEngine.Bounds>().Serialize(ref writer, value.Bounds, formatterResolver);
            formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target>>().Serialize(ref writer, value.Targets, formatterResolver);
            formatterResolver.GetFormatterWithVerify<global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion>().Serialize(ref writer, value.Union, formatterResolver);
        }

        public global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target2 Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            var length = reader.ReadArrayHeader();

            var __Name__ = default(string);
            var __Vector2__ = default(global::UnityEngine.Vector2);
            var __Bounds__ = default(global::UnityEngine.Bounds);
            var __Targets__ = default(global::System.Collections.Generic.List<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target>);
            var __Union__ = default(global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Name__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        __Vector2__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector2>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 2:
                        __Bounds__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Bounds>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 3:
                        __Targets__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target>>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 4:
                        __Union__ = formatterResolver.GetFormatterWithVerify<global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            var ____result = new global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target2();
            ____result.Name = __Name__;
            ____result.Vector2 = __Vector2__;
            ____result.Bounds = __Bounds__;
            ____result.Targets = __Targets__;
            ____result.Union = __Union__;
            return ____result;
        }
    }


    [global::UGF.MessagePack.Runtime.MessagePackFormatterAttribute(typeof(global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target3))]
    public sealed class TestSerialization_Target3Formatter : global::MessagePack.Formatters.IMessagePackFormatter<global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target3>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestSerialization_Target3Formatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Name", 0},
                { "BoolValue", 1},
                { "FloatValue", 2},
                { "IntValue", 3},
                { "Flags", 4},
                { "Union", 5},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("Name"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("BoolValue"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("FloatValue"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("IntValue"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("Flags"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("Union"),
            };
        }


        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target3 value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.WriteMapHeader(6);
            writer.WriteRaw(this.____stringByteKeys[0]);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Name, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.Write(value.BoolValue);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.Write(value.FloatValue);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.Write(value.IntValue);
            writer.WriteRaw(this.____stringByteKeys[4]);
            formatterResolver.GetFormatterWithVerify<global::UnityEngine.HideFlags>().Serialize(ref writer, value.Flags, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[5]);
            formatterResolver.GetFormatterWithVerify<global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion>().Serialize(ref writer, value.Union, formatterResolver);
        }

        public global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target3 Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            var length = reader.ReadMapHeader();

            var __Name__ = default(string);
            var __BoolValue__ = default(bool);
            var __FloatValue__ = default(float);
            var __IntValue__ = default(int);
            var __Flags__ = default(global::UnityEngine.HideFlags);
            var __Union__ = default(global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion);

            for (int i = 0; i < length; i++)
            {
                var stringKey = reader.ReadStringSegment();
                int key;
                if (!____keyMapping.TryGetValue(stringKey, out key))
                {{
                    reader.Skip();
                    continue;
                }}

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
                    case 5:
                        __Union__ = formatterResolver.GetFormatterWithVerify<global::UGF.MessagePack.Runtime.Tests.TestSerialization.ITargetUnion>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            var ____result = new global::UGF.MessagePack.Runtime.Tests.TestSerialization.Target3();
            ____result.Name = __Name__;
            ____result.BoolValue = __BoolValue__;
            ____result.FloatValue = __FloatValue__;
            ____result.IntValue = __IntValue__;
            ____result.Flags = __Flags__;
            ____result.Union = __Union__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

