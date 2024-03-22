using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string SecondName { get; set; }
        public override string ToString()
        {
            return "Id=" + Id + ", Name=" + Name + ", Family=" + Family + ", SecondName=" + SecondName;
        }

    }
}
