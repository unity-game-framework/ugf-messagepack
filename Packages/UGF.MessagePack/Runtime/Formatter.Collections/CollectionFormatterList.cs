using System.Collections.Generic;

namespace UGF.MessagePack.Runtime.Formatter.Collections
{
    public class CollectionFormatterList<T> : MessagePackFormatterBase<List<T>>
    {
        private IMessagePackFormatter<T> m_formatterList;

        public CollectionFormatterList(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            m_formatterList = Provider.Get<T>();
        }

        public override void Serialize(ref MessagePackWriter writer, List<T> value)
        {
            if (value != null)
            {
                writer.WriteArrayHeader(value.Count);

                for (int i = 0; i < value.Count; i++)
                {
                    m_formatterList.Serialize(ref writer, value[i]);
                }
            }
            else
            {
                writer.WriteNil();
            }
        }

        public override List<T> Deserialize(ref MessagePackReader reader)
        {
            if (!reader.TryReadNil())
            {
                int count = reader.ReadArrayHeader();
                var value = new List<T>(count);

                for (int i = 0; i < count; i++)
                {
                    value.Add(m_formatterList.Deserialize(ref reader));
                }

                return value;
            }

            return null;
        }
    }
}
