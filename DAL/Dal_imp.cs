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
        //Linq type 1
        //deal with all configuration issues 
        public Host GetHost(string id) => DataSource.getHosts().FirstOrDefault(t => t.ID == id);//gets a code and return the host
        public HostingUnit GetHostingUnit(string name) => DataSource.getHostingUnits().FirstOrDefault(t => t.HostingUnitName == name);//gets an id and returns the hosting unit
        public Guest GetGuest(string id) => DataSource.getGuests().FirstOrDefault(t => t.ID == id);//gets an id and returns the guest
        public Order GetOrder(int guestkey, int unitkey) => DataSource.GetOrders().FirstOrDefault(t => t.HostingUnitKey == unitkey && t.GuestRequestKey == guestkey);//returns order with the guest and unit 

        #region Guest 
        public void AddGuestReq(Guest guest)
        {
            Guest guest1 = GetGuest(guest.ID);
            if (guest1 == null)//if guest doesnt exist 
            {
                if (guest.GuestRequestKey < 10000000)
                    guest.GuestRequestKey = (Configuration.GuestRequestKey++);//update serial number
                DataSource.getGuests().Add(guest.Clone());//adds new guest to list of guest(using clone funcion- sends a copy of the original)f 
            }
            else
                throw new Exception("Guest with this ID already exists!");
        }

        public void UpdateGuestReq(Guest guest)
        {
            int index = DataSource.getGuests().FindIndex(t => t.ID == guest.ID);//finds ondex of guest with id  
            if (index == -1)//meaning id not found
                throw new Exception("No Guest with this id!");
            if (guest.GuestRequestKey < 10000000)
                guest.GuestRequestKey = (Configuration.GuestRequestKey++);//update serial number
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
                hostingUnit.HostingUnitKey = (Configuration.HostingUnitKey++);
                DataSource.getHostingUnits().Add(hostingUnit.Clone());
            }
            else
                throw new Exception("Hosting Unit with the same name exists!");
        }

        public void UpdateHostUnit(HostingUnit hostingUnit)
        {
            int index = DataSource.getHostingUnits().FindIndex(t => t.HostingUnitName == hostingUnit.HostingUnitName);//finds ondex of guest with id  
            if (index == -1)//meaning name not found
                throw new Exception("No Hosting Unit with this name Exists!");
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
            throw new Exception("Hosting Unit does not exist!");
        }
        #endregion

        #region Order
        public void AddOrder(Order order)
        {
            Order order1 = GetOrder(order.GuestRequestKey, order.HostingUnitKey);
            if (order1 == null)//if guest doesnt exist 
            {
                order.OrderKey = (Configuration.OrderKey++);
                DataSource.GetOrders().Add(order.Clone());//adds new order to list of orders(using clone funcion- sends a copy of the original)f 
            }
            else
                throw new Exception("Same order already exists!");
        }

        public void UpdateOrder(Order order)
        {
            int index = DataSource.GetOrders().FindIndex(t => t.HostingUnitKey == order.HostingUnitKey && t.GuestRequestKey == order.GuestRequestKey);
            if (index == -1)//meaning id not found
                throw new Exception("No order was found!");
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


    }
}
