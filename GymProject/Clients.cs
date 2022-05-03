using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject
{
    class Clients : Users
    {
        public List<string> Rates;
        public List<Treners> Treners;
        public Clients(int id, Gender gender, string name,
             List<string> rates, List<Treners> treners) :
            base(id, gender, name)
        {
            Rates = rates;
            Treners = treners;
        }
    }
}
