using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject
{
    class Halls
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public int numOfCl { get; private set; }

        public Halls(int id, string name, int numOfCl)
        {
            this.id = id;
            this.name = name;
            this.numOfCl = numOfCl;
        }

        public Halls()
        { }

    }
}
