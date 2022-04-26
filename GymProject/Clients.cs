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
        public Clients(int id, Gender gender, string login, string pasword, string name,
            string surname, string midname, List<string> rates, List<Treners> treners) :
            base(id, gender, login, pasword, name, surname, midname)
        {
            Rates = rates;
            Treners = treners;
        }
    }
}
