using System;
using NServiceBus;

namespace OrderingContracts
{
    public class OrderPlaced : IEvent
    {
        public Guid OrderId { get; set; }
        public string CustomerId { get; set; }
    }
}
