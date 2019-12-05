using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Guest
    {
        //GuestRequestKey - in configuration
        public string FirstName { get; set; }//guest first name
        public string LastName { get; set; }//guest last name
        public string EmailAddress { get; set; }//guest email address
        //enum GuestStatus-in Enum
        public DateTime RegistrationDate { get; set; }//guest registration date to website
        public DateTime EntryDate { get; set; }//guests entry date
        public DateTime ReleaseDate { get; set; }//guests release date
        //enum type
       //enum Area
        public int Adults { get; set; }
        public int children { get; set; }
        //enum pool
        //enum Jacuzzi
        //enum Garden
        //enum ChildrensAttractions
        //enum wifi
        public override string ToString()
        { return FirstName + " " + LastName + "\n" + EmailAddress + "\n" + "Registered: " + RegistrationDate;}

    }
}
