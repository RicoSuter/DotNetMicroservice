using System;

namespace DotNetMicroservice.Domain
{
    public class Product
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public TimeSpan SubscriptionDuration { get; set; }
    }
}
