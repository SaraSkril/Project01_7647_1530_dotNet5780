using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Host
    {
        private Enums.CollectionClearance CollectionClearance1;
        public static int HostKey { get; }//check if need to put in configure
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public BankAccount BankDetails { get; set; }
        public Enums.CollectionClearance CollectionClearance { get { return CollectionClearance1; } set { CollectionClearance1 = value; } }
        public override string ToString()
        {
            return "Host ID: " + HostKey + "\n" + FirstName + " " + LastName + "\n" +
              "Phone Number: " + PhoneNumber + "\n" + "Email: " + EmailAddress;
        }

    }
}
