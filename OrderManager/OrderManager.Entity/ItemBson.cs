using MongoDB.Bson.Serialization.Attributes;

namespace OrderManager.Entity
{
    public class ItemBson
    {
        [BsonElement("sku")]
        public virtual string? Sku { get; set; }

        [BsonElement("product_name")]
        public virtual string? ProductName { get; set; }

        [BsonElement("qtd")]
        public virtual decimal? Quantity { get; set; }

        [BsonElement("cost")]
        public virtual decimal? Cost { get; set; }
    }
}
