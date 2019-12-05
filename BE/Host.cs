using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Host
    {
        public static int HostKey { get; }//check if need to put in configure
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public BankAccount BankDetails { get; set; }
        //enum CollectionClearance
        public override string ToString()
        {
            return "Host ID: " + HostKey + "\n" + FirstName + " " + LastName + "\n" +
              "Phone Number: " + PhoneNumber + "\n" + "Email: " + EmailAddress;
        }

    }
}
