// <copyright file="Program.cs" company="Erratic Motion">
//   Copyright (c) Erratic Motion. All rights reserved.
// </copyright>

namespace Consumer
{
    using System;
    using System.Threading;
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

            configuration["group.id"] = "kafka-dotnet-getting-started";
            configuration["auto.offset.reset"] = "earliest";

            const string topic = "purchases";

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            Console.WriteLine("Starting to consume");
            using (var consumer = new ConsumerBuilder<string, string>(
                       configuration.AsEnumerable()).Build())
            {
                consumer.Subscribe(topic);
                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);
                        Console.WriteLine(
                            $"Consumed event from topic {topic} with key {cr.Message.Key,-10} and value {cr.Message.Value}");
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }
}