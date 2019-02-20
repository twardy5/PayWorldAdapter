using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;


namespace PayWorldAdapter
{
    public class CommunicationClient: TcpClient
    {
        public CommunicationClient(string posID) : base()
        {

            UniqueID = posID;

        }
        public CommunicationClient(string posID ,string Hostname, int Port):base(Hostname,Port)
        {
           
            UniqueID = posID;
          
        }
        public string UniqueID { get; private set; }
    }
}
