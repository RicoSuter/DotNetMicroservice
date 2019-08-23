namespace DotNetMicroservice.Models
{
    public class OrderDto
    {
        public string Id { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public string ParcelNumber { get; set; }

        public string State { get; set; }
    }
}
