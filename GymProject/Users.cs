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
        string name { get; set; }
        

        public Users(int id, Gender gender, string name)
        {
            this.id = id;
            this.gender = gender;
            this.name = name;
            
        }



    }
}
