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
using BE;
using System.Net;



namespace DAL
{
    class Dal_XML_imp:Idal
    {
        bool bankDownloaded;
        void DownloadBank()
        {
            #region downloadBank
            const string xmlLocalPath = @"atm.xml";
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
               @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            catch (Exception)
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            finally
            {
                wc.Dispose();
            }
        }
            #endregion

        private XElement HostRoot;
        private XElement HostingUnitRoot;
        private XElement GuestRoot;
        private XElement OrderRoot;
        private XElement ConfigRoot;
  //      private XElement AtmRoot;
        private const string HostRootPath = @"Hosts.xml";
        private const string HostingUnitRootPath = @"HostingUnits.xml";
        private const string GuestRootPath = @"Guests.xml";
        private const string ConfigRootPath = @"Config.xml";
        private const string OrderRootPath = @"Orders.xml";
  //      private const string AtmRootPath = @"atm.xml";

        #region Singleton
        private static readonly Dal_XML_imp instance = new Dal_XML_imp();
        public static Dal_XML_imp Instance
        {
            get { return instance; }
        }

       // public Dal_XML_imp() { }
        static Dal_XML_imp() { }

        #endregion
     

        public Dal_XML_imp()
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
                    
                }
                else
                    ConfigRoot = XElement.Load(ConfigRootPath);
               /* if (!File.Exists(AtmRootPath))
                {
                    OrderRoot = new XElement("Atm");
                    OrderRoot.Save(AtmRootPath);
                }
                else
                    LoadData(AtmRoot, AtmRootPath);*/

            }
            catch
            {
                throw new Exception ("Issue with opening XML file");
            }
            deleteDup();

        }
        private void deleteDup()
        {
           /* List<BankAccount> b = loadListFromXML<BankAccount>(AtmRootPath);
            for(int i=0;i<b.Count;i++)
            {
                for (int j = i + 1; j < b.Count; j++)
                    if (b[i].BankName == b[j].BankName && b[i].BankNumber == b[j].BankNumber && b[i].BranchAddress == b[j].BranchAddress && b[i].BranchCity == b[j].BranchCity && b[i].BranchNumber == b[j].BranchNumber)
                        b.Remove(b[j]);
            }
            saveListToXML(b, AtmRootPath);*/
        }
        private void LoadConfig()
        {/*
            ConfigRoot = XElement.Load(ConfigRootPath);
            Configuration.GuestRequestKey = int.Parse((ConfigRoot.Element("config").ConfigRoot.Element("GuestRequestKey").Value));
            Configuration.OrderKey = int.Parse(ConfigRoot.Element("OrderKey").Value.ToString());
            Configuration.commission = int.Parse(ConfigRoot.Element("commission").Value.ToString());
            Configuration.HostingUnitKey = int.Parse(ConfigRoot.Element("HostingUnitKey").Value.ToString());*/
        }

        private void CreateConfig()
        {
            ConfigRoot = new XElement("config", new XElement("GuestRequestKey", "10000000"), new XElement("OrderKey", "10000000"), new XElement("HostingUnitKey", "10000000"), new XElement("commission", "10"));
            
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
                        try
                        {
                            LoadConfig();
                            guest.GuestRequestKey = ++Configuration.GuestRequestKey;
                            XElement GuestRequestKey = new XElement("GuestRequestKey",Configuration.GuestRequestKey);
                            GuestRequestKey.Save(ConfigRootPath);

                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        
                       
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
            XElement toUpdate = (from item in GuestRoot.Elements()
                                 let temp = ConvertGuest(item)
                                 where temp.GuestRequestKey==guest.GuestRequestKey
                                 select item).FirstOrDefault();
            if (toUpdate == null)
                throw new Exception("No Guest with this Key!");

            foreach (PropertyInfo item in typeof(Guest).GetProperties())
                toUpdate.Element(item.Name).SetValue(item.GetValue(guest));

            GuestRoot.Save(GuestRootPath);
        }

        public void AddHost(Host host)
        {
            Host h = GetHost(host.ID);
            if (h != null)
                throw new DuplicateWaitObjectException("Guest with this ID already exists!");
            HostRoot.Add(ConvertHost(host));

            HostRoot.Save(HostRootPath);
        }

        public void UpdateHost(Host host)
        {
            List<Host> l = loadListFromXML<Host>(HostRootPath);
            int index =l.FindIndex(t => t.ID == host.ID);//finds ondex of guest with id 
            if (index != -1)
               l[index] = host;
            else
                throw new DuplicateWaitObjectException("No Host with this id Exists!");
        }

        public void AddHostingUnit(HostingUnit hostingUnit)
        {

            HostingUnit hosting = GetHostingUnit(hostingUnit.HostingUnitName, hostingUnit.Owner.ID);
            if (hosting == null)
            {
                try
                {
                    LoadConfig();
                    hostingUnit.HostingUnitKey = ++Configuration.HostingUnitKey;
                    XElement HostingUnitKey = new XElement("HostingUnitKey", Configuration.HostingUnitKey);
                    HostingUnitKey.Save(ConfigRootPath);

                }
                catch (Exception e)
                {
                    throw e;
                }
                
                List<HostingUnit> list = loadListFromXML<HostingUnit>(HostingUnitRootPath);
                list.Add(hostingUnit);
                saveListToXML(list, HostingUnitRootPath);
            }
            else
                throw new DuplicateWaitObjectException("Hosting Unit with the same name exists!");
        }

        public void DelHostingUnit(int key)
        {
            List<HostingUnit> hu = loadListFromXML<HostingUnit>(HostingUnitRootPath);
            if (hu.Exists(x => x.HostingUnitKey == key))
            {
               hu.Remove(hu.Find(x => x.HostingUnitKey == key));
                saveListToXML(hu, HostingUnitRootPath);

            }
            else
                throw new KeyNotFoundException("Hosting Unit does not exist!");
        }

        public void UpdateHostUnit(HostingUnit hostingUnit)
        {
            List<HostingUnit> hu = loadListFromXML<HostingUnit>(HostingUnitRootPath);
            int index = hu.FindIndex(t => t.HostingUnitKey == hostingUnit.HostingUnitKey);//finds ondex of unit with key  
                                                                                                                    //  hostingUnit.HostingUnitKey = GetHostingUnit(hostingUnit.HostingUnitName).HostingUnitKey;
            if (index == -1)//meaning name not found
                throw new DuplicateWaitObjectException("No Hosting Unit with this name Exists!");
            hu[index] = hostingUnit;//update the hosting unit 
            saveListToXML(hu, HostingUnitRootPath);

        }

        public void AddOrder(Order order)
        {
            Order order1 = GetOrder(order.OrderKey);
            if (order1 == null)//if guest doesnt exist 
            {
                try
                {
                    LoadConfig();
                    order.OrderKey = ++Configuration.OrderKey;
                    XElement OrderKey = new XElement("OrderKey", Configuration.OrderKey);
                    OrderKey.Save(ConfigRootPath);

                }
                catch (Exception e)
                {
                    throw e;
                }
                OrderRoot.Add(ConvertOrder(order));

                OrderRoot.Save(OrderRootPath);
            }
            else
                throw new DuplicateWaitObjectException("Same order already exists!");
        }

        public void UpdateOrder(Order order)
        {
            List<Order> ord = loadListFromXML<Order>(OrderRootPath);
            int index = ord.FindIndex(t => t.OrderKey == order.OrderKey);
            if (index == -1)//meaning id not found
                throw new KeyNotFoundException("No order was found!");
            ord[index] = order;//update the order
            saveListToXML(ord, OrderRootPath);
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            List<HostingUnit> list = loadListFromXML<HostingUnit>(HostingUnitRootPath);
            return list;
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
            List<Host> list = loadListFromXML<Host>(HostRootPath);
            foreach (Host h  in list)
                if (h.ID == id)
                    return h;
            return null; 
        }

        public HostingUnit GetHostingUnit(int key)
        {
            List<HostingUnit> list = loadListFromXML<HostingUnit>(HostingUnitRootPath);
            foreach (HostingUnit unit in list)
                if (unit.HostingUnitKey == key)
                    return unit;
            return null;
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
            List<HostingUnit> list = loadListFromXML<HostingUnit>(HostingUnitRootPath);
            foreach (HostingUnit unit in list)
                if (unit.HostingUnitName == name && unit.Owner.ID == id)
                    return unit;
            return null;
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
           /* return (List<Host>)from item in HostRoot.Elements()
                                select ConvertHost(item);*/
            List<Host> list = loadListFromXML<Host>(HostRootPath);
            return list;
        }

        

    }
}
