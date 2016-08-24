using NServiceBus;

namespace OrderingContracts
{
    public class DecrementEligibleOrder : ICommand
    {
        public string CustomerId { get; set; }
    }
}