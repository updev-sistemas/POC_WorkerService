namespace OrderManager.ModelDTO
{

    public class OrderJson
    {
        public virtual int? Id { get; set; }
        public virtual string? Number { get; set; }
        public virtual string? Status { get; set; }
        public virtual CustomerJson? Customer { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual ItemJson[]? Items { get; set; }
    }

}