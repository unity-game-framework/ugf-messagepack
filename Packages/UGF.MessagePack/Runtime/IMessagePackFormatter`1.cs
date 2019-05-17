namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackFormatter<T>
    {
        void Serialize(ref MessagePackWriter writer, T value);
        void Serialize(ref MessagePackWriter writer, T value, IMessagePackContext context);
        T Deserialize(ref MessagePackReader reader);
        T Deserialize(ref MessagePackReader reader, IMessagePackContext context);
    }
}
