
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayWorldAdapter;

namespace PayWorldConnectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            VPJClient client = new VPJClient("192.168.2.96", 50000);
            client.SendAmout("2003", 1 *100,CurrencyType.Item978);
        }
    }
}
