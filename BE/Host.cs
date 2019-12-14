using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Host
    {
        private CollectionClearance CollectionClearance1;
        public static int HostKey { get; }//check if need to put in configure
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public BankAccount BankDetails { get; set; }
        public CollectionClearance CollectionClearance { get { return CollectionClearance1; } set { CollectionClearance1 = value; } }
        public override string ToString()
        {
            return "Host ID: " + HostKey + "\n" + FirstName + " " + LastName + "\n" +
              "Phone Number: " + PhoneNumber + "\n" + "Email: " + EmailAddress;
        }

    }
}
