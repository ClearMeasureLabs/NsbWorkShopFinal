using System;
using NServiceBus;

namespace ShippingService.Contracts
{
    public class ShipUps : ICommand
    {
        public Guid OrderId { get; set; }
    }
}