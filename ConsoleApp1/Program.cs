using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Diagnostics;

public class Program
{
	static void Main(string[] args)
	{
		var factory = new ConnectionFactory() { Uri = new Uri("amqps://frseewas:xcVFnbux3PtCmsBegoRH3PC36zFPihyg@porpoise.rmq.cloudamqp.com/frseewas") }; //подставить свой ключ!!!

		using (var connection = factory.CreateConnection())
		using (var channel = connection.CreateModel())
		{
			channel.QueueDeclare(queue: "MyQueue",
							 durable: false,
							 exclusive: false,
							 autoDelete: false,
							 arguments: null);

			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine(" [x] Received {0}", message);
			};
			channel.BasicConsume(queue: "MyQueue",
							 autoAck: true,
						 consumer: consumer);

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();

		}
	}
}

