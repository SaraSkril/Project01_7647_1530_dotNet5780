using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class HostingUnit
    {
        private static int HostingUnitKey1 = 100000;

        public static int HostingUnitKey { get { return HostingUnitKey1; }  }//check if need to put in configure
        public Host Owner { get; set; }//owner of hosting unit
        public string HostingUnitName { get; set; }//name of the hosting unit
        public bool[,] Diary = new bool[12, 31];//matrix of hosting unit status
        public override string ToString()
        {
            return "Hosting unit key: " + HostingUnitKey + "\n" + "Unit Name: " + HostingUnitName + "\n" + Owner.ToString();
        }

    }
}
