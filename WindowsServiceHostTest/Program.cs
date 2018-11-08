﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsServiceHostTest
{
   class Program
   {
      static void Main(string[] args)
      {
         Uri baseAddress = new Uri("http://localhost:8080/hello");

         // Create the ServiceHost.
         using (ServiceHost host = new ServiceHost(typeof(HelloWorldService), baseAddress))
         {
            // Enable metadata publishing.
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            host.Description.Behaviors.Add(smb);

            // Open the ServiceHost to start listening for messages. Since
            // no endpoints are explicitly configured, the runtime will create
            // one endpoint per base address for each service contract implemented
            // by the service.
            host.Open();

            Console.WriteLine("The service is ready at {0}", baseAddress);
            Console.WriteLine("Press <Enter> to stop the service.");
            Console.ReadLine();

            // Close the ServiceHost.
            host.Close();
         }
      }
   }

   public class HelloWorldService : IHelloWorldService
   {
      public string SayHello(string name)
      {
         return ReturnGreeting(name);
      }

      public Task<string> SayHelloWithTask(string name)
      {
         return Task.Run(() => ReturnGreeting(name));
      }

      private static string ReturnGreeting(string name)
      {
         Thread.Sleep(3000);
         var a = Enumerable.Range(0, 10000);
         return $"Hello, {name}, {string.Join("|",a)}";
      }
   }
}
