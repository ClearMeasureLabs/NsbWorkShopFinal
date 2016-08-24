using System;
using NServiceBus;

namespace ShippingService.Contracts
{
    public class ShipFedEx : ICommand
    {
        public Guid OrderId { get; set; }
    }
}