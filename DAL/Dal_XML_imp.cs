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
        private const string HostRootPath = @"..\..\..\Xml\Hosts.xml";
        private const string HostingUnitRootPath = @"..\..\..\Xml\HostingUnits.xml";
        private const string GuestRootPath = @"..\..\..\Xml\Guests.xml";
        private const string ConfigRootPath = @"..\..\..\Xml\Config.xml";
        private const string OrderRootPath = @"..\..\..\Xml\Orders.xml";
        protected static Dal_XML_imp instance = null;
        public static Dal_XML_imp Getinstance()
        {
            if (instance == null)
                instance = new Dal_XML_imp();
            return instance;
        }

        protected Dal_XML_imp()
        {
            try
            {
                if (!File.Exists(HostRootPath))
                {
                    HostRoot = new XElement("Hosts");
                    HostRoot.Save(HostRootPath);
                }
                else
                    LoadData(HostRoot, HostRootPath);
                if (!File.Exists(HostingUnitRootPath))
                {
                    HostingUnitRoot = new XElement("HostingUnit");
                    HostingUnitRoot.Save(HostingUnitRootPath);
                }
                else
                    LoadData(HostingUnitRoot, HostingUnitRootPath);
                if (!File.Exists(GuestRootPath))
                {
                    GuestRoot = new XElement("Guest");
                    GuestRoot.Save(GuestRootPath);
                }
                else
                    LoadData(GuestRoot, GuestRootPath);
                if (!File.Exists(OrderRootPath))
                {
                    OrderRoot = new XElement("Orders");
                    OrderRoot.Save(OrderRootPath);
                }
                else
                    LoadData(OrderRoot, OrderRootPath);
                if (!File.Exists(ConfigRootPath))
                {
                    CreateConfig();
                    LoadConfig();
                }
                else
                    LoadConfig();
            }
            catch
            {
                throw new Exception ("Issue with opening XML file");
            }

        }
        private void LoadConfig()
        {
            ConfigRoot = XElement.Load(ConfigRootPath);
            Configuration.GuestRequestKey = int.Parse(ConfigRoot.Element("GuestRequestKey").Value.ToString());
            Configuration.OrderKey = int.Parse(ConfigRoot.Element("OrderKey").Value.ToString());
            Configuration.commission = int.Parse(ConfigRoot.Element("commission").Value.ToString());
            Configuration.HostingUnitKey = int.Parse(ConfigRoot.Element("HostingUnitKey").Value.ToString());
        }

        private void CreateConfig()
        {
            XElement GuestRequestKey = new XElement("GuestRequestKey", "10000000");
            XElement OrderKey = new XElement("OrderKey", "10000000");
            XElement HostingUnitKey = new XElement("HostingUnitKey", "10000000");
            XElement commission = new XElement("commission", "10");
            ConfigRoot.Save(ConfigRootPath);
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
        #region Convert
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
        XElement ConvertOrder(Order order)
        {
            XElement OrderElement = new XElement("Order");

            foreach (PropertyInfo item in typeof(Order).GetProperties())
                OrderElement.Add
                    (
                    new XElement(item.Name, item.GetValue(order, null))
                    );

            return OrderElement;
        }
        Order ConvertOrder(XElement element)
        {
            Order o = new Order();

            foreach (PropertyInfo item in typeof(Order).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);

                item.SetValue(o, convertValue);
            }

            return o;
        }

       
        XElement ConvertHost(Host host)
        {
            XElement HostElement = new XElement("Host");

            HostElement.Add(new XElement("ID", host.ID));
            HostElement.Add(new XElement("FirstName", host.FirstName));
            HostElement.Add(new XElement("LastName", host.LastName));
            HostElement.Add(new XElement("PhoneNumber", host.PhoneNumber));
            HostElement.Add(new XElement("EmailAddress", host.EmailAddress));
            HostElement.Add(new XElement("BankDetails", new XElement("BankName", host.BankDetails.BankName), new XElement("BankNumber", host.BankDetails.BankNumber), new XElement("BranchAddress", host.BankDetails.BranchAddress), new XElement("BranchCity", host.BankDetails.BranchCity), new XElement("BranchNumber", host.BankDetails.BranchNumber)));
            HostElement.Add(new XElement("BankAccountNumber", host.BankAccountNumber));
            HostElement.Add(new XElement("CollectionClearance", host.CollectionClearance));
            HostElement.Add(new XElement("commission", host.commission));
       
            return HostElement;
        }
        Host ConvertHost(XElement element)
        {
            Host h = new Host();
            
            h.ID = element.Element("ID").Value.ToString();
            h.FirstName = element.Element("FirstName").Value;
            h.LastName = element.Element("LastName").Value;
            h.PhoneNumber = int.Parse(element.Element("PhoneNumber").Value);
            h.EmailAddress = element.Element("EmailAddress").Value;
            h.BankDetails.BankName = element.Element("BankDetails").Element("BankName").Value;
            h.BankDetails.BankNumber = int.Parse(element.Element("BankDetails").Element("BankNumber").Value);
            h.BankDetails.BranchAddress = element.Element("BankDetails").Element("BranchAddress").Value;
            h.BankDetails.BranchCity = element.Element("BankDetails").Element("BranchCity").Value;
            h.BankDetails.BranchNumber =int.Parse( element.Element("BankDetails").Element("BranchNumber").Value);
            h.BankAccountNumber= int.Parse(element.Element("BankAccountNumber").Value);
            h.CollectionClearance = (CollectionClearance)Enum.Parse(typeof(CollectionClearance), element.Element("CollectionClearance").Value);
            h.commission = int.Parse(element.Element("commission").Value);
            return h;
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
            return (List<Order>)from item in OrderRoot.Elements()
                                select ConvertOrder(item);
        }

        public IEnumerable<BankAccount> GetAllBankAccounts()
        {
            throw new NotImplementedException();
        }

        public Host GetHost(string id)
        {
            XElement host = null;

            try
            {
                host = (from item in HostRoot.Elements()
                         where (item.Element("ID").Value) == id
                         select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            if (host == null)
                return null;

            return ConvertHost(host);
        }

        public HostingUnit GetHostingUnit(int key)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int guestkey, int unitkey)
        {
            XElement order = null;
           
            try
            {
                order = (from item in OrderRoot.Elements()
                         where int.Parse(item.Element("GuestRequestKey").Value) == guestkey && int.Parse(item.Element("HostingUnitKey").Value) == unitkey
                         select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            if (order == null)
                return null;

            return ConvertOrder(order);
        }

        public Order GetOrder(int orderkey)
        {
            XElement order = null;
            
            try
            {
                order = (from item in OrderRoot.Elements()
                      where int.Parse(item.Element("OrderKey").Value) == orderkey
                         select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            if (order == null)
                return null;

            return ConvertOrder(order);
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
            return (List<Host>)from item in HostRoot.Elements()
                                select ConvertHost(item);
        }

        

    }
}
