using System;
using NServiceBus;

namespace ShippingService.Contracts
{
    public class UpsResponse : IMessage
    {
        public Guid TrackingCode { get; set; }
    }
}