using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Globalization;
using Microsoft.ServiceBus;
using Common;
using System.Configuration;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceNamespace = ConfigurationManager.AppSettings["serviceNamespace"];
            string servicePath = "/";


            Uri serviceAddress = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, servicePath);

            NetTcpRelayBinding binding = new NetTcpRelayBinding();

            binding.Security.RelayClientAuthenticationType = RelayClientAuthenticationType.None;

            ChannelFactory<IMyChannel> channelFactory = new ChannelFactory<IMyChannel>(binding, new EndpointAddress(serviceAddress));


            IMyChannel channel = channelFactory.CreateChannel();
            channel.Open();

            Random random = new Random();
            try
            {
                while (true)
                {
                    int i = random.Next(int.MaxValue);
                    int j = random.Next(int.MaxValue);

                    int k = channel.Add(i, j);

                    Console.WriteLine("{0} + {1} = {2}",i ,j ,k);
                }
            }
            finally
            {
                channel.Close();
            }
        }
    }
}
