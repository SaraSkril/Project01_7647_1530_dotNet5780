using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Guest
    {
        private Enums.GuestStatus GuestStatus1;
        private Enums.Type Type1;
        private Enums.Area Area1;
        private Enums.Pool Pool1;
        private Enums.Jacuzzi Jacuzzi1;
        private Enums.Garden Garden1;
        private Enums.ChildrensAttractions ChildrensAttractions1;
        private Enums.Wifi Wifi1;
        //GuestRequestKey - in configuration
     
        static int GuestRequestKey1 = 10000000;//guest serial number

        public static int GuestRequestKey
        {
            get { return GuestRequestKey1; }
            set { GuestRequestKey1 = value; }
        }
       // public static int GuestRequestKey { get; set; }//check configu and initialize
        public string FirstName { get; set; }//guest first name
        public string LastName { get; set; }//guest last name
        public string EmailAddress { get; set; }//guest email address
        public Enums.GuestStatus GuestStatus { get { return GuestStatus1; } set { GuestStatus1 = value; } }
        public DateTime RegistrationDate { get; set; }//guest registration date to website
        public DateTime EntryDate { get; set; }//guests entry date
        public DateTime ReleaseDate { get; set; }//guests release date
        public Enums.Type Type { get { return Type1; } set { Type1 = value; } } //enum type
        public Enums.Area Area { get { return Area1; } set { Area1 = value; } }//enum Area
        public int Adults { get; set; }
        public int children { get; set; }
        public Enums.Pool Pool { get { return Pool1; }set { Pool1 = value; } }//enum pool
        public Enums.Jacuzzi Jacuzzi { get { return Jacuzzi1;  }set { Jacuzzi1 = value; } }//enum Jacuzzi
        public Enums.Garden Garden { get { return Garden1; }set { Garden1 = value; } }//enum Garden
        public Enums.ChildrensAttractions ChildrensAttractions { get { return ChildrensAttractions1; }set { ChildrensAttractions1 = value; } }//enum ChildrensAttractions
        public Enums.Wifi Wifi { get { return Wifi1; }set { Wifi1 = value; } }//enum wifi
        public override string ToString()
        { return FirstName + " " + LastName + "\n" + EmailAddress + "\n" + "Registered: " + RegistrationDate;}

    }
}
