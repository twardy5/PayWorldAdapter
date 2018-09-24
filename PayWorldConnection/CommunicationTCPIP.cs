
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PayWorldConnection
{
    class CommunicationTCPIP
    {
       // private static readonly log4net.ILog log =
        //           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string SendingFilePath = string.Empty;
        private const int BufferSize = 1024;
        public string Status = string.Empty;
        public Thread T = null;
        TcpClient client = null;
        public  CommunicationTCPIP(string ip, Int32 port)
        {
            Hostname = ip;
            Port = port;
        }

        public void SendByTCPIP(string message, string ip, Int32 port)
        {
            try
            {
                SendTCP(message, ip, port);
            }
            catch (Exception e)
            {

                 
            }

        }
        
        internal void StartReceiving(string hostname,string port)
        {
            ListingPort = Convert.ToInt32(port);
            Hostname = hostname;
            read = true;
            //ThreadStart Ts = new ThreadStart(StartReceiving);
            ThreadStart Ts = new ThreadStart(Listen);
            T = new Thread(Ts);
            T.Start();
        }

        internal List<String> SendData(byte[] messageBytes)
        {
            List<String> messages = new List<String>();
            NetworkStream netstream = null;
            try
            {

                if (client == null )
                    client = new TcpClient(Hostname, Port);
                client.SendTimeout = 2000;
                netstream = client.GetStream();
                netstream.Write(messageBytes, 0, (int)messageBytes.Length);

                Thread.Sleep(50);

                // byte[] RecData = new byte[client.ReceiveBufferSize];
                //log.Info(String.Format("Data Sent to {0}:{1}",Hostname,Port));
                while (netstream.DataAvailable)
                {
                    //string SaveFileName = @"C:\Data\Payworld Interface\Recived_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".xml";
                    //FileStream Fs = new FileStream(SaveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    byte[] data = new byte[1024];
                    using (MemoryStream ms = new MemoryStream())
                    {

                        int numBytesRead;
                        numBytesRead = netstream.Read(data, 0, data.Length);
                        {
                            ms.Write(data, 0, numBytesRead);
                        }
                        messages = receiveMessageFromVPJ(ms.ToArray());
                        //log.Info(String.Format("Data recived from {0}:{1}, message count {2}", Hostname, Port,messages.Count));
                    }
                    
                }
                

                //RecBytes = netstream.Read(RecData, 0, RecData.Length);
                
                //Fs.Write(RecData, 0, RecBytes);
               
                
               //Fs.Close();
               

                netstream.Close();
                client.Close();
            }


            catch (Exception e)
            {
                //log.Error(e);

            }
            return messages;
        }

        private List<String> receiveMessageFromVPJ(byte[] netstream)
        {
            //int receiveOneMessageTimeoutMilliseconds = 30000;
            try
            {
                              
                List<String> messages = new List<String>();
                //ByteArrayOutputStream messageBuffer = new ByteArrayOutputStream();
                byte[] RecData = new byte[4];
                byte[] lengthBytes = new byte[4];
                int index = 0;
                do
                {
                    String posMessage = "";
                    int MessageLenght = 0;
                    //messageBytes = this.vpjConnection.receive(4, receiveOneMessageTimeoutMilliseconds, true);
                    System.Array.Copy(netstream, index, lengthBytes, 0, 4);
                  
                    //int lengthBytesFromMessage = Math.min(4 - index, messageBytes.length);
                    int lengthBytesFromMessage = Math.Min(4, lengthBytes.Length);
                    index = index + lengthBytes.Length;
                    MessageLenght = PayWorldConnection.Util.Helper.ToInt32(lengthBytes, 0);

                    if (lengthBytesFromMessage == 4 && MessageLenght > 0)
                    {
                        int messageLength = MessageLenght;
                        RecData = new byte[messageLength];
                        System.Array.Copy(netstream, index, RecData, 0, messageLength);
                       
                        index = index + RecData.Length;
                        string xml = System.Text.UTF8Encoding.UTF8.GetString(RecData);
                        posMessage = xml;
                        messages.Add(posMessage);
                    }
                } while (index < netstream.Length);
                return messages;
            }
             
            catch (Exception e)
            {
                //log.Error(e);
            }
            return null;
        }

        private void SendTCP(string message, string IPA, Int32 PortN) {
            byte[] SendingBuffer = null;
            TcpClient client = null;
           
            NetworkStream netstream = null;
            try
            {
                
                client = new TcpClient(IPA, PortN);
                client.SendTimeout = 2000;
                netstream = client.GetStream();
            FileStream Fs = new FileStream(message, FileMode.Open, FileAccess.Read);
            

                int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Fs.Length) / Convert.ToDouble(BufferSize)));

                int TotalLength = (int)Fs.Length, CurrentPacketLength;
                for (int i = 0; i < NoOfPackets; i++)
                {
                    if (TotalLength > BufferSize)
                    {
                        CurrentPacketLength = BufferSize;
                        TotalLength = TotalLength - CurrentPacketLength;
                    }
                    else
                        CurrentPacketLength = TotalLength;
                    SendingBuffer = new byte[CurrentPacketLength];
                    Fs.Read(SendingBuffer, 0, CurrentPacketLength);
                    netstream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                   
                }
                Fs.Close();
            }
            catch (Exception e)
            {

                
            }
        }
        private void SendTCPMessage(string message, string IPA, Int32 PortN)
        {
            byte[] SendingBuffer = null;
            TcpClient client = null;
            int counter = 0;
            NetworkStream netstream = null;
            try
            {
                
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(message);
                string path = @"C:\Data\Payworld Interface\sendXML.xml";
                doc.Save(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                netstream.Close();
                client.Close();

            }
        }

 

        private bool read;
        private string Hostname;
        public void Listen()
        {
            try
            {

           IPAddress ipAddress = Dns.Resolve("localhost").AddressList[0];
            TcpListener serverSocket = new TcpListener(ipAddress, ListingPort);
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine(" >> Server Started");
            try
            {
                while (read)
                {
                    if (serverSocket.Pending())
                    {
                        clientSocket = serverSocket.AcceptTcpClient();
                         
                            byte[] RecData = new byte[BufferSize];
                          
                            NetworkStream netstream = null;
                            netstream = clientSocket.GetStream();
                            Status = "Connected to a client\n";
                            // result = MessageBox.Show(message, caption, buttons);                        
                            var ReciveFileName = @"C:\Data\Payworld Interface\" + string.Format("RecvdFile-{0:yyyy-MM-dd_hh-mm-ss-tt}.xml", DateTime.Now);

                            if (ReciveFileName != string.Empty)
                            {
                                byte[] data = new byte[1024];
                                using (MemoryStream ms = new MemoryStream())
                                {

                                    int numBytesRead;
                                    while ((numBytesRead = netstream.Read(data, 0, data.Length)) > 0)
                                    {
                                        ms.Write(data, 0, numBytesRead);
                                    }

                                }

                            }



                            netstream.Close();

                        }
                    else
                    {
                       
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine(" >> Server Ended");
                T = null;
            }
            }
            catch (Exception e)
            {

               
            }
        }

        private void ProcessRequest(TcpClient client)
        {
            try
            {

                
            }
            catch (Exception e)
            {

             
            }
          
           

        }


        public void StopReciving()
        {
            read = false;
            //T.Abort();
           
        }
        int ListingPort ;
        private IPEndPoint HostName;
        private int Port;
    }
}
