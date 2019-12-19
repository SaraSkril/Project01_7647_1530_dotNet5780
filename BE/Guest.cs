using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Guest
    {
        //private Enums.GuestStatus GuestStatus1;
        //private Enums.Type Type1;
        //private Enums.Area Area1;
        //private Enums.Pool Pool1;
        //private Enums.Jacuzzi Jacuzzi1;
        //private Enums.Garden Garden1;
        //private Enums.ChildrensAttractions ChildrensAttractions1;
        //private Enums.Wifi Wifi1;
        //GuestRequestKey - in configuration
     
        static int GuestRequestKey1 = 10000000;//guest serial number

        public static int GuestRequestKey
        {
            get { return GuestRequestKey1; }
            set { GuestRequestKey1 = value; }
        }
       // public static int GuestRequestKey { get; set; }//check configu and initialize
       public string ID { get; set; }
        public string FirstName { get; set; }//guest first name
        public string LastName { get; set; }//guest last name
        public string EmailAddress { get; set; }//guest email address
        public GuestStatus GuestStatus { get; set; }
        public DateTime RegistrationDate { get; set; }//guest registration date to website
        public DateTime EntryDate { get; set; }//guests entry date
        public DateTime ReleaseDate { get; set; }//guests release date
        public TypeUnit TypeUnit { get; set; } //enum type
        public Area Area { get; set; }//enum Area
        public int Adults { get; set; }
        public int Children { get; set; }
        public Pool Pool { get; set; }//enum pool
        public Jacuzzi Jacuzzi { get; set; }//enum Jacuzzi
        public Garden Garden { get; set; }//enum Garden
        public ChildrensAttractions ChildrensAttractions { get; set; }//enum ChildrensAttractions
        public Wifi Wifi { get; set; }//enum wifi
        public Guest(string id)
        {
            ID = id;
        }
        public override string ToString()
        { return FirstName + " " + LastName + "\n" + EmailAddress + "\n" + "Registered: " + RegistrationDate;}

    }
}
