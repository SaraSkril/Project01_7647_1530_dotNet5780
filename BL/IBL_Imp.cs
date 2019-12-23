using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    class IBL_Imp : IBL
    {
        #region Guest
        public void AddGuestReq(Guest guest)
        {
            try
            {
               
            }
        }

        public void UpdateGuestReq(Guest guest)
        {
            throw new NotImplementedException();
        }

        public List<Guest> GetAllGuests()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region HostingUnit

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            throw new NotImplementedException();
        }

        public void DelHostingUnit(string name)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Order 

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }
        #endregion
        

        public IEnumerable<BankAccount> GetAllBankAccounts()
        {
            throw new NotImplementedException();
        }

        

        

        

       

      

        
    }
}
