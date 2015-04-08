﻿namespace DreamFactory.Demo
{
    using System;

    class Program
    {
        // private const string BaseAddress = "http://pinebit.ddns.net";
        private const string BaseAddress = "https://dsp-pinebit.cloud.dreamfactory.com";

        static void Main()
        {
            Console.WriteLine("DreamFactory REST API Demo");

            // HTTP functions demo
            Console.WriteLine();
            Console.WriteLine("### HTTP functions demo");
            HttpDemo.Run().Wait();

            // UserSession API
            Console.WriteLine();
            Console.WriteLine("### UserSession API demo");
            UserSessionDemo.Run(BaseAddress).Wait();

            // Files API
            Console.WriteLine();
            Console.WriteLine("### Files API demo");
            FilesDemo.Run(BaseAddress).Wait();
        }
    }
}
