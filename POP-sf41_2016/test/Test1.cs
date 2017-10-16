using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.test
{
    class Test1
    {
        public Test1()
        {
            A a = new A();

            a.SetNaziv("naziv svih naziva");

            Console.WriteLine("Naziv je: " + a.GetNaziv());

            a.Ime = "Ime svih vremena";
            Console.WriteLine("Ime je: " + a.Ime);
            Console.ReadLine(); 
        }
    }
}
