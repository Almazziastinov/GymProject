using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject
{
    class Treners : Users
    {
        public List<string> Subjects;
        public List<Clients> Clients;
        public Treners(int id, Gender gender, string login, string pasword, string name, 
            string surname, string midname, List<string> subjects, List<Clients> clients) : 
            base(id, gender, login, pasword, name, surname, midname)
        {
            Subjects = subjects;
            Clients = clients;
        }
    }
}
