using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouse_Assignment3
{
    public class Performer
    {
        public string Name { get; set; }
        public double Fee { get; set; }

        public Performer(string name, double fee)
        {
            this.Name = name;
            this.Fee = fee;
        }


    }
}