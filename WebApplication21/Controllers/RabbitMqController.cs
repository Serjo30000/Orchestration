using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication21.Models;

namespace WebApplication21.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqController : ControllerBase
    {
        private readonly ICinema _mqService1;
        private readonly IAccounting _mqService2;
        private readonly IConsumer _mqService3;
        public RabbitMqController(ICinema mqService1, IAccounting mqService2, IConsumer mqService3)
        {
            _mqService1 = mqService1;
            _mqService2 = mqService2;
            _mqService3 = mqService3;
        }
        /// <summary>
		/// Создание клиента
		/// </summary>
		/// <response code="200">Успешное выполнение</response>
		/// <response code="400">Плохой запрос</response>
        [HttpPost]
        [Route("[action]")]
        public IActionResult SendClient([FromBody] Client client)
        {
            if (_mqService3.IsClient(client)==true || !ModelState.IsValid)
            {
                _mqService3.SendMessage("VERIFICATIONFAILED");
                _mqService3.SendMessage("PURCHASECREATIONFAILED");
                return BadRequest(ModelState); 
            }
            else
            {
                _mqService3.SendMessage("VERIFYCONSUMER");
                _mqService3.AddClient(client);
                Debug.WriteLine(_mqService3.GetClient(_mqService3.GetList().Count - 1));
            }
            return Ok("Сообщение отправлено");
        }
        /// <summary>
        /// Создание билета
        /// </summary>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Плохой запрос</response>
        [HttpPost]
        [Route("[action]")]
        public IActionResult SendTicket([FromBody] Ticket ticket)
        {
            if (_mqService1.IsTicket(ticket) || !ModelState.IsValid)
            {
                _mqService1.SendMessage("VERIFICATIONFAILED");
                _mqService1.SendMessage("PURCHASECREATIONFAILED");
                return BadRequest(ModelState);
            }
            else
            {
                _mqService1.SendMessage("CONSUMERVERIFIRED");
                _mqService1.AddTicket(ticket);
                Debug.WriteLine(_mqService1.GetTicket(_mqService1.GetList().Count - 1));
            }
            return Ok("Сообщение отправлено");
        }
        /// <summary>
		/// Создание карты
		/// </summary>
		/// <response code="200">Успешное выполнение</response>
		/// <response code="400">Плохой запрос</response>
        [HttpPost]
        [Route("[action]")]
        public IActionResult SendCard([FromBody] Card card)
        {
            _mqService2.SendMessage("CONSUMERVERIFIRED");
            _mqService2.SendMessage("AUTHORIZECARD");
            if (_mqService2.IsCard(card) || !ModelState.IsValid)
            {
                _mqService2.SendMessage("CARDAUTHORIZATIONFAILED");
                _mqService2.SendMessage("VERIFICATIONFAILED");
                _mqService2.SendMessage("PURCHASECREATIONFAILED");
                return BadRequest(ModelState);
            }
            else
            {
                _mqService2.SendMessage("CARDAUTHORIZATIONVERIFY");
                _mqService2.AddCard(card);
                Debug.WriteLine(_mqService2.GetCard(_mqService2.GetList().Count - 1));
            }
            return Ok("Сообщение отправлено");
        }
        /// <summary>
		/// Подтверждение покупки
		/// </summary>
		/// <response code="200">Успешное выполнение</response>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ApproveMessage()
        {
            _mqService1.SendMessage("CONSUMERVERIFIRED");
            _mqService1.SendMessage("AUTHORIZECARD");
            _mqService2.SendMessage("CARDAUTHORIZATIONVERIFY");
            _mqService2.SendMessage("ORDERCONFIRMED");
            _mqService2.SendMessage("PURCHASE");
            return Ok("Сообщение отправлено");
        }
    }
}
