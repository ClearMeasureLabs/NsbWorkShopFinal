using System;
using NServiceBus;

namespace OrderingContracts
{
    public class PlaceOrder : ICommand
    {
        public Guid OrderId { get; set; }
        public string CustomerId { get; set; }
    }
}