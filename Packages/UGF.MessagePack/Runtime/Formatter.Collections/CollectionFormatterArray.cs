namespace UGF.MessagePack.Runtime.Formatter.Collections
{
    public class CollectionFormatterArray<T> : MessagePackFormatterBase<T[]>
    {
        private IMessagePackFormatter<T> m_formatterElement;

        public CollectionFormatterArray(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            m_formatterElement = Provider.Get<T>();
        }

        public override void Serialize(ref MessagePackWriter writer, T[] value)
        {
            if (value != null)
            {
                writer.WriteArrayHeader(value.Length);

                for (int i = 0; i < value.Length; i++)
                {
                    m_formatterElement.Serialize(ref writer, value[i]);
                }
            }
            else
            {
                writer.WriteNil();
            }
        }

        public override T[] Deserialize(ref MessagePackReader reader)
        {
            if (!reader.TryReadNil())
            {
                int count = reader.ReadArrayHeader();
                var value = new T[count];

                for (int i = 0; i < count; i++)
                {
                    value[i] = m_formatterElement.Deserialize(ref reader);
                }

                return value;
            }

            return null;
        }
    }
}
