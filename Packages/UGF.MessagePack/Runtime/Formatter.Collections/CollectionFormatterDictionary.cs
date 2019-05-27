using System.Collections.Generic;

namespace UGF.MessagePack.Runtime.Formatter.Collections
{
    public class CollectionFormatterDictionary<TKey, TValue> : MessagePackFormatterBase<Dictionary<TKey, TValue>>
    {
        private IMessagePackFormatter<TKey> m_formatterKey;
        private IMessagePackFormatter<TValue> m_formatterValue;

        public CollectionFormatterDictionary(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            m_formatterKey = Provider.Get<TKey>();
            m_formatterValue = Provider.Get<TValue>();
        }

        public override void Serialize(ref MessagePackWriter writer, Dictionary<TKey, TValue> value)
        {
            if (value != null)
            {
                writer.WriteMapHeader(value.Count);

                foreach (KeyValuePair<TKey, TValue> pair in value)
                {
                    m_formatterKey.Serialize(ref writer, pair.Key);
                    m_formatterValue.Serialize(ref writer, pair.Value);
                }
            }
            else
            {
                writer.WriteNil();
            }
        }

        public override Dictionary<TKey, TValue> Deserialize(ref MessagePackReader reader)
        {
            if (!reader.TryReadNil())
            {
                int count = reader.ReadMapHeader();
                var value = new Dictionary<TKey, TValue>(count);

                for (int i = 0; i < count; i++)
                {
                    value.Add(m_formatterKey.Deserialize(ref reader), m_formatterValue.Deserialize(ref reader));
                }

                return value;
            }

            return null;
        }
    }
}
