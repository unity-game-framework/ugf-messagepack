namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackFormatter<T> : IMessagePackFormatter
    {
        void Serialize(ref MessagePackWriter writer, T value, IMessagePackProvider provider, IMessagePackContext context);
        new T Deserialize(ref MessagePackReader reader, IMessagePackProvider provider, IMessagePackContext context);
    }
}
