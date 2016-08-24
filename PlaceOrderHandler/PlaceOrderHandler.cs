using System;
using System.Threading;
using NServiceBus;
using OrderingContracts;

namespace OrderingService
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        private readonly IBus _bus;

        public PlaceOrderHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(PlaceOrder message)
        {
            Console.WriteLine($"Placing order {message.OrderId} for customer {message.CustomerId}");
            Thread.Sleep(500);
            Console.WriteLine($"Order placed for {message.CustomerId}.");

            _bus.Publish(new OrderPlaced { OrderId = message.OrderId, CustomerId = message.CustomerId });
        }
    }
}
