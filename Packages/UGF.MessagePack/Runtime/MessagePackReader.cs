using System;
using System.Text;
using MessagePack;

namespace UGF.MessagePack.Runtime
{
    public struct MessagePackReader
    {
        public byte[] Buffer;
        public int Position;

        public MessagePackReader(byte[] buffer, int position = 0)
        {
            if (position < 0) throw new ArgumentException("Position can not be less than zero.");

            Buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
            Position = position;
        }

        public MessagePackReader GetReader(int positionOffset = 0)
        {
            return new MessagePackReader(Buffer, Position + positionOffset);
        }

        public void ReadNext()
        {
            Position += MessagePackBinary.ReadNext(Buffer, Position);
        }

        public void ReadNextBlock()
        {
            Position += MessagePackBinary.ReadNextBlock(Buffer, Position);
        }

        public bool TryReadNil()
        {
            bool value = MessagePackBinary.IsNil(Buffer, Position);

            if (value)
            {
                Position++;
            }

            return value;
        }

        public Nil ReadNil()
        {
            Nil value = MessagePackBinary.ReadNil(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public bool ReadBoolean()
        {
            bool value = MessagePackBinary.ReadBoolean(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public byte ReadByte()
        {
            byte value = MessagePackBinary.ReadByte(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public sbyte ReadSByte()
        {
            sbyte value = MessagePackBinary.ReadSByte(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public short ReadInt16()
        {
            short value = MessagePackBinary.ReadInt16(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public ushort ReadUInt16()
        {
            ushort value = MessagePackBinary.ReadUInt16(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public int ReadInt32()
        {
            int value = MessagePackBinary.ReadInt32(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public uint ReadUInt32()
        {
            uint value = MessagePackBinary.ReadUInt32(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public long ReadInt64()
        {
            long value = MessagePackBinary.ReadInt64(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public ulong ReadUInt64()
        {
            ulong value = MessagePackBinary.ReadUInt64(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public float ReadSingle()
        {
            float value = MessagePackBinary.ReadSingle(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public double ReadDouble()
        {
            double value = MessagePackBinary.ReadDouble(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public char ReadChar()
        {
            char value = MessagePackBinary.ReadChar(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public string ReadString()
        {
            string value = MessagePackBinary.ReadString(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public ArraySegment<byte> ReadStringSegment()
        {
            ArraySegment<byte> value = MessagePackBinary.ReadStringSegment(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public DateTime ReadDateTime()
        {
            DateTime value = MessagePackBinary.ReadDateTime(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public byte[] ReadBytes()
        {
            byte[] value = MessagePackBinary.ReadBytes(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public ArraySegment<byte> ReadBytesSegment()
        {
            ArraySegment<byte> value = MessagePackBinary.ReadBytesSegment(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public int ReadArrayHeader()
        {
            int value = MessagePackBinary.ReadArrayHeader(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public int ReadMapHeader()
        {
            int value = MessagePackBinary.ReadMapHeader(Buffer, Position, out int readSize);

            Position += readSize;

            return value;
        }

        public string Print()
        {
            var builder = new StringBuilder();

            Print(builder);

            return builder.ToString();
        }

        public void Print(StringBuilder builder)
        {
            builder.Append('[');

            for (int i = 0; i < Position; i++)
            {
                builder.Append(Buffer[i]);

                if (i != Position - 1)
                {
                    builder.Append(", ");
                }
            }

            builder.Append(']');
        }
    }
}
