using ProstorImena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016
{
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();

            a.SetNaziv("naziv svih naziva");

            Console.WriteLine("Naziv je" + a.GetNaziv());
            Console.ReadLine();
        }
    }
}
