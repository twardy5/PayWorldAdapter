
using PayWorldConnection.Extenssion;
using PayWorldConnection.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
 

namespace PayWorldConnection
{
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]

    public class VPJClient:IPOSMessages
    {
       // private static readonly log4net.ILog log = 
                    //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private CommunicationTCPIP communication;
        public VPJClient(string host,int port)
        {
            this.communication = new CommunicationTCPIP(host,port);
        }

        public  VPJClient()
        {
        }
        private int nPort;
        public void Connect(string host, int port)
        {
            this.communication = new CommunicationTCPIP(host, port);

        }
        public int Port
        {
            get { return nPort; }
            set { nPort = value; }
        }
        private string mHost;

        public string Host
        {
            get { return mHost; }
            set { mHost = value; }
        }

        public void sendFileToVPJ(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(fileName))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
                sendMessageToVPJ(sb.ToString());
            }
                
        }
        public List<String> sendMessageToVPJ(string messageString)
        {
            byte[] messageBytes;
            List<String> messages = new List<String>();
            try
            {
                //messageString = generateMessageString(posMessage, contentType);
            }
            catch (Exception e)
            {
                messageString = "";
                //log.Error(e);
            }
           
           // this.logger.info("Message to be send:\n" + (this.contentType.isXml() ? StringUtils.formatXml(messageString) : messageString));
            try
            {
               
                messageBytes = messageString.ToByteArrayUTF8();

            }
            catch (Exception e)
            {
                messageBytes = null ;
                //log.Error(e);
            }
            byte[] requestLength = CommonMethods.intToBigEndian(messageBytes.Length);
            messageBytes = requestLength.Combine(messageBytes);
            try
            {

                messages = communication.SendData(messageBytes);
            }
            catch (Exception e)
            {
                //log.Error(e);
            }
            return messages;
        }
        public void Test()
        {
            ///log.Info("Test");
        }
        public string SendAmout(string posID, double amount, CurrencyType curency)
        {
            //log.Info("Send Amount started");
            FinancialTrxRequest data = new FinancialTrxRequest { posId = posID,
                trxData = new TransactionData {
                    amount = (ulong)amount,
                    currency = CurrencyType.Item978,
                    transactionType = 0 }
            };
            string xml = data.Serialize();
                sendMessageToVPJ(xml);
            return xml;
        }
    }
}
