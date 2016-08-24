using System;
using System.Threading;
using NServiceBus;
using NServiceBus.Persistence;
using OrderingContracts;
using static System.Console;
namespace OrderingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var busConfig = new BusConfiguration();
            busConfig.UsePersistence<NHibernatePersistence>().ConnectionString(@"Server=(localdb)\mssqllocaldb;Database=NSB;Trusted_Connection=True;");

            var bus = Bus.Create(busConfig);
            while (true)
            {
                Write("Please enter the customer id to place the order: ");
                var customerName = ReadLine();

                if (string.IsNullOrEmpty(customerName)) break;

                var orderId = Guid.NewGuid();

                var placeOrderCommand = new PlaceOrder { OrderId = orderId, CustomerId = customerName };
                bus.Send(placeOrderCommand);

                WriteLine($"Order {orderId} has been placed!");
            }
        }
    }
}
