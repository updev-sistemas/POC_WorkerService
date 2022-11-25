namespace OrderManager.ModelDTO
{
    public class ItemJson
    {
        public virtual string? Sku { get; set; }
        public virtual string? Product { get; set; }
        public virtual decimal? Quantity { get; set; }
        public virtual decimal? Cost { get; set; }
    }

}