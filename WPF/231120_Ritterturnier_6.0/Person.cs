using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231120_Ritterturnier_6._0
{
    class Person
    {
        protected string name;
        protected string telnum;

        public Person(string name, string telnum)
        {
            this.name = name;
            this.telnum = telnum;
        }

        public virtual string toString()
        {
            string s = "";
            return s;
        }

        public string getName() { return name; }


    }
}