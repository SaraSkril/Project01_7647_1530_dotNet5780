using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
    {
        #region Guest
        void AddGuestReq(Guest guest);//Adds a new Guest Request
        void UpdateGuestReq(Guest guest);//Updates guest

        #endregion

        #region HostingUnit
        void AddHostingUnit(HostingUnit hostingUnit);//Adds new Hosting unit;
        void DelHostingUnit(string name);//Deletes Hosting Unit
        void UpdateHostUnit(HostingUnit hostingUnit);//Updates Hosting Unit;

        #endregion

        #region Order
        void AddOrder(Order order);//adds a new order
        void UpdateOrder(Order order);//Updates Order

        #endregion

        List<HostingUnit> GetAllHostingUnits();//returns a lists with all hosting unit
        List<Guest> GetAllGuests();//returns a list with all Guests
        List<Order> GetAllOrders();//returns a list with all orders
        IEnumerable<BankAccount> GetAllBankAccounts();//returns a list with all Bank Accounts 

        #region finds element
        Host GetHost(string id);
        HostingUnit GetHostingUnit(string name);
        Guest GetGuest(string id);
        Order GetOrder(int guestkey, int unitkey);
        List<Host> GetHosts();
        #endregion
    }
}
