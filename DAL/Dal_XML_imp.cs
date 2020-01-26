﻿using System;
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
    class Dal_XML_imp : Idal
    {
         XElement HostRoot;
         XElement HostingUnitRoot;
         XElement GuestRoot;
        XElement OrderRoot;
         XElement ConfigRoot;
        //      private XElement AtmRoot;
         string HostRootPath = @"Hosts.xml";
        string HostingUnitRootPath = @"HostingUnits.xml";
        string GuestRootPath = @"Guests.xml";
         string ConfigRootPath = @"Config.xml";
        string OrderRootPath = @"Orders.xml";
        //      private const string AtmRootPath = @"atm.xml";

        #region Singleton
        private static readonly Dal_XML_imp instance = new Dal_XML_imp();
        public static Dal_XML_imp Instance
        {
            get { return instance; }
        }

       
        static Dal_XML_imp() { }

        public Dal_XML_imp()
        {
            if (!File.Exists(GuestRootPath))
                CreatFileGuests();
            else
                LoadDataGuests();

            if (!File.Exists(HostingUnitRootPath))
                CreatFileHU();
            else
                LoadDataHU();

            if (!File.Exists(HostRootPath))
                CreatFileHost();
            else
                LoadDataHost();
            if (!File.Exists(OrderRootPath))
                CreatFileOrder();
            else
                LoadDataOrder();

            if (!File.Exists(ConfigRootPath))
            {
                ConfigRoot = new XElement("config", new XElement("config", "00000000"));
                ConfigRoot.Save(ConfigRootPath);
            }
            else ConfigRoot = XElement.Load(ConfigRootPath);
        }
        #region Load&Create
        private void LoadDataGuests()//load from file to program (סוג xelement)
        {
            try
            {
                GuestRoot = XElement.Load(GuestRootPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        private void CreatFileGuests()//for new file
        {
            GuestRoot = new XElement("Guest");
            GuestRoot.Save(GuestRootPath);//add new main element
        }

        private void CreatFileOrder()
        {
            OrderRoot = new XElement("Order");
            OrderRoot.Save(OrderRootPath);
        }

        private void LoadDataOrder()
        {
            try
            {
                OrderRoot = XElement.Load(OrderRootPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadDataHU()//load from file to program 
        {
            try
            {
                HostingUnitRoot = XElement.Load(HostingUnitRootPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        private void CreatFileHU()//for new file
        {
            HostingUnitRoot = new XElement("HostingUnit");
            HostingUnitRoot.Save(HostingUnitRootPath);//add new main element
        }

        private void CreatFileHost()//for new file
        {
            HostRoot = new XElement("Host");
            HostRoot.Save(HostRootPath);//add new main element
        }

        private void LoadDataHost()//load from file to program (סוג xelement)
        {
            try
            {
                HostRoot = XElement.Load(HostRootPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        private void LoadDataConfig()
        {
            try
            {
                ConfigRoot = XElement.Load(ConfigRootPath);
            }
            catch
            {
                throw new Exception("File upload problem");

            }
        }

        #endregion

        /*  public Dal_XML_imp()
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
                       LoadData(AtmRoot, AtmRootPath);

              }
              catch
              {
                  throw new Exception("Issue with opening XML file");
              }
              deleteDup();

          }

          public static volatile bool bankDownloaded = false;//flag if bank was downloaded
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
                  bankDownloaded = true;
              }
              catch (Exception ex)
              {
                  try
                  {
                      string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                      wc.DownloadFile(xmlServerPath, xmlLocalPath);
                      bankDownloaded = true;
                  }
                  catch (Exception exeption)
                  {
                      //tries again if the connection didn't allow to download it
                  }
              }
              finally
              {
                  wc.Dispose();
              }
              #endregion

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
              saveListToXML(b, AtmRootPath);
          }
          private void LoadConfig()
          {/*
              ConfigRoot = XElement.Load(ConfigRootPath);
              Configuration.GuestRequestKey = int.Parse((ConfigRoot.Element("config").ConfigRoot.Element("GuestRequestKey").Value));
              Configuration.OrderKey = int.Parse(ConfigRoot.Element("OrderKey").Value.ToString());
              Configuration.commission = int.Parse(ConfigRoot.Element("commission").Value.ToString());
              Configuration.HostingUnitKey = int.Parse(ConfigRoot.Element("HostingUnitKey").Value.ToString());
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
          }*/

        private void LoadConfig()
        {
             /* ConfigRoot = XElement.Load(ConfigRootPath);
              Configuration.GuestRequestKey = int.Parse((ConfigRoot.Element("config").ConfigRoot.Element("GuestRequestKey").Value));
              Configuration.OrderKey = int.Parse(ConfigRoot.Element("OrderKey").Value.ToString());
              Configuration.commission = int.Parse(ConfigRoot.Element("commission").Value.ToString());
              Configuration.HostingUnitKey = int.Parse(ConfigRoot.Element("HostingUnitKey").Value.ToString());*/
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
        XElement ConvertHU(HostingUnit hosting)
        {
            XElement HUelement = new XElement("HostingUnit");

           // HostElement.Add(new XElement("BankDetails", new XElement("BankName", host.BankDetails.BankName), new XElement("BankNumber", host.BankDetails.BankNumber), new XElement("BranchAddress", host.BankDetails.BranchAddress), new XElement("BranchCity", host.BankDetails.BranchCity), new XElement("BranchNumber", host.BankDetails.BranchNumber)));
            HUelement.Add(new XElement("HostingUnitKey", hosting.HostingUnitKey));
           // XElement Owner = ConvertHost(hosting.Owner);
           // HUelement.Add(new XElement("Owner", ConvertHost(hosting.Owner)));
           HUelement.Add(new XElement()
            HUelement.Add(new XElement("HostingUnitName", hosting.HostingUnitName));
            HUelement.Add(new XElement("area", hosting.area));
            HUelement.Add(new XElement("TypeUnit", hosting.TypeUnit));
            HUelement.Add(new XElement("pool", hosting.pool));
            HUelement.Add(new XElement("Jacuzzi", hosting.Jacuzzi));
            HUelement.Add(new XElement("Garden", hosting.Garden));
            HUelement.Add(new XElement("ChildrensAttractions", hosting.ChildrensAttractions));
            HUelement.Add(new XElement("Wifi", hosting.Wifi));

            XElement Diary = new XElement("Diary");
            for (int i = 0; i <12; i++)
                for (int j = 0; j < 31; j++)
                    Diary.Add(hosting.Diary[i, j].ToString() + " ");

            HUelement.Add(Diary);

            return HUelement;
        }

        HostingUnit ConvertHU(XElement element)
        {
          HostingUnit t = new HostingUnit();
            
            t.HostingUnitKey = int.Parse(element.Element("HostingUnitKey").Value);
            t.Owner = ConvertHost(element.Element("Owner"));
            t.HostingUnitName = element.Element("HostingUnitName").Value;
            t.area = (Area)Enum.Parse(typeof(Area),element.Element("area").Value);
            t.TypeUnit = (TypeUnit)Enum.Parse(typeof(TypeUnit), element.Element("TypeUnit").Value);
            t.pool = bool.Parse(element.Element("pool").Value);
            t.Jacuzzi = bool.Parse(element.Element("Jacuzzi").Value);
            t.Garden = bool.Parse(element.Element("Garden").Value);
            t.ChildrensAttractions = bool.Parse(element.Element("ChildrensAttractions").Value);
            t.Wifi = bool.Parse(element.Element("Wifi").Value);
          
            var Temp = element.Element("Diary").Value;
            int k = 0;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                    t.Diary[i, j] = bool.Parse(Temp.Split(' ')[k++]);

            return t;

        }
        XElement ConvertHostHU(this XElement HostElement, Host host)
        {
           // XElement HostElement = new XElement("Host");

            HostElement.Add(new XElement("ID", host.ID));
            HostElement.Add(new XElement("FirstName", host.FirstName));
            HostElement.Add(new XElement("LastName", host.LastName));
            HostElement.Add(new XElement("PhoneNumber", host.PhoneNumber));
            HostElement.Add(new XElement("EmailAddress", host.EmailAddress));
            HostElement.Add(new XElement("BankDetails", new XElement("BankName", host.BankDetails.BankName), new XElement("BankNumber", host.BankDetails.BankNumber), new XElement("BranchAddress", host.BankDetails.BranchAddress), new XElement("BranchCity", host.BankDetails.BranchCity), new XElement("BranchNumber", host.BankDetails.BranchNumber)));
            // HostElement.Add(new XElement("BankDetails").Element("BankName").Value = host.BankDetails.BankName.ToString());
            HostElement.Add(new XElement("BankAccountNumber", host.BankAccountNumber));
            HostElement.Add(new XElement("CollectionClearance", host.CollectionClearance));
            HostElement.Add(new XElement("commission", host.commission));

            return HostElement;
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
         // HostElement.Add(new XElement("BankDetails").Element("BankName").Value = host.BankDetails.BankName.ToString());
            HostElement.Add(new XElement("BankAccountNumber", host.BankAccountNumber));
            HostElement.Add(new XElement("CollectionClearance", host.CollectionClearance));
            HostElement.Add(new XElement("commission", host.commission));
       
            return HostElement;
        }

        public static BankAccount ToBank(XElement a)
        {
            BankAccount help = new BankAccount();
            help.BankName = a.Element("BankName").Value;
            help.BankNumber = Int32.Parse(a.Element("BankNumber").Value);
            help.BranchAddress = a.Element("BranchAddress").Value;
            help.BranchCity = a.Element("BranchCity").Value;
            help.BranchNumber = Int32.Parse(a.Element("BranchNumber").Value);
            return help;
        }
        Host ConvertHost(XElement element)
        {
            Host h = new Host();
            
            h.ID = element.Element("ID").Value;
            h.FirstName = element.Element("FirstName").Value;
            h.LastName = element.Element("LastName").Value;
            h.PhoneNumber = int.Parse(element.Element("PhoneNumber").Value);
            h.EmailAddress = element.Element("EmailAddress").Value;
            h.BankDetails = ToBank(element.Element("BankDetails"));
            h.CollectionClearance = (CollectionClearance)Enum.Parse(typeof(CollectionClearance), element.Element("CollectionClearance").Value);
            h.commission = int.Parse(element.Element("commission").Value);
            return h;
        }

      
        public static bool[,] ToMatrix(XElement m)
        {
            bool[,] help = new bool[12,31];
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                    help[i, j] = bool.Parse(m.Element("_" + i + "_" + j).Value);
            return help;
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
            LoadDataHost();
            XElement HostElement = (from item in HostRoot.Elements()
                                 let temp = ConvertHost(item)
                                 where temp.ID == host.ID
                                 select item).FirstOrDefault();
            if (HostElement == null)
                throw new Exception("No Host with this Id found!");
            HostElement.Remove();
            HostRoot.Add(ConvertHost(host));
            HostRoot.Save(HostRootPath);

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

                HostingUnitRoot.Add(ConvertHU(hostingUnit));

                HostingUnitRoot.Save(HostingUnitRootPath);
            }
            else
                throw new DuplicateWaitObjectException("Hosting Unit with the same name exists!");
        }

        public void DelHostingUnit(int key)
        {
            LoadDataHU();
            XElement removeHUElement;

            //find wanted to be deleted
            removeHUElement = (from HUElement in HostingUnitRoot.Elements()
                                    where int.Parse(HUElement.Element("HostingUnitKey").Value) == key
                                    select HUElement).FirstOrDefault();

            if (removeHUElement == null)//cant remove cause didnt find
                throw new Exception("No such Hosting Unit in the system");

            removeHUElement.Remove();//delete from root
            HostingUnitRoot.Save(HostingUnitRootPath);//save to file
            
        }

        public void UpdateHostUnit(HostingUnit hostingUnit)
        {
            LoadDataHU();
            XElement HostUElement = (from item in HostingUnitRoot.Elements()
                                    let temp = ConvertHU(item)
                                    where temp.HostingUnitKey == hostingUnit.HostingUnitKey
                                    select item).FirstOrDefault();
            if (HostUElement == null)
                throw new Exception("No Host with this Id found!");
            HostUElement.Remove();
            HostingUnitRoot.Add(ConvertHU(hostingUnit));
            HostingUnitRoot.Save(HostingUnitRootPath);
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
            LoadDataOrder();
            XElement orderElement = (from item in OrderRoot.Elements()
                                     let temp = ConvertOrder(item)
                                     where temp.OrderKey == order.OrderKey
                                     select item).FirstOrDefault();
            if (orderElement == null)
                throw new Exception("Order Not found!");
            orderElement.Remove();
            OrderRoot.Add(ConvertOrder(order));
            OrderRoot.Save(OrderRootPath);
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            LoadDataHU();
            List<HostingUnit> hu = new List<HostingUnit>();
            foreach (XElement o in HostingUnitRoot.Elements())
            {
                HostingUnit t = ConvertHU(o);

                hu.Add(t);
            }
            return hu;
        }

        public List<Guest> GetAllGuests()
        {
            LoadDataGuests();
            List<Guest> guest = new List<Guest>();
            foreach (XElement o in GuestRoot.Elements())
            {
                Guest t = ConvertGuest(o);

                guest.Add(t);
            }
            return guest;
        
    }

        public List<Order> GetAllOrders()
        {
            
            LoadDataOrder();
            List<Order> allorder = new List<Order>();
            foreach (XElement o in OrderRoot.Elements())
            {
                Order t = ConvertOrder(o);

                allorder.Add(t);
            }
            return allorder;
        }

        public IEnumerable<BankAccount> GetAllBankAccounts()
        {
            throw new NotImplementedException();
            
        }

        public Host GetHost(string id)
        {
            List<Host> list = (List<Host>)GetHosts();
            foreach (Host h in list)
                if (h.ID == id)
                    return h;
            return null; 

        }

        public HostingUnit GetHostingUnit(int key)
        {
            List<HostingUnit> list = GetAllHostingUnits();
            foreach (HostingUnit unit in list)
                if (unit.HostingUnitKey == key)
                    return unit;
            return null;
            
        }

        public Order GetOrder(int guestkey, int unitkey)
        {
            LoadDataOrder();
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
            LoadDataOrder();
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
            List<HostingUnit> list = GetAllHostingUnits();
            foreach(HostingUnit h in list)
            {
                if (h.HostingUnitName == name && h.Owner.ID == id)
                    return h;
            }
            return null;
        }

        public Guest GetGuest(int key)
        {
            LoadDataGuests();
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

        public List<Host> GetHosts()//works bh
        {
         
            LoadDataHost();
            List<Host> allHosts = new List<Host>();
            foreach (XElement hostElement in HostRoot.Elements())
            {
                Host t = ConvertHost(hostElement);

                allHosts.Add(t);
            }
           return  allHosts;
           

        }

        

    }
}
#endregion