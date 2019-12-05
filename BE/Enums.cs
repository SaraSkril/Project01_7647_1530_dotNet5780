using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Enums
    {
        enum GuestStatus//status of guest
        {
            Active,Archive
        }
        enum Area//destination area
        {
            All,North,South,Center,Jerusalem
        }
        //enum SubArea
        //{

        //}
        enum Type//type of guest unit
        {
            Zimmer,Hotel,CampingSite,AirBNB
        }
        enum Pool//if guest wants pool
        {
         Interested, NotIntersted, Maybe
        }
        enum Jacuzzi//if guest wants jacuzzi
        {
            Interested, NotIntersted, Maybe
        }
        enum Garden//if guest wants garden
        {
            Interested, NotIntersted, Maybe
        }
        enum ChildrensAttractions//if guest wants child attractions
        {
            Interested, NotIntersted, Maybe
        }
        enum Wifi//if guest wants wifi
        {
            Interested, NotIntersted, Maybe
        }
        enum CollectionClearance //if host lets access to bank accout to withdrawl money
        {
            Yes,No
        }
    }
}
