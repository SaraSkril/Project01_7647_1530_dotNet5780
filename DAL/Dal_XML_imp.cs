using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;

namespace DAL
{
    class Dal_XML_imp
    {
        private XElement HostRoot;
        private XElement HostingUnitRoot;
        private XElement GuestRoot;
        private XElement OrderRoot;
        private XElement ConfigRoot;
        private const string HostRootPath = @"..\..\..\Xml files\Hosts.xml";
        private const string HostingUnitRootPath = @"..\..\..\Xml files\HostingUnits.xml";
        private const string GuestRootPath = @"..\..\..\Xml files\Guests.xml";
        private const string ConfigRootPath = @"..\..\..\Xml files\Config.xml";
        private const string OrderRootPath = @"..\..\..\Xml files\Orders.xml";
        protected Dal_XML_imp()
        {
           
        }
    }
}
