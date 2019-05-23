using System;
using System.Text;
using MessagePack;

namespace UGF.MessagePack.Runtime
{
    public struct MessagePackWriter
    {
        public byte[] Buffer;
        public int Position;

        public MessagePackWriter(byte[] buffer = null, int position = 0)
        {
            if (position < 0) throw new ArgumentException("Position can not be less than zero.");

            Buffer = buffer;
            Position = position;
        }

        public MessagePackWriter GetWrite(int positionOffset = 0)
        {
            return new MessagePackWriter(Buffer, Position + positionOffset);
        }

        public void WriteNil()
        {
            Position += MessagePackBinary.WriteNil(ref Buffer, Position);
        }

        public void WriteBoolean(bool value)
        {
            Position += MessagePackBinary.WriteBoolean(ref Buffer, Position, value);
        }

        public void WriteByte(byte value)
        {
            Position += MessagePackBinary.WriteByte(ref Buffer, Position, value);
        }

        public void WriteSByte(sbyte value)
        {
            Position += MessagePackBinary.WriteSByte(ref Buffer, Position, value);
        }

        public void WriteInt16(short value)
        {
            Position += MessagePackBinary.WriteInt16(ref Buffer, Position, value);
        }

        public void WriteUInt16(ushort value)
        {
            Position += MessagePackBinary.WriteUInt16(ref Buffer, Position, value);
        }

        public void WriteInt32(int value)
        {
            Position += MessagePackBinary.WriteInt32(ref Buffer, Position, value);
        }

        public void WriteUInt32(uint value)
        {
            Position += MessagePackBinary.WriteUInt32(ref Buffer, Position, value);
        }

        public void WriteInt64(long value)
        {
            Position += MessagePackBinary.WriteInt64(ref Buffer, Position, value);
        }

        public void WriteUInt64(ulong value)
        {
            Position += MessagePackBinary.WriteUInt64(ref Buffer, Position, value);
        }

        public void WriteSingle(float value)
        {
            Position += MessagePackBinary.WriteSingle(ref Buffer, Position, value);
        }

        public void WriteDouble(double value)
        {
            Position += MessagePackBinary.WriteDouble(ref Buffer, Position, value);
        }

        public void WriteChar(char value)
        {
            Position += MessagePackBinary.WriteChar(ref Buffer, Position, value);
        }

        public void WriteString(string value)
        {
            Position += MessagePackBinary.WriteString(ref Buffer, Position, value);
        }

        public void WriteStringBytes(byte[] value)
        {
            Position += MessagePackBinary.WriteStringBytes(ref Buffer, Position, value);
        }

        public void WriteStringUnsafe(string value)
        {
            Position += MessagePackBinary.WriteStringUnsafe(ref Buffer, Position, value, Encoding.UTF8.GetByteCount(value));
        }

        public void WriteStringUnsafe(string value, int byteCount)
        {
            Position += MessagePackBinary.WriteStringUnsafe(ref Buffer, Position, value, byteCount);
        }

        public void WriteStringUnsafeFixed(string value, int byteCount)
        {
            Position += MessagePackBinary.WriteFixedStringUnsafe(ref Buffer, Position, value, byteCount);
        }

        public void WriteStringForceStr32Block(string value)
        {
            Position += MessagePackBinary.WriteStringForceStr32Block(ref Buffer, Position, value);
        }

        public void WriteDateTime(DateTime dateTime)
        {
            Position += MessagePackBinary.WriteDateTime(ref Buffer, Position, dateTime);
        }

        public void WriteBytes(byte[] value)
        {
            Position += MessagePackBinary.WriteBytes(ref Buffer, Position, value);
        }
    }
}
