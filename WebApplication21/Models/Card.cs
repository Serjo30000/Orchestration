using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string NameCard { get; set; }
        public bool Status { get; set; }
        public override string ToString()
        {
            return "Id="+Id+ ", NameCard=" + NameCard + ", Status=" + Status;
        }
    }
}
