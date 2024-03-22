using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication21.Models;

namespace WebApplication21
{
    public interface IAccounting
    {
        Card GetCard(int id);
        void AddCard(Card card);
        List<Card> GetList();
        bool IsCard(Card card);
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}
