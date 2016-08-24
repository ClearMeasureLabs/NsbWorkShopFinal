using System;
using NServiceBus.Saga;
using OrderingContracts;

namespace CustomerCareService
{
    public class CustomerVipPolicy : Saga<CustomerVipStatus>, IAmStartedByMessages<OrderPlaced>, IHandleTimeouts<DecrementEligibleOrder>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CustomerVipStatus> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(message => message.CustomerId).ToSaga(saga => saga.CustomerId);
        }

        public void Handle(OrderPlaced message)
        {
            if (Data.CustomerId != message.CustomerId) Data.CustomerId = message.CustomerId;

            Data.EligibleOrderCount += 1;

            if (Data.EligibleOrderCount >= 5 && !Data.IsVip)
            {
                Data.IsVip = true;
                Bus.Publish(new CustomerMadeVip { CustomerId = message.CustomerId });
                Console.WriteLine($"Customer {message.CustomerId} is now VIP.");
            }

            RequestTimeout(DateTime.Now.AddSeconds(10), new DecrementEligibleOrder { CustomerId = message.CustomerId });
        }

        public void Timeout(DecrementEligibleOrder state)
        {
            Data.EligibleOrderCount -= 1;
            Console.WriteLine($"Eligible order removed for customer {Data.CustomerId}.");
            if (Data.EligibleOrderCount < 5 && Data.IsVip)
            {
                Data.IsVip = false;
                Console.WriteLine($"Customer {Data.CustomerId} is no longer a VIP.");
            }
        }
    }

    public class CustomerVipStatus : ContainSagaData
    {
        public virtual string CustomerId { get; set; }
        public virtual int EligibleOrderCount { get; set; }
        public virtual bool IsVip { get; set; }
    }
}