using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication21.Models;

namespace WebApplication21
{
    public interface ICinema
    {
        Ticket GetTicket(int id);
        void AddTicket(Ticket ticket);
        List<Ticket> GetList();
        bool IsTicket(Ticket ticket);
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}
