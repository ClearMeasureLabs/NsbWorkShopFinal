using NServiceBus;

namespace CustomerCareService
{
    public class CustomerMadeVip : IEvent
    {
        public string CustomerId { get; set; }
    }
}