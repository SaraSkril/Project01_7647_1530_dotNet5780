using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class HostingUnit
    {
        public static int HostingUnitKey { get; }//check if need to put in configure
        public Host Owner { get; set; }//owner of hosting unit
        public string HostingUnitName { get; set; }//name of the hosting unit

       // public bool diary[,] = new bool[12, 31] { get; }
    }
}
