using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    interface Idal
    {
        #region Guest
        void AddGuestReq(Guest guest);//Adds a new Guest Request
        void UpdateGuestReq(Guest guest);//Updates guest
        
        #endregion

        #region HostingUnit
        void AddHostingUnit(HostingUnit hostingUnit);//Adds new Hosting unit;
        void DelHostingUnit(HostingUnit hostingUnit);//Deletes Hosting Unit
        void UpdateHostUnit(HostingUnit hostingUnit);//Updates Hosting Unit;
        
        #endregion

        #region Order
        void AddOrder(Order order);//adds a new order
        void UpdateOrder(Order order);//Updates Order
        
        #endregion

        List<HostingUnit> GetAllHostingUnits();//returns a lists with all hosting unit
        List<Guest> GetAllGuests();//returns a list with all Guests
        List<Order> GetAllOrders();//returns a list with all orders
        List<BankAccount> GetAllBankAccounts();//returns a list with all Bank Accounts 

    }
}
