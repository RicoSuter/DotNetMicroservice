namespace DotNetMicroservice.Domain
{
    public class Order
    {
        public string Id { get; set; }

        public OrderState State { get; set; }

        public string UserId { get; set; }

        public string ProductId { get; set; }

        public string ParcelNumber { get; set; }

        public int Quantity { get; set; }
    }
}
