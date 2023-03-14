﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VWW.Samples.Chat.Service;

namespace VWW.Samples.Chat.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(ChatService)))
            {
                // Open the host and start listening for incoming messages.
                serviceHost.Open();
                // Keep the service running until the Enter key is pressed.
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press the Enter key to terminate service.");
                Console.ReadLine();
            }
        }
    }
}
