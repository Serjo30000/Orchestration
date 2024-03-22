using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication21.Models;

namespace WebApplication21
{
    public interface IConsumer
    {
        Client GetClient(int id);
        void AddClient(Client client);
        List<Client> GetList();
        bool IsClient(Client client);
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}
