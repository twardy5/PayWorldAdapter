
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
            var posID = Guid.NewGuid().ToString();
            client.SendAmout(posID,1,CurrencyType.Item978);
            client.MessageRecivedEvent += Client_MessageRecivedEvent;
           
        }

        private static void Client_MessageRecivedEvent(MessageEventArgs e)
        {
            
        }
    }
}
