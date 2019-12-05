using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Configuration
    {
        static int GuestRequestKey1 = 10000000;//guest serial number

        public int GuestRequestKey
        {
            get { return GuestRequestKey1; }
            set { GuestRequestKey1 = value; }
        }
    }
}
