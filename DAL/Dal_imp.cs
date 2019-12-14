using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    class Dal_imp : Idal
    {
        private List<Guest> guests = new List<Guest>();
        private static List<Host> hosts = new List<Host> ();
        private static List<Order> orders = new List<Order> ();
        private static List<HostingUnit> hostingUnits = new List<HostingUnit>();

        public Guest GetGuest(int ID)//returns a guest with ID inputed ,null if dosent exis
        {
            return guests.FirstOrDefault(s => s.ID == ID);//linq
        }

        public void AddGuestReq(Guest guest)//adds a guest request to list of guests
        {
            Guest g1 = GetGuest(guest.ID);
            if (g1 != null)
                throw new Exception("Guest with the same ID already exists!");
            guests.Add(guest);
        }

        public HostingUnit GetHosting(String name, Host owner)
        {
            return hostingUnits.FirstOrDefault(s => s.HostingUnitName == name && s.Owner==owner);//linq
        }

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            HostingUnit h1 = GetHosting(hostingUnit.HostingUnitName,hostingUnit.Owner);
            if (h1 != null)
                throw new Exception("Hosting Unit with the same name and owner already exists!");
            hostingUnits.Add(hostingUnit);
        }

        public Order GetOrder(int orderkey)
        {
            return orders.FirstOrDefault(s => s.);
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DelHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public List<BankAccount> GetAllBankAccounts()
        {
            throw new NotImplementedException();
        }

        public List<Guest> GetAllGuests()
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestReq(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        
    }
}
