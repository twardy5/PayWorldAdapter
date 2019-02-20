using PayWorldAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            VPJClient client = new VPJClient();
            client.OpenConnection("10.0.0.24", 50000);
            var posID = Guid.NewGuid().ToString();
            //client.MessageRecivedEvent += Client_MessageRecivedEvent;
            // client.Ping(posID);
            //client.SendAmout(posID, 32323, 59, "123");
            var retValue = client.Ping("12121213");
        }
    }
}
