using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public  class Order
    {

        private Status Status1;
        private static int OrderKey1;
        public int HostingUnitKey { get; set; }
        public static int GuestRequestKey { get; set; }
        public static int OrderKey        //check if need to put in configure
        {
            get { return OrderKey1; }
        }
        public Status Status{ get { return Status1; }set {Status1=value; } }//enum status
        public DateTime CreateDate { get; set; }//date order was created 
        public DateTime OrderDate { get; set; }//date email was sent  to client - guest
        public override string ToString()
        {
            return "Order Number: " + OrderKey + "\n" + "Guest ID: " + GuestRequestKey + "\n" + "Hosting Unit Number: " + HostingUnitKey + "\n"
                + "Order Placed: " + CreateDate + "\n" + "Email Sent: " + OrderDate;
        }

    }
}
