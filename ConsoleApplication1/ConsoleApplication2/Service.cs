using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Service : Interface
    {
        public string test(string g)
        {
            string servg = "Test = " + g;
            return servg;
        }
    }
}
