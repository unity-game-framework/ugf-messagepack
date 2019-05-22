namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackFormatter<T> : IMessagePackFormatter
    {
        void Serialize(ref MessagePackWriter writer, T value);
        new T Deserialize(ref MessagePackReader reader);
    }
}
