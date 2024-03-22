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
    public class Cinema : ICinema
    {
		private List<Ticket> lstT = new List<Ticket>();
		public void AddTicket(Ticket ticket)
		{
			lstT.Add(ticket);

		}
		public Ticket GetTicket(int id)
		{
			return lstT[id];

		}
		public List<Ticket> GetList()
		{
			return lstT;

		}
		public bool IsTicket(Ticket ticket)
        {
			return ticket.Id < 0 || ticket.NameSession == "" || ticket.Price < 0 || ticket.Hall < 0 || ticket.DateSession == null || ticket.NameSession == null;

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
