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
    public class Accounting : IAccounting
    {
		private List<Card> lstC = new List<Card>();
		public void AddCard(Card client)
		{
			lstC.Add(client);

		}
		public Card GetCard(int id)
		{
			return lstC[id];

		}
		public List<Card> GetList()
		{
			return lstC;

		}
		public bool IsCard(Card card)
        {
			return card.Id < 0 || card.NameCard == "" || card.NameCard == null;

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
