// THIS IS A GENERATED CODE. DO NOT EDIT.
// ReSharper disable all

using MessagePack;
using UnityEngine;
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace TestAssembly.Formatters.UGF.MessagePack.Runtime.Tests.TestAssembly
{
    using System;
	using System.Buffers;
    using MessagePack;


    public sealed class TestTargetFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::UGF.MessagePack.Runtime.Tests.TestAssembly.TestTarget>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTargetFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Name", 0},
                { "BoolValue", 1},
                { "FloatValue", 2},
                { "IntValue", 3},
                { "Vector2", 4},
                { "Bounds", 5},
                { "Flags", 6},
                { "VirtualProperty", 7},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("Name"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("BoolValue"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("FloatValue"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("IntValue"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("Vector2"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("Bounds"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("Flags"),
                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes("VirtualProperty"),
            };
        }


        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::UGF.MessagePack.Runtime.Tests.TestAssembly.TestTarget value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.WriteMapHeader(8);
            writer.WriteRaw(this.____stringByteKeys[0]);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Name, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.Write(value.BoolValue);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.Write(value.FloatValue);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.Write(value.IntValue);
            writer.WriteRaw(this.____stringByteKeys[4]);
            formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector2>().Serialize(ref writer, value.Vector2, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[5]);
            formatterResolver.GetFormatterWithVerify<global::UnityEngine.Bounds>().Serialize(ref writer, value.Bounds, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[6]);
            formatterResolver.GetFormatterWithVerify<global::UnityEngine.HideFlags>().Serialize(ref writer, value.Flags, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[7]);
            writer.Write(value.VirtualProperty);
        }

        public global::UGF.MessagePack.Runtime.Tests.TestAssembly.TestTarget Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.IFormatterResolver formatterResolver)
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
            var __Vector2__ = default(global::UnityEngine.Vector2);
            var __Bounds__ = default(global::UnityEngine.Bounds);
            var __Flags__ = default(global::UnityEngine.HideFlags);
            var __VirtualProperty__ = default(int);

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
                        __Vector2__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector2>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 5:
                        __Bounds__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Bounds>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 6:
                        __Flags__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.HideFlags>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 7:
                        __VirtualProperty__ = reader.ReadInt32();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            var ____result = new global::UGF.MessagePack.Runtime.Tests.TestAssembly.TestTarget();
            ____result.Name = __Name__;
            ____result.BoolValue = __BoolValue__;
            ____result.FloatValue = __FloatValue__;
            ____result.IntValue = __IntValue__;
            ____result.Vector2 = __Vector2__;
            ____result.Bounds = __Bounds__;
            ____result.Flags = __Flags__;
            ____result.VirtualProperty = __VirtualProperty__;
            return ____result;
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

namespace TestAssembly.Formatters.UnityEngine.AI
{
    using System;
	using System.Buffers;
    using MessagePack;


    public sealed class NavMeshBuildSettingsFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::UnityEngine.AI.NavMeshBuildSettings>
    {

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::UnityEngine.AI.NavMeshBuildSettings value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            writer.WriteArrayHeader(21);
            writer.Write(value.agentTypeID);
            writer.Write(value.agentRadius);
            writer.Write(value.agentHeight);
            writer.Write(value.agentSlope);
            writer.Write(value.agentClimb);
            writer.Write(value.minRegionArea);
            writer.Write(value.overrideVoxelSize);
            writer.Write(value.voxelSize);
            writer.Write(value.overrideTileSize);
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.Write(value.tileSize);
        }

        public global::UnityEngine.AI.NavMeshBuildSettings Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (reader.TryReadNil())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }

            var length = reader.ReadArrayHeader();

            var __agentTypeID__ = default(int);
            var __agentRadius__ = default(float);
            var __agentHeight__ = default(float);
            var __agentSlope__ = default(float);
            var __agentClimb__ = default(float);
            var __minRegionArea__ = default(float);
            var __overrideVoxelSize__ = default(bool);
            var __voxelSize__ = default(float);
            var __overrideTileSize__ = default(bool);
            var __tileSize__ = default(int);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __agentTypeID__ = reader.ReadInt32();
                        break;
                    case 1:
                        __agentRadius__ = reader.ReadSingle();
                        break;
                    case 2:
                        __agentHeight__ = reader.ReadSingle();
                        break;
                    case 3:
                        __agentSlope__ = reader.ReadSingle();
                        break;
                    case 4:
                        __agentClimb__ = reader.ReadSingle();
                        break;
                    case 5:
                        __minRegionArea__ = reader.ReadSingle();
                        break;
                    case 6:
                        __overrideVoxelSize__ = reader.ReadBoolean();
                        break;
                    case 7:
                        __voxelSize__ = reader.ReadSingle();
                        break;
                    case 8:
                        __overrideTileSize__ = reader.ReadBoolean();
                        break;
                    case 20:
                        __tileSize__ = reader.ReadInt32();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            var ____result = new global::UnityEngine.AI.NavMeshBuildSettings();
            ____result.agentTypeID = __agentTypeID__;
            ____result.agentRadius = __agentRadius__;
            ____result.agentHeight = __agentHeight__;
            ____result.agentSlope = __agentSlope__;
            ____result.agentClimb = __agentClimb__;
            ____result.minRegionArea = __minRegionArea__;
            ____result.overrideVoxelSize = __overrideVoxelSize__;
            ____result.voxelSize = __voxelSize__;
            ____result.overrideTileSize = __overrideTileSize__;
            ____result.tileSize = __tileSize__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

