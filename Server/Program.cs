using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ServiceBus;
using System.Globalization;
using System.ServiceModel;
using Common;
using System.Configuration;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            string issuerName = ConfigurationManager.AppSettings["issuerName"];
            string issuerSecret = ConfigurationManager.AppSettings["issuerSecret"];
            string serviceNamespace = ConfigurationManager.AppSettings["serviceNamespace"];

            string servicePath = "/";


            TransportClientEndpointBehavior relayCredentials = new TransportClientEndpointBehavior();
            relayCredentials.TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);

            NetTcpRelayBinding binding = new NetTcpRelayBinding();

            // The client is not required to present a security token to the AppFabric Service Bus.
            binding.Security.RelayClientAuthenticationType = RelayClientAuthenticationType.None;

            Uri serviceAddress = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, servicePath);

            // Create the service host.
            ServiceHost host = new ServiceHost(typeof(MyService), serviceAddress);

            // Add the service endpoint with the NetTcpRelayBinding binding.
            host.AddServiceEndpoint(typeof(IMyContract), binding, serviceAddress);

            // Add the credentials through the endpoint behavior.
            host.Description.Endpoints[0].Behaviors.Add(relayCredentials);

            // Start the service.
            host.Open();

            Console.WriteLine(".NET Service Bus Sample Server is running.");
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();

            host.Close();
        }
    }
}
