using System;
using NServiceBus;
using ShippingService.Contracts;

namespace ShippingService
{
    public class ShipFedExHandler : IHandleMessages<ShipFedEx>
    {
        private readonly IBus _bus;

        public ShipFedExHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(ShipFedEx message)
        {
            return;
            _bus.Reply(new FedExResponse { TrackingCode = Guid.NewGuid() });
        }
    }
}