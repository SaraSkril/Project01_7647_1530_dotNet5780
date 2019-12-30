using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    class Dal_imp : Idal
    {
        #region Singleton
        private static readonly Dal_imp instance = new Dal_imp();
        public static Dal_imp Instance
        {
            get { return instance; }
        }

        public Dal_imp() { }
        static Dal_imp() { }

        #endregion
        //Linq type 1
        //deal with all configuration issues 
        public Host GetHost(string id) 
            {
            var Host = from host in DataSource.getHosts()
                          where host.ID == id
                          select host;
            return Host.FirstOrDefault();
            }

        public HostingUnit GetHostingUnit(string name) //gets an id and returns the hosting unit
            {
             var request = from HU in DataSource.getHostingUnits()
                          where HU.HostingUnitName==name
                          select HU;
            return request.FirstOrDefault();
            }

        public HostingUnit GetHostingUnit(int key) //gets an id and returns the hosting unit
            {
             var request = from HU in DataSource.getHostingUnits()
                          where HU.HostingUnitKey==key
                          select HU;
            return request.FirstOrDefault();
            }
        
        public Guest GetGuest(string id) //gets an id and returns the guest
        {
        var request = from guest in DataSource.getGuests()
                          where guest.ID==id
                          select guest;
            return request.FirstOrDefault();
         }

        public Guest GetGuest(int key) //gets an id and returns the guest
        {
        var request = from guest in DataSource.getGuests()
                          where guest.GuestRequestKey==key
                          select guest;
            return request.FirstOrDefault();
         }

        public Order GetOrder(int guestkey, int unitkey)
            {
          var request = from ord in DataSource.GetOrders()
                          where ord.HostingUnitKey==unitkey&&ord.GuestRequestKey==guestkey
                          select ord;
            return request.FirstOrDefault();
            }

        public Order GetOrder(int OrderKey)
            {
           var request = from ord in DataSource.GetOrders()
                          where ord.OrderKey==OrderKey
                          select ord;
            return request.FirstOrDefault();
            }

        #region Guest 
        public void AddGuestReq(Guest guest)
        {
            Guest guest1 = GetGuest(guest.ID);
            if (guest1 == null)//if guest doesnt exist 
            {
                if (guest.GuestRequestKey < 10000000)
                {
                    guest.GuestRequestKey =++Configuration.GuestRequestKey;//update serial number
                   
                }
                DataSource.getGuests().Add(guest.Clone());//adds new guest to list of guest(using clone funcion- sends a copy of the original)f 
            }
            else
                throw new DuplicateWaitObjectException("Guest with this ID already exists!");
        }

        public void UpdateGuestReq(Guest guest)
        {
            int index = DataSource.getGuests().FindIndex(t => t.ID == guest.ID);//finds ondex of guest with id  
            if (index == -1)//meaning id not found
                throw new KeyNotFoundException("No Guest with this id!");
         
            DataSource.getGuests()[index] = guest.Clone();//update the guest
        }

        public List<Guest> GetAllGuests()
        {
            return DataSource.getGuests();
        }

        #endregion

        #region Hosting Unit
        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            HostingUnit hosting = GetHostingUnit(hostingUnit.HostingUnitName);
            if (hosting == null)
            {
                hostingUnit.HostingUnitKey = ++(Configuration.HostingUnitKey);
                DataSource.getHostingUnits().Add(hostingUnit.Clone());
            }
            else
                throw new DuplicateWaitObjectException("Hosting Unit with the same name exists!");
        }

        public void UpdateHostUnit(HostingUnit hostingUnit)
        {
            int index = DataSource.getHostingUnits().FindIndex(t => t.HostingUnitName == hostingUnit.HostingUnitName);//finds ondex of guest with id  
            if (index == -1)//meaning name not found
                throw new DuplicateWaitObjectException("No Hosting Unit with this name Exists!");
            DataSource.getHostingUnits()[index] = hostingUnit.Clone();//update the hosting unit 
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            return DataSource.getHostingUnits();
        }

        public void DelHostingUnit(string name)
        {

            if (DataSource.getHostingUnits().Exists(x => x.HostingUnitName == name))
            {
                DataSource.getHostingUnits().Remove(DataSource.getHostingUnits().Find(x => x.HostingUnitName == name));

            }
            throw new KeyNotFoundException("Hosting Unit does not exist!");
        }
        #endregion

        #region Order
        public void AddOrder(Order order)
        {
            Order order1 = GetOrder(order.OrderKey);
            if (order1 == null)//if guest doesnt exist 
            {
                DataSource.GetOrders().Add(order.Clone());//adds new order to list of orders(using clone funcion- sends a copy of the original)f 
            }
            else
                throw new DuplicateWaitObjectException("Same order already exists!");
        }

        public void UpdateOrder(Order order)
        {
            int index = DataSource.GetOrders().FindIndex(t => t.OrderKey==order.OrderKey);
            if (index == -1)//meaning id not found
                throw new KeyNotFoundException("No order was found!");
            DataSource.GetOrders()[index] = order.Clone();//update the order

        }

        public List<Order> GetAllOrders()
        {
            return DataSource.GetOrders();
        }

        #endregion


        public IEnumerable<BankAccount> GetAllBankAccounts()//Linq 4
        {

            return from BA in DataSource.GetBankAccounts()
                   select BA.Clone();

        }
        public List<Host> GetHosts()
        {
            return DataSource.getHosts();
        }

    }
}
