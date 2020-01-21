using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Xml.Serialization;




namespace DAL
{
    class Dal_XML_imp:Idal
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

            if (!File.Exists(HostRootPath))
            {
                HostRoot = new XElement("Hosts");
                HostRoot.Save(HostRootPath);
            }
            else
                LoadData(HostRoot,HostRootPath);
            if (!File.Exists(HostingUnitRootPath))
            {
                HostingUnitRoot = new XElement("HostingUnit");
                HostingUnitRoot.Save(HostingUnitRootPath);
            }
            else
                LoadData(HostingUnitRoot,HostingUnitRootPath);
            if (!File.Exists(GuestRootPath))
            {
                GuestRoot = new XElement("Guest");
                GuestRoot.Save(GuestRootPath);
            }
            else
                LoadData(GuestRoot,GuestRootPath);
            if (!File.Exists(OrderRootPath))
            {
                OrderRoot = new XElement("Orders");
                OrderRoot.Save(OrderRootPath);
            }
            else
                LoadData(OrderRoot,OrderRootPath);
            if (!File.Exists(ConfigRootPath))
            {
                HostRoot = new XElement("Config");
                HostRoot.Save(ConfigRootPath);
            }
            else
                LoadData(ConfigRoot, ConfigRootPath);

        }
        private void LoadData(XElement x, string s)
        {
            try
            {
                x = XElement.Load(s);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        #region Covert
        XElement ConvertGuest(Guest guest)
        {
            XElement GuestElement = new XElement("Guest");

            foreach (PropertyInfo item in typeof(Guest).GetProperties())
                GuestElement.Add
                    (
                    new XElement(item.Name, item.GetValue(guest, null))
                    );

            return GuestElement;
        }
        Guest ConvertGuest(XElement element)
        {
            Guest g = new Guest();

            foreach (PropertyInfo item in typeof(Guest).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);

                item.SetValue(g, convertValue);
            }

            return g;
        }


        public static void saveListToXML<T>(List<T> list, string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            x.Serialize(fs, list);
        }

        public static List<T> loadListFromXML<T>(string path)
        {
            List<T> list;
            XmlSerializer x = new XmlSerializer(typeof(List<T>));
            FileStream fs = new FileStream(path, FileMode.Open);
            list = (List<T>)x.Deserialize(fs);
            return list;

        }

        #endregion
        public void AddGuestReq(Guest guest)
        {
            try
            {
                Guest guest1 = GetGuest(guest.GuestRequestKey);
                if (guest1 == null)//if guest doesnt exist 
                {
                    guest.GuestStatus = Status.Active;
                    {
                        // guest.GuestRequestKey = ++Configuration.GuestRequestKey;//update serial number
                        //how do we get the config number
                    }
                    GuestRoot.Add(ConvertGuest(guest));

                    GuestRoot.Save(GuestRootPath);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void UpdateGuestReq(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void AddHost(Host host)
        {
            throw new NotImplementedException();
        }

        public void UpdateHost(Host host)
        {
            throw new NotImplementedException();
        }

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void DelHostingUnit(int key)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            throw new NotImplementedException();
        }

        public List<Guest> GetAllGuests()
        {
            return(List<Guest>) from item in GuestRoot.Elements()
                   select ConvertGuest(item);
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BankAccount> GetAllBankAccounts()
        {
            throw new NotImplementedException();
        }

        public Host GetHost(string id)
        {
            throw new NotImplementedException();
        }

        public HostingUnit GetHostingUnit(int key)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int guestkey, int unitkey)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int orderkey)
        {
            throw new NotImplementedException();
        }

        public HostingUnit GetHostingUnit(string name, string id)
        {
            throw new NotImplementedException();
        }

        public Guest GetGuest(int key)
        {

            XElement gu = null;

            try
            {
                gu = (from item in GuestRoot.Elements()
                       where int.Parse(item.Element("GuestRequestKey").Value) == key
                       select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            if (gu == null)
                return null;

            return ConvertGuest(gu);
        }

        public List<Host> GetHosts()
        {
            throw new NotImplementedException();
        }

        

    }
}
