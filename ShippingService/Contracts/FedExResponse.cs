using System;
using NServiceBus;

namespace ShippingService.Contracts
{
    public class FedExResponse : IMessage
    {
        public Guid TrackingCode { get; set; }
    }
}