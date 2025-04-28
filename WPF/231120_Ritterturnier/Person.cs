using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231120_Ritterturnier
{
    class Person
    {
        protected string name;
        protected string telnum;

        public Person()
        {
            
        }

        public Person(string name, string telnum)
        {
            this.name = name;
            this.telnum = telnum;
        }

        public virtual string ToString()        //toString für Ausgabe
        {
            string s = "";
            return s;
        }

        public string GetName() { return name; }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Telnum
        {
            get { return telnum; }
            set { telnum = value; }
        }
    }
}
