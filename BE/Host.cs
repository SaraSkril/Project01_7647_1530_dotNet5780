﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BE
{
   public class Host
    {
        private CollectionClearance CollectionClearance1;
        public string ID { get; set; }//ID 
        public static int HostKey { get; }//check if need to put in configure
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public BankAccount BankDetails { get; set; }
        public int BankAccountNumber { get; set; }
        public CollectionClearance CollectionClearance { get; set; }
        public override string ToString()
        {

            return this.ToStringProperty();
        }

    }
}
