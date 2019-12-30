
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

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
        #region Add
        public void AddGuestReq(Guest guest)//Adds a new Guest Request
        {
            if (!CheckDate(guest.EntryDate, guest.ReleaseDate))
                throw new ArgumentOutOfRangeException("Dates are not valid/n ");

            if (checkID(guest.ID) == false)
                throw new ArgumentOutOfRangeException("ID not valid\n");
            if (checkEmail(guest.EmailAddress) == false)
                throw new ArgumentOutOfRangeException("Invalid Email Address\n");
            try
            {
                dal.AddGuestReq(guest.Clone());
            }
            catch (DuplicateWaitObjectException s)
            {
                throw s;
            }

        }
        public void AddHostingUnit(HostingUnit hostingUnit)//Adds new Hosting unit;
        {
            if (checkID(hostingUnit.Owner.ID))
            {
                try
                {
                    dal.AddHostingUnit(hostingUnit.Clone());
                }
                catch (DuplicateWaitObjectException e)
                {
                    throw e;
                }
            }
            else
            {
                throw new KeyNotFoundException("Owner ID  is not valid/n");
            }
        }
        public void AddOrder(Order order)//adds a new order
        {
            HostingUnit hosting = dal.GetHostingUnit(order.HostingUnitKey);
            if (hosting == null)
                throw new KeyNotFoundException("Invalid Hosting Unit");
            Guest guest = dal.GetGuest(order.GuestRequestKey);
            if (guest == null)
                throw new KeyNotFoundException("Invalid Guest");
            order.OrderKey=++Configuration.OrderKey;
            order.Status = Status.Active;
            order.CreateDate = DateTime.Now;
            
            try
            {
                dal.AddOrder(order.Clone());
            }
            catch (DuplicateWaitObjectException e)
            {
                throw e;
            }

        }
        #endregion
        #region Update
        public void UpdateGuestReq(Guest guest)//Updates guest
        {
            if (!CheckDate(guest.EntryDate, guest.ReleaseDate))
                throw new ArgumentOutOfRangeException("Dates are not valid/n ");
            if (checkID(guest.ID) == false)
                throw new ArgumentOutOfRangeException("ID not valid/n");
            try
            {
                dal.UpdateGuestReq(guest.Clone());
            }
            catch(KeyNotFoundException e)
            {
                throw e;
            }
        }
        public void UpdateHostUnit(HostingUnit hostingUnit)//Updates Hosting Unit;
        {
            if (checkID(hostingUnit.Owner.ID))
                try
                {
                    dal.UpdateHostUnit(hostingUnit.Clone());
                }
                catch (DuplicateWaitObjectException e)
                {
                    throw e;
                }
            else
                throw new ArgumentOutOfRangeException("Owner ID  is not valid\n");
        }
        public void UpdateOrder(Order order)//Updates Order
        {
            Order orig = GetAllOrders().FirstOrDefault(t => t.OrderKey == order.OrderKey);
            if ((orig.Status == Status.Closed_ClientRequest || orig.Status == Status.Closed_NoReply) && orig.Status != order.Status)
                throw new Exception("Status cannot be changed");
            if ((orig.Status == Status.Closed_ClientRequest || orig.Status == Status.Closed_NoReply) && orig.Status == order.Status)
                try
                {
                    dal.UpdateOrder(order.Clone());

                }
                catch (KeyNotFoundException e)
                {
                    throw e;
                }
            if (orig.Status == Status.Mail_Sent && order.Status == Status.Mail_Sent)
                try
                {
                    dal.UpdateOrder(order.Clone());

                }
                catch (KeyNotFoundException e)
                {
                    throw e;
                }
            if (order.Status == Status.Closed_NoReply)
                try
                {
                    Guest guest = dal.GetGuest(order.GuestRequestKey);
                    guest.GuestStatus = Status.Closed_NoReply;
                    dal.UpdateOrder(order.Clone());

                }
                catch (KeyNotFoundException e)
                {
                    throw e;
                }
            if (order.Status == Status.Closed_ClientRequest)
            {
                Guest guest = dal.GetGuest(order.GuestRequestKey);
                guest.GuestStatus = Status.Closed_ClientRequest;
                UpdateGuestReq(guest);
                Charge(FindHost(order.HostingUnitKey), DaysBetween(guest.EntryDate, guest.ReleaseDate));//charges the host 10 nis 
                HostingUnit tmp = dal.GetHostingUnit(order.HostingUnitKey);
                if (!CheckOffDates(tmp, guest.EntryDate, guest.ReleaseDate))
                    throw new TaskCanceledException("could not book dates");
               UpdateHostUnit(tmp.Clone());//need if we figure out how to clone diary
                foreach (Order order1 in dal.GetAllOrders())//closes all orders that are open for this guest
                {
                    if (order1.GuestRequestKey == guest.GuestRequestKey)
                        order1.Status = Status.Closed_ClientRequest;
                }
            }
            if (order.Status == Status.Mail_Sent)
            {
                HostingUnit hosting = dal.GetHostingUnit(order.HostingUnitKey);
                if (!CheckIsBankAllowed(hosting.Owner, order))
                    throw new TaskCanceledException("Cannot send mail. No debit authorization");
     
                Guest guest = dal.GetGuest(order.GuestRequestKey);
                guest.GuestStatus = Status.Mail_Sent;
                UpdateGuestReq(guest);
                SendMail(order);
                order.OrderDate = DateTime.Now;
            }
            try
            {
                dal.UpdateOrder(order.Clone());

            }
            catch (KeyNotFoundException e)
            {
                throw e;
            }

        }
        #endregion
        #region Delete

        public void DelHostingUnit(HostingUnit hosting)//Deletes Hosting Unit
        {
            if (!IsHostingUnitActive(hosting))
                try
                {
                    dal.DelHostingUnit(hosting.HostingUnitName);
                }
                catch (KeyNotFoundException e)
                {
                    throw e;
                }
            throw new TaskCanceledException("Hosting Unit cannot be deleted\n");
        }
        #endregion
        #region GetAll
        public List<HostingUnit> GetAllHostingUnits()//returns a lists with all hosting unit
        {
            return dal.GetAllHostingUnits();
        }
        public List<Guest> GetAllGuests()//returns a list with all Guests
        {
            return dal.GetAllGuests();
        }
        public List<Order> GetAllOrders()//returns a list with all orders
        {
            return dal.GetAllOrders();
        }
        public IEnumerable<BankAccount> GetAllBankAccounts()//returns a list with all Bank Accounts 
        {
            return dal.GetAllBankAccounts();
        }
        #endregion
        #region check
        public bool checkEmail(string email)
        {
            if (!email.Contains('@'))
                return false;
            if (!email.Contains('.'))
                return false;
            return true;
        }
        public bool checkID(string id)
        {
            if (Int32.Parse(id) < 100000000 || Int32.Parse(id) > 999999999)
                return false;
            return true;
        }
        public bool CheckDate(DateTime start, DateTime end)//check if end day is longer than 1 day by start
        {
            DateTime temp = start.AddDays(1);
            if (temp > end)
                return false;
            return true;
        }
        public bool CheckIsBankAllowed(Host host, Order order)//check if host allows access to bank acct, if so sends email else returns false 
        {
            if (host.CollectionClearance == CollectionClearance.Yes)//checks if theres access to bank acct 
                return true;
            return false;

        }
        public bool IsAvailible(HostingUnit hostingUnit, DateTime start, DateTime end)//checks if dates in hosting unit are availble 
        {
            DateTime tempstart = start;
            while (tempstart != end)
            {
                if (hostingUnit.Diary[tempstart.Month-1, tempstart.Day-1] == true)//if date accupied
                    return false;
                tempstart=tempstart.AddDays(1);
            }
            return true;//dates were available
        }
        public bool CheckifOrderIsClosed(Order order)//checks if status can be changed
        {
            if (order.Status == Status.Closed_ClientRequest || order.Status == Status.Closed_NoReply)//order closed
                return true;//status cannot be changed
            return false;
        }
        public bool Charge(Host host, int numdays)//when order is closed we charge the owner 10 nis
        {
            try
            {
                host.commission += Configuration.commission * numdays;
                return true;
            }
            catch
            {
                throw new TaskCanceledException(" Not able to charge comission");
            }

        }
        public Host FindHost(int key)//recieves hosting unit key and returns the host that ownes it
        {
            HostingUnit hostingUnit = dal.GetHostingUnit(key);
            return hostingUnit.Owner;//returns owner
        }
        public bool CheckOffDates(HostingUnit hostingUnit, DateTime start, DateTime end)//when status is changed to closed, update diary in hosting unit 
        {
            if (IsAvailible(hostingUnit, start, end) == false)
                return false;
            
            DateTime tempstart = start;
            while (tempstart != end)
            {
                hostingUnit.Diary[tempstart.Month-1, tempstart.Day-1] = true;//marks that date is accupied

                tempstart=tempstart.AddDays(1);
            }
            return true;//dates were available

        }

        public bool IsHostingUnitActive(HostingUnit hostingUnit)//checks if hosting unit is active in any order, if so w cant delete
        {

            var orders = from order in dal.GetAllOrders()//gets all orders from this unit
                         where order.HostingUnitKey.Equals(hostingUnit.HostingUnitKey)
                         select order;
            foreach (var ord in orders)//checks if any order is active if so returns false so we cant delete the hosting unit
            {
                if (ord.Status == Status.Active || ord.Status == Status.Closed_ClientRequest)
                    return true;
            }
            return false;

        }
        public bool ChangeCollectionClearance(HostingUnit hostingUnit)//checks if theres a open order, if so we cannot change collection clearance
        {
            if (IsHostingUnitActive(hostingUnit))//if hosting unit is in a active status then we cant change collection clearance 
                return false;
            hostingUnit.Owner.CollectionClearance = CollectionClearance.No;//change collection clearence
            return true;
        }
        public void SendMail(Order order)//when status of order is changed to "sent mail", this function will send the mail
        {
            Console.WriteLine(" Email sent");
        }
        public List<HostingUnit> AllDays(DateTime date, int duration)//returns all hosting units with avilble date
        {
            DateTime end = date.AddDays(duration);
            var result = from item in dal.GetAllHostingUnits()//selects all hosting units with available dates  into a list
                         where IsAvailible(item, date, end) == true
                         select item;
            return (List<HostingUnit>)result;//returns the list
        }
        public int DaysBetween(DateTime start/*end=now*/)//returns the days between the start day and now 
        {
            DateTime now = DateTime.Now;
            return (now - start).Days;

        }
        public int DaysBetween(DateTime start, DateTime end)//returns the days between the start and last day
        {
            return (end - start).Days;
        }
        public List<Order> DaysFromOrder(int num)//returns all orders that "num"days have past since they were created / sent email (num or bigger)
        {
            var result = from item in dal.GetAllOrders()
                         where DaysBetween(item.OrderDate) >= num
                         select item;
            return (List<Order>)result;

        }
        public int NumOfVacationers(Guest g)//recieves a guest and returns num of vacationers
        {
            return g.Adults + g.Children;
        }
        public int NumOfHostingUnits(Host h)//recieves a host and returns num of hosting units he ownes
        {
            int count = 0;
            foreach (HostingUnit unit in dal.GetAllHostingUnits())
            {
                if (unit.Owner.ID == h.ID)
                    count++;
            }
            return count;

        }
        public bool HostExist(Host host)
        {
            Host host1 = dal.GetHost(host.ID);
            if (host1 != null)
                return true;
            return false;


        }
        public bool GuestExist(Guest guest)
        {
            Guest guest1 = dal.GetGuest(guest.ID);
            if (guest1 != null)
                return true;
            return false;
        }
        public bool HostingUnitExist(HostingUnit hostingUnit)
        {
            HostingUnit unit = dal.GetHostingUnit(hostingUnit.HostingUnitName);
            if (unit != null)
                return true;
            return false;
        }

        //Func -דלגייט שקיים  מקבל משהו ומחזיר משהו
        //predicate-בודק אם תנאי מסוים שמציבים בו מתקיים
        // IEnumeable מאפשר לנו לעבור על איברי האובייקט שלנו, לסדר אותם, לסנן אותם או סתם לדלות מהם מידע.
        public IEnumerable<Guest> GetAllGuests(Func<Guest, bool> predicate = null)//recieves a predicate and returns all guests that  satisfy the predicate condition
        {

            var result = from item in dal.GetAllGuests()
                         where predicate(item)
                         select item;
            return result;
        }
        public int NumForGuest(Guest guest)//counts and returns how many orders have been sent to him
        {
            int count = 0;
            foreach (Order item in dal.GetAllOrders())
            {
                if (item.GuestRequestKey == guest.GuestRequestKey)
                    count++;
            }
            return count;


        }
        public int NumForUnit(HostingUnit hostingUnit)//counts how many orders were  closed/sent for hosting unit
        {
            int count = 0;
            foreach (Order item in dal.GetAllOrders())
            {
                if (item.HostingUnitKey == hostingUnit.HostingUnitKey &&
                    item.Status == Status.Closed_ClientRequest || item.HostingUnitKey == hostingUnit.HostingUnitKey &&
                    item.Status == Status.Mail_Sent)
                    count++;
            }
            return count;
        }
        public int GetGuestKeyByID(string id)
            {
            Guest g= dal.GetGuest(id);
            return g.GuestRequestKey;
             }

        public int GetHUkeyBuName(string name)
            {
             HostingUnit h=dal.GetHostingUnit(name);
            return h.HostingUnitKey;
          
           }
        #endregion
        #region Group
        public IEnumerable<IGrouping<Area, Guest>> GetGuestsGroupsByArea()//groups geusts according to area
        {
            return from item in dal.GetAllGuests()
                   orderby item.LastName, item.FirstName
                   group item by item.Area
                         into g
                   orderby g.Key
                   select g;

        }
        public IEnumerable<IGrouping<int, Guest>> GetGuestsGroupsByVacationers()//groups guests according to num vacation
        {
            return from item in dal.GetAllGuests()
                   orderby item.LastName, item.FirstName
                   group item by NumOfVacationers(item)
                       into g
                   orderby g.Key
                   select g;
        }
        public IEnumerable<IGrouping<int, Host>> GetHostsGroupsByHostingUnits()//groups hosts according to num of hosting units
        {
            return from item in dal.GetHosts()
                   orderby item.LastName, item.FirstName
                   group item by NumOfHostingUnits(item)
                       into g
                   orderby g.Key
                   select g;
        }
        public IEnumerable<IGrouping<Area, HostingUnit>> GetHUGroupsByArea()//groups hosting units according to area
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
