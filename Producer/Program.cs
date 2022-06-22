// <copyright file="Program.cs" company="Erratic Motion">
//   Copyright (c) Erratic Motion. All rights reserved.
// </copyright>

namespace Producer
{
    using System;
    using Confluent.Kafka;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Entry point into the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddIniFile("getting-started.properties")
                .Build();

            const string topic = "purchases";

            string[] users = { "eabara", "jsmith", "sgarcia", "jbernard", "htanaka", "awalther" };
            string[] items = { "book", "alarm clock", "t-shirts", "gift card", "batteries" };

            using (var producer = new ProducerBuilder<string, string>(configuration.AsEnumerable()).Build())
            {
                var numProduced = 0;
                const int numMessages = 10;
                for (var i = 0; i < numMessages; ++i)
                {
                    Random rnd = new ();
                    var user = users[rnd.Next(users.Length)];
                    var item = items[rnd.Next(items.Length)];

                    producer.Produce(
                        topic,
                        new Message<string, string> { Key = user, Value = item },
                        deliveryReport =>
                        {
                            if (deliveryReport.Error.Code != ErrorCode.NoError)
                            {
                                Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                            }
                            else
                            {
                                Console.WriteLine($"Produced event (2) to topic {topic}: key = {user,-10} value = {item}");
                                numProduced += 1;
                            }
                        });
                }

                producer.Flush(TimeSpan.FromSeconds(10));
                Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
                Console.ReadLine();
            }
        }
    }
}