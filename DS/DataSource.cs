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
        static List<Guest> guests = new List<Guest> { new Guest { FirstName = "Elisheva", LastName = "Aronstam", EmailAddress = "eli7@gmail.com", GuestStatus = new Enums.GuestStatus {/*how do we initialize enum*/},
            RegistrationDate =new DateTime(12/10/2019), EntryDate=new DateTime(12/10/2019),ReleaseDate=new DateTime(12/15/2019),
        Type=new Enums.Type{},Area=new Enums.Area{ },Adults=1,Children=0,Pool = new Enums.Pool{},Jacuzzi=new Enums.Jacuzzi{ }, Garden=new Enums.Garden{ },
        ChildrensAttractions= new Enums.ChildrensAttractions{ }, Wifi= new Enums.Wifi{ } },/**/
        new Guest { FirstName = "Sara", LastName = "Skriloff", EmailAddress = "sara@gmail.com", GuestStatus = new Enums.GuestStatus {/*how do we initialize enum*/},
            RegistrationDate =new DateTime(10/10/2019), EntryDate=new DateTime(11/10/2019),ReleaseDate=new DateTime(12/15/2019),
        Type=new Enums.Type{},Area=new Enums.Area{ },Adults=1,Children=0,Pool = new Enums.Pool{},Jacuzzi=new Enums.Jacuzzi{ }, Garden=new Enums.Garden{ },
        ChildrensAttractions= new Enums.ChildrensAttractions{ }, Wifi= new Enums.Wifi{ } }};
        static List<Host> hosts = new List<Host> { };
        static List<Order> orders = new List<Order> { };
    }
}
