using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWorldConnection
{
    interface IPOSMessages
    {
        string SendAmout(string posID,double amount, CurrencyType curency);
    }
}
