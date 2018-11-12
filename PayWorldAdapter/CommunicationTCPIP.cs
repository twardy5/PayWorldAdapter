
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
    internal class CommunicationTCPIP
    {
        private static readonly log4net.ILog log =
                  log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string SendingFilePath = string.Empty;
        private const int BufferSize = 1024;
        public string Status = string.Empty;
        public Thread T = null;
        //TcpClient client = null;
        internal CommunicationTCPIP(string ip, Int32 port)
        {
            Hostname = ip;
            Port = port;
        }

        public event EventHandler<MessageEventArgs> MessageRecivedEvent;

        protected virtual void OnMessageRecived(MessageEventArgs e)
        {
            MessageRecivedEvent?.Invoke(this, e);
        }

        internal void StartReceiving(string hostname, string port)
        {
            Port = Convert.ToInt32(port);
            Hostname = hostname;
            read = true;
            Thread Ts = new Thread(() => Listen(null));
            Ts.Start();
        }


        internal List<String> SendData(byte[] messageBytes,string posID)
        {
            List<String> messages = new List<String>();
            NetworkStream netstream = null;
            try
            { 
                var client = new CommunicationClient(posID,Hostname, Port);
                client.SendTimeout = 2000;
                netstream = client.GetStream();

                StartReceiving(client);
                netstream.Write(messageBytes, 0, (int)messageBytes.Length);

                Thread.Sleep(50);
                log.Info(String.Format("PosID {0}, Data Sent to {1}:{2}:{3}",posID, Hostname, Port, client.UniqueID));

            }


            catch (Exception e)
            {
                log.Error(String.Format("PosID: {0}, {1}", posID,e));

            }
            return messages;
        }
        internal void StartReceiving(CommunicationClient clientSocket)
        {
            read = true;
            Thread Ts = new Thread(() => Listen(clientSocket));
            Ts.Start();
           

        }
     
        private List<String> receiveMessageFromVPJ(byte[] netstream,string posID)
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
                log.Error(String.Format("PosID: {0}, {1}",posID,e));
            }
            return null;
        }

        private bool read;
        private string Hostname;
        internal void ListenTest(CommunicationClient clientSocket) {
            Thread.Sleep(5000);
        }
        internal void Listen(CommunicationClient clientSocket)
        {
            try
            {

                if (clientSocket != null && clientSocket.Connected)

                    Console.WriteLine(" >> Server Started");
                var startTime = DateTime.Now;
                try
                {
                    var netstream = clientSocket.GetStream();

                    while (read)
                    {

                        try
                        { 
                        
                            if (clientSocket.Connected)
                            {
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
                                        var messages = receiveMessageFromVPJ(ms.ToArray(),clientSocket.UniqueID);
                                        log.Info(String.Format("PosID: {0}, Data recived from {1}:{2}, message count {3}",clientSocket.UniqueID, Hostname, Port, messages.Count));
                                        foreach (var msg in messages)
                                        {
                                            log.Info(String.Format("PosID: {0},Event: Data recived  {1}", clientSocket.UniqueID, msg));
                                            OnMessageRecived(new MessageEventArgs(msg,clientSocket.UniqueID));
                                        }
                                    }
                                }
                            }
                            else
                            {

                            }

                            if ((DateTime.Now - startTime).Seconds > 30)
                            {
                                read = false;
                                log.Info(String.Format("PosID: {0}, Listen Thread end at {1} ", clientSocket.UniqueID, DateTime.Now));
                            }
                        }
                        catch (Exception ei)
                        {
                            read = false;
                            log.Error(String.Format("PosID: {0}, {1} ", clientSocket.UniqueID,ei.ToString()));

                        }
                        Thread.Sleep(100);
                    }

                    netstream.Close();
                }
                
                catch (ThreadAbortException)
                {
                    Console.WriteLine(" >> Server Ended");
                    T = null;
                }
                clientSocket.Close();
            }

            catch (Exception e)
            {


            }

        }
      
        public void StopReciving()
        {
            read = false;
            Thread.Sleep(100);
            //T.Abort();
           
        }

        private int Port;
    }
}
