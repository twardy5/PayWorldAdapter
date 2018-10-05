using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PayWorldAdapter
{
    public class CommunicationClient: TcpClient
    {
       
        public CommunicationClient(string posID ,string Hostname, int Port):base(Hostname,Port)
        {
           
            UniqueID = posID;
          
        }
        public string UniqueID { get; private set; }
    }
}
