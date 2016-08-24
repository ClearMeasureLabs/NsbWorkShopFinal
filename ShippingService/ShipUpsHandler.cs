using System;
using NServiceBus;
using ShippingService.Contracts;

namespace ShippingService
{
    public class ShipUpsHandler : IHandleMessages<ShipUps>
    {
        private readonly IBus _bus;

        public ShipUpsHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(ShipUps message)
        {
            _bus.Reply(new UpsResponse { TrackingCode = Guid.NewGuid() });
        }
    }
}