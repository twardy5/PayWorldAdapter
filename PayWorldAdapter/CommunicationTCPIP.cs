
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

namespace PayWorldAdapter
{
    class CommunicationTCPIP
    {
        private static readonly log4net.ILog log =
                  log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

       
        
        internal void StartReceiving(string hostname,string port)
        {
            Port = Convert.ToInt32(port);
            Hostname = hostname;
            read = true;
            Thread Ts = new Thread(() => Listen(null));
            Ts.Start();
        }
        internal void StartReceiving(TcpClient clientSocket)
        {
            read = true;
            Thread Ts = new Thread(() => Listen(clientSocket));
            Ts.Start();
           
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

                StartReceiving(client);
                netstream.Write(messageBytes, 0, (int)messageBytes.Length);

                Thread.Sleep(50);

                // byte[] RecData = new byte[client.ReceiveBufferSize];
                log.Info(String.Format("Data Sent to {0}:{1}",Hostname,Port));


                //while (!netstream.DataAvailable)
                //{

                //}

                //if (netstream.DataAvailable)
                //{
                //    //string SaveFileName = @"C:\Data\Payworld Interface\Recived_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".xml";
                //    //FileStream Fs = new FileStream(SaveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                //    byte[] data = new byte[1024*10];
                //    using (MemoryStream ms = new MemoryStream())
                //    {

                //        int numBytesRead;
                //        numBytesRead = netstream.Read(data, 0, data.Length);
                //        {
                //            ms.Write(data, 0, numBytesRead);
                //        }
                //        messages = receiveMessageFromVPJ(ms.ToArray());
                //        log.Info(String.Format("Data recived from {0}:{1}, message count {2}", Hostname, Port,messages.Count));
                //    }

                //}


                //RecBytes = netstream.Read(RecData, 0, RecData.Length);

                //Fs.Write(RecData, 0, RecBytes);


                //Fs.Close();


                //netstream.Close();
                //client.Close();
            }


            catch (Exception e)
            {
                log.Error(e);

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
                    MessageLenght = PayWorldAdapter.Util.Helper.ToInt32(lengthBytes, 0);

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
                log.Error(e);
            }
            return null;
        }

        private bool read;
        private string Hostname;
        public void Listen(TcpClient clientSocket)
        {
            try
            {

                if (clientSocket == null)
                    clientSocket = default(TcpClient);
            
       
            Console.WriteLine(" >> Server Started");
            try
            {
                while (read)
                {
                    if (clientSocket.Connected)
                    {

                            var netstream = client.GetStream();
                            //string SaveFileName = @"C:\Data\Payworld Interface\Recived_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".xml";
                            //FileStream Fs = new FileStream(SaveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                            if (netstream.DataAvailable)
                            {
                                byte[] data = new byte[1024 * 10];
                                using (MemoryStream ms = new MemoryStream())
                                {

                                    int numBytesRead;
                                    numBytesRead = netstream.Read(data, 0, data.Length);
                                    {
                                        ms.Write(data, 0, numBytesRead);
                                    }
                                    var messages = receiveMessageFromVPJ(ms.ToArray());
                                    log.Info(String.Format("Data recived from {0}:{1}, message count {2}", Hostname, Port, messages.Count));
                                    foreach (var msg in messages)
                                    {
                                        log.Info(String.Format("Data recived  {0}", msg));

                                    }
                                }
                            }
                        }
                        else
                    {
                       
                    }
                Thread.Sleep(100);
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

        private int Port;
    }
}
