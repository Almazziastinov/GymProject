using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject
{
    enum Gender
    {
        male, female
    }
    abstract class Users
    {
        int id { get; set; }
        Gender gender { get; set; }
        string login { get; set; }
        string pasword { get; set; }
        string name { get; set; }
        string surname { get; set; }
        string midname { get; set; }

        public Users(int id, Gender gender, string login, string pasword, string name, string surname, string midname)
        {
            this.id = id;
            this.gender = gender;
            this.login = login;
            this.pasword = pasword;
            this.name = name;
            this.surname = surname;
            this.midname = midname;
        }



    }
}
