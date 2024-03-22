using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string NameSession { get; set; }
        public int Price { get; set; }
        public int Hall { get; set; }
        public DateTime DateSession { get; set; }
        public override string ToString()
        {
            return "Id=" + Id + ", NameSession=" + NameSession + ", Price=" + Price + ", Hall=" + Hall + ", DateSession=" + DateSession;
        }
    }
}
