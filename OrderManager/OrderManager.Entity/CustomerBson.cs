using MongoDB.Bson.Serialization.Attributes;

namespace OrderManager.Entity
{
    public class CustomerBson
    {
        [BsonElement("name")]
        public virtual string? Name { get; set; }

        [BsonElement("email")]
        public virtual string? Email { get; set; }

        [BsonElement("phone")]
        public virtual string? Phone { get; set; }
    }
}
