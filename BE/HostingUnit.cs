﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class HostingUnit
    {
        private static int HostingUnitKey1 = 100000;

        public int HostingUnitKey { get; set; }//check if need to put in configure
        public Host Owner { get; set; }//owner of hosting unit
        public string HostingUnitName { get; set; }//name of the hosting unit
        public bool[,] Diary = new bool[12, 31];//matrix of hosting unit status
        public Area area { get; set; }
        public TypeUnit TypeUnit { get; set; }
        public bool pool { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Garden { get; set; }
        public bool ChildrensAttractions { get; set; }
        public bool Wifi { get; set; }
        
        public override string ToString()
        {
            // return "Hosting unit key: " + HostingUnitKey + "\n" + "Unit Name: " + HostingUnitName + "\n" + Owner.ToString();
            return this.ToStringProperty();
        }

    }
}
