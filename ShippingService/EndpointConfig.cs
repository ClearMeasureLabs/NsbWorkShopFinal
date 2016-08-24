
using NServiceBus.Persistence;

namespace ShippingService
{
    using NServiceBus;
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<NHibernatePersistence>().ConnectionString(@"Server=(localdb)\mssqllocaldb;Database=NSB;Trusted_Connection=True;");
        }
    }
}
