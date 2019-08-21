namespace DotNetMicroservice.Services.Messages
{
    public class CreateSubscriptionMessage
    {
        public string Id { get; set; }

        public string SubscriptionId { get; set; }

        public string UserId { get; set; }

        public string ProductId { get; set; }
    }
}
