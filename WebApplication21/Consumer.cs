using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication21.Models;

namespace WebApplication21
{
    public class Consumer : IConsumer
    {
		private List<Client> lstCl = new List<Client>();
		public void AddClient(Client client)
		{
			lstCl.Add(client);

		}
		public Client GetClient(int id)
		{
			return lstCl[id];

		}
		public List<Client> GetList()
		{
			return lstCl;

		}
		public bool IsClient(Client client)
        {
			return client.Id < 0 || client.Name == "" || client.Family == "" || client.SecondName == "" || client.Name == null || client.Family == null || client.SecondName == null;
        }
		public void SendMessage(object obj)
		{
			var message = JsonSerializer.Serialize(obj);
			SendMessage(message);
		}

		public void SendMessage(string message)
		{
			var factory = new ConnectionFactory() { Uri = new Uri("amqps://frseewas:xcVFnbux3PtCmsBegoRH3PC36zFPihyg@porpoise.rmq.cloudamqp.com/frseewas") }; //своя очередь

			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "MyQueue",
						   durable: false,
						   exclusive: false,
						   autoDelete: false,
						   arguments: null);

				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(exchange: "",
					   routingKey: "MyQueue",
					   basicProperties: null,
						   body: body);
			}
		}
	}
}
