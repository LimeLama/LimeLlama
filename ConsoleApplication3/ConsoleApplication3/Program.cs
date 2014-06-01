using ConsoleApplication3.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication3
{
    class Program
    {           
        static void Main(string[] args)
        {
            InterfaceClient client = new InterfaceClient();
            Console.WriteLine(client.AddLogin("Login8", "333"));
            Console.ReadLine();
        }
    }
}
