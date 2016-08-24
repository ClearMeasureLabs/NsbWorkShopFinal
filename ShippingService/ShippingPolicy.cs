using System;
using NServiceBus;
using NServiceBus.Saga;
using OrderingContracts;
using ShippingService.Contracts;

namespace ShippingService
{
    public class ShippingPolicy : Saga<ShippingState>, IAmStartedByMessages<OrderPlaced>,
        IHandleMessages<FedExResponse>,
        IHandleMessages<UpsResponse>,
        IHandleTimeouts<FedExTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShippingState> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(m => m.OrderId).ToSaga(d => d.OrderId);
        }

        public void Handle(OrderPlaced message)
        {
            Data.OrderId = message.OrderId;
            Data.FedExStatus = ShippingStatus.Attempting;

            Bus.Send(new ShipFedEx { OrderId = message.OrderId });
            RequestTimeout(DateTime.Now.AddSeconds(10), new FedExTimeout());
            Data.FedExStatus = ShippingStatus.Attempting;
            Console.WriteLine($"Attempting to ship FedEx for order {message.OrderId}");
        }

        public void Handle(FedExResponse message)
        {
            throw new NotImplementedException();
        }

        public void Handle(UpsResponse message)
        {
            Data.UpsStatus = ShippingStatus.Shipped;
            Console.WriteLine($"Shipping UPS was Successful for order {Data.OrderId}. Tracking Code: {message.TrackingCode}");
        }

        public void Timeout(FedExTimeout state)
        {
            Console.WriteLine("The FedEx request timedout. Shipping via UPS now.");
            Data.FedExStatus = ShippingStatus.Failed;
            Data.UpsStatus = ShippingStatus.Attempting;
        }

    }

    public class ShippingState : ContainSagaData
    {
        public virtual ShippingStatus FedExStatus { get; set; }
        public virtual ShippingStatus UpsStatus { get; set; }
        [Unique]
        public virtual Guid OrderId { get; set; }
    }

    public enum ShippingStatus
    {
        NotStarted = 0,
        Attempting = 1,
        Shipped = 2,
        Failed = 3
    }
}