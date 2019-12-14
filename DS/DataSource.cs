using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {
        //cant figure out how to use the enum objects
         List<Guest> guests = new List<Guest> (); new List<Guest> { new Guest { FirstName = "Elisheva", LastName = "Aronstam", EmailAddress = "eli7@gmail.com", GuestStatus = new Enums.GuestStatus {/*how do we initialize enum*/},*/
            RegistrationDate =new DateTime(12/10/2019), EntryDate=new DateTime(12/10/2019),ReleaseDate=new DateTime(12/15/2019),
        Type=,Area=0,Adults=1,Children=0,Pool = 0,Jacuzzi=new Enums.Jacuzzi{ }, Garden=new Enums.Garden{ },
        ChildrensAttractions= new Enums.ChildrensAttractions{ }, Wifi= new Enums.Wifi{ } },/**/
      /*  new Guest { FirstName = "Sara", LastName = "Skriloff", EmailAddress = "sara@gmail.com", GuestStatus = new Enums.GuestStatus {/*how do we initialize enum*/},
            /*RegistrationDate =new DateTime(10/10/2019), EntryDate=new DateTime(11/10/2019),ReleaseDate=new DateTime(12/15/2019),
        Type=new Enums.Type{},Area=new Enums.Area{ },Adults=1,Children=0,Pool = new Enums.Pool{},Jacuzzi=new Enums.Jacuzzi{ }, Garden=new Enums.Garden{ },
        ChildrensAttractions= new Enums.ChildrensAttractions{ }, Wifi= new Enums.Wifi{ } }};*/
        private static List<Host> hosts = new List<Host> { };
        private static List<Order> orders = new List<Order> { };
       

    }
}
