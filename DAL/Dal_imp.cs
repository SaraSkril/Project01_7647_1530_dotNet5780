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
    public Host GetHost(string id)=>  DataSource.getHosts().FirstOrDefault(t => t.ID == id);//gets a code and return the host
    public HostingUnit GetHostingUnit(string name) => DataSource.getHostingUnits().FirstOrDefault(t => t.HostingUnitName== name);//gets an id and returns the hosting unit
    public Guest GetGuest(string id) => DataSource.getGuests().FirstOrDefault(t => t.ID == id);//gets an id and returns the guest
    
        public void AddGuestReq(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
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
