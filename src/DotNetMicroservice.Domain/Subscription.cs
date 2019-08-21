using System;

namespace DotNetMicroservice.Domain
{
    public class Subscription
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string ProductId { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }
    }
}
