using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
namespace BL
{
    interface IBL
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

        #region now starting to do the rest of function 
        bool CheckDate(DateTime start, DateTime end);//check if end day is longer than 1 day by start
        bool CheckIsBankAllowed(Guest guest);//check if guest allows access to bank acct
        bool IsAvailible(HostingUnit hostingUnit, DateTime start, DateTime end);//checks if dates in hosting unit are availble 
        bool CheckifOrderIsClosed(Order order);//if order is closed we cant change the status (return false) else return true, if status is canged to closed we have to charg 10 nis 
        bool CanCharge(Host host);//when order is closed we charge the owner 10 nis
        bool CheckOffDates(HostingUnit hostingUnit);//when status is changed to closed, uodate diary in hosting unit 
        void updateInfo(Guest guest/*all orders with guest*/);//when order status is chenged to clod=sed update guest and all orders that have to do with the guest
        bool IsHostingUnitActive(HostingUnit hostingUnit);//checks if hosting unit is active in any order, if so w cant delete
        bool ChangeCollectionClearance(HostingUnit hostingUnit);//checks if theres a open order, if so we cannot change collection clearance
        void SendMail(Order order);//when status of order is changed to "sent mail", this function will send the mail
        List<HostingUnit> AllDays(DateTime date, int duration);//returns all hosting units with avilble date
        int DaysBetween(DateTime start/*end=now*/);//returns the days between the start day and now 
        int DaysBetween(DateTime start,DateTime end);//returns the days between the start and last day
        List<Order> DaysFromOrder(int num);//returns all orders that "num"days have past since they were created / sent email (num or bigger)



        #endregion
    }
}
