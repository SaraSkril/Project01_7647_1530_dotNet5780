using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    internal class IBL_Imp : IBL

    {
        DAL.Idal dal;

        public IBL_Imp()//ctor
        {
            dal = DAL.FactoryDal.GetDal();
        }

        #region Guest
        void AddGuestReq(Guest guest)//Adds a new Guest Request
        {

        }
        void UpdateGuestReq(Guest guest)//Updates guest
        {

        }

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

        List<HostingUnit> GetAllHostingUnits()//returns a lists with all hosting unit
        List<Guest> GetAllGuests();//returns a list with all Guests
        List<Order> GetAllOrders();//returns a list with all orders
        IEnumerable<BankAccount> GetAllBankAccounts();//returns a list with all Bank Accounts 

        #region now starting to do the rest of function 
        bool CheckDate(DateTime start, DateTime end)//check if end day is longer than 1 day by start
        {
            DateTime temp = start.AddDays(1);
            if (temp > end)
                return false;
            return true;
        }
        bool CheckIsBankAllowed(Host host, Order order)//check if host allows access to bank acct, if so sends email else returns false 
        {
            if ((int)(host.CollectionClearance) == 0)//checks if theres access to bank acct 
            {
                order.Status = Status.Mail_Sent;
                SendMail(order);
                return true;
            }
            return false;

        }
        bool IsAvailible(HostingUnit hostingUnit, DateTime start, DateTime end)//checks if dates in hosting unit are availble 
        {
            DateTime tempstart = start;
            while (tempstart != end)
            {
                if (hostingUnit.Diary[tempstart.Month, tempstart.Day] == true)//if date accupied
                    return false;
                tempstart.AddDays(1);
            }
            return true;//dates were available
        }
        bool CheckifOrderIsClosed(Order order)//checks if status can be changed
        {
            if (order.Status == Status.Closed_ClientRequest || order.Status == Status.Closed_NoReply)//order closed
                return true;//status cannot be changed
            return false;
        }
        bool Charge(Host host, int numdays);//when order is closed we charge the owner 10 nis
        Host FindHost(int key);//recieves hosting unit key and returns the host that ownes it
        bool CheckOffDates(HostingUnit hostingUnit, DateTime start, DateTime end)//when status is changed to closed, update diary in hosting unit 
        {
            if (IsAvailible(hostingUnit, start, end) == false)

                throw new OperationCanceledException("Dates are not availible for this Unit.");
            DateTime tempstart = start;
            while (tempstart != end)
            {
                hostingUnit.Diary[tempstart.Month, tempstart.Day] = true;//marks that date is accupied
                    
                tempstart.AddDays(1);
            }
            return true;//dates were available

        }
        void updateInfo(Guest guest, Order order/*all orders with guest*/)//when order status is chenged to closed update guest and all orders that have to do with the guest
        {
            if (CheckifOrderIsClosed(order) == true)
            {
                throw new TaskCanceledException("order status is closed already");
            }
            Charge(FindHost(order.HostingUnitKey), DaysBetween(guest.EntryDate, guest.ReleaseDate));//charges the host 10 nis 
            HostingUnit tmp=dal.GetHostingUnit()
            CheckOffDates();
            guest.GuestStatus = Status.Closed_ClientRequest;
            foreach (Order order1 in dal.GetAllOrders())
            {
                if (order1.GuestRequestKey == guest.GuestRequestKey)
                    order1.Status = Status.Closed_ClientRequest;
            }
        }
        bool IsHostingUnitActive(HostingUnit hostingUnit);//checks if hosting unit is active in any order, if so w cant delete
        bool ChangeCollectionClearance(HostingUnit hostingUnit);//checks if theres a open order, if so we cannot change collection clearance
        void SendMail(Order order);//when status of order is changed to "sent mail", this function will send the mail
        List<HostingUnit> AllDays(DateTime date, int duration);//returns all hosting units with avilble date
        int DaysBetween(DateTime start/*end=now*/);//returns the days between the start day and now 
        int DaysBetween(DateTime start, DateTime end);//returns the days between the start and last day
        List<Order> DaysFromOrder(int num);//returns all orders that "num"days have past since they were created / sent email (num or bigger)
        int NumOfVacationers(Guest g);//recieves a guest and returns num of vacationers
        int NumOfHostingUnits(Host h);//recieves a host and returns num of hosting units he ownes
        bool HostExist(Host host);
        bool GuestExist(Guest guest);
        bool HostingUnitExist(HostingUnit hostingUnit);
        bool OrderExist(Order order);
        bool isDateTaken(HostingUnit hostingUnit, DateTime start, DateTime end);
        //Func -דלגייט שקיים  מקבל משהו ומחזיר משהו
        //predicate-בודק אם תנאי מסוים שמציבים בו מתקיים
        // IEnumeable מאפשר לנו לעבור על איברי האובייקט שלנו, לסדר אותם, לסנן אותם או סתם לדלות מהם מידע.
        IEnumerable<Guest> GetAllGuests(Func<Guest, bool> predicate = null);//recieves a predicate and returns all guests that  satisfy the predicate condition
        int NumForGuest(Guest guest);//counts and returns how many orders have been sent to him
        int NumForUnit(HostingUnit hostingUnit);//counts how many orders were  closed/sent for hosting unit
        Host FindHost(int key);//recieves hosting unit key and returns the host that ownes it

        IEnumerable<IGrouping<Area, Guest>> GetGuestsGroupsByArea()//groups geusts according to area
        {
            //try
            {
                return from item in dal.GetAllGuests()
                       orderby item.LastName, item.FirstName
                       group item by item.Area
                       into g
                       orderby g.Key
                       select g;
            }
            //catch (DirectoryNotFoundException e)
            //{
            //    throw e;
            //}
        }
        IEnumerable<IGrouping<int, Guest>> GetGuestsGroupsByVacationers()//groups guests according to num vacation
        {
            return from item in dal.GetAllGuests()
                   orderby item.LastName, item.FirstName
                   group item by NumOfVacationers(item)
                       into g
                   orderby g.Key
                   select g;
        }
        IEnumerable<IGrouping<int, Host>> GetHostsGroupsByHostingUnits()//groups hosts according to num of hosting units
        {
            return from item in dal.GetHosts()
                   orderby item.LastName, item.FirstName
                   group item by NumOfHostingUnits(item)
                       into g
                   orderby g.Key
                   select g;
        }
        IEnumerable<IGrouping<Area, HostingUnit>> GetHUGroupsByArea()//groups hosting units according to area
        {
            return from item in dal.GetAllHostingUnits()
                   orderby item.HostingUnitName
                   group item by item.area
                       into g
                   orderby g.Key
                   select g;
        }

        #endregion
    }
}
