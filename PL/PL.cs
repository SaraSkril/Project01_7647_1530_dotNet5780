﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class PL
    {
        static void Main(string[] args)
        {
            #region add
            IBL ibl = FactoryBl.GetBL();
            Console.WriteLine("hello");
            Guest guest1 = new Guest();
            {
                guest1.ID = "123456789";
                guest1.FirstName = "moo";
                guest1.LastName = "parah";
                guest1.EmailAddress = "parah@gmail.com";
                guest1.GuestStatus = Status.Not_Treated;
                guest1.RegistrationDate = DateTime.Now;
                guest1.EntryDate = new DateTime(2020, 8, 1);
                guest1.ReleaseDate = new DateTime(2020, 8, 5);
                guest1.TypeUnit = TypeUnit.Hotel;
                guest1.Area = Area.Jerusalem;
                guest1.Adults = 2;
                guest1.Children = 5;
                guest1.Pool = Pool.Interested;
                guest1.Jacuzzi = Jacuzzi.Maybe;
                guest1.Garden = Garden.NotIntersted;
                guest1.ChildrensAttractions = ChildrensAttractions.Interested;
                guest1.Wifi = Wifi.Interested;

            }
            try
            {
                ibl.AddGuestReq(guest1);
            }
            catch (DuplicateWaitObjectException e)
            {
                Console.WriteLine(e.Message);
            }
            Host host1 = new Host();
            {
                host1.ID = "319031530";
                host1.FirstName = "Sara Raizel";
                host1.LastName = "Skriloff";
                host1.PhoneNumber = 0527148093;
                host1.EmailAddress = "srskriloff@gmail.com";
                host1.BankDetails = new BankAccount() { BankName = "Poalim", BankNumber = 12, BranchAddress = "Kanfei Nesharim 55", BranchCity = "Jerusalem", BranchNumber = 446 };
                host1.BankAccountNumber = 11245;
                host1.CollectionClearance = CollectionClearance.Yes;
            }
            HostingUnit hostingUnit1 = new HostingUnit();
            {
                hostingUnit1.HostingUnitName = "Ramada2";
                hostingUnit1.Owner = host1;
                hostingUnit1.area = Area.Jerusalem;
                hostingUnit1.pool = false;
                hostingUnit1.Jacuzzi = false;
                hostingUnit1.Garden = false;
                hostingUnit1.ChildrensAttractions = true;
                hostingUnit1.Wifi = true;
                hostingUnit1.TypeUnit = TypeUnit.Hotel;

            }
            try
            {
                ibl.AddHostingUnit(hostingUnit1);
            }
            catch (DuplicateWaitObjectException e)
            {
                Console.WriteLine(e.Message);
            }
            Order order1 = new Order();
            {
                order1.GuestRequestKey = guest1.GuestRequestKey;
                order1.HostingUnitKey = hostingUnit1.HostingUnitKey;
                order1.OrderDate = DateTime.Now;
                order1.Status = Status.Active;
            }
            try
            {
                ibl.AddOrder(order1);
            }
            catch (DuplicateWaitObjectException e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion
            #region Update
            guest1.Children = 10;

            try
            {
                ibl.UpdateGuestReq(guest1);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            hostingUnit1.pool = true;
            try
            {
                ibl.UpdateHostUnit(hostingUnit1);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            order1.Status = Status.Not_Treated;
            try
            {
                ibl.UpdateOrder(order1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            #endregion
        }
    }
}
