﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankAccount
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        ///public int BankAccountNumber { get; set; }
    
        public override string ToString()
        {
            /* return "Bank Name: "+ BankName+"\n"+BranchAddress+ ", " + BranchCity + "\n" +"Bank Number: "+BankAccountNumber+ "Branch Number: "+BranchNumber
                 + "Account: "+BankAccountNumber+"\n";*/
            return this.ToStringProperty();
        }

    }
}
 