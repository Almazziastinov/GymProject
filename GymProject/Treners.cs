using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject
{
    class Treners : Users
    {
        public string Subjects;
        public List<Clients> Clients;
        string login { get; set; }
        string pasword { get; set; }
        public Treners(int id, Gender gender, string login, string pasword, string name, 
           string subjects, List<Clients> clients) : 
            base(id, gender, name)
        {
            Subjects = subjects;
            Clients = clients;
            this.login = login;
            this.pasword = pasword;
        }
    }
}
