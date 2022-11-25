using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderManager.Entity
{
    public class OrderBson : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string? Id { get; set; }

        [BsonElement("number")]
        public virtual string? Number { get; set; }

        [BsonElement("order_status")]
        public virtual string? Status { get; set; }

        [BsonElement("date")]
        public virtual DateTime? Date { get; set; }

        [BsonElement("customer")]
        public virtual CustomerBson? Customer { get; set; }

        [BsonElement("items")]
        public virtual ICollection<ItemBson>? Items { get; set; }
    }
}
