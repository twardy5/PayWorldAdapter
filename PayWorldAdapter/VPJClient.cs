
using PayWorldAdapter.Extenssion;
using PayWorldAdapter.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
 

namespace PayWorldAdapter
{
   
    public class VPJClient:IPOSMessages
    {
        public delegate void MessageRecivedEventhandler(MessageEventArgs e);
        public delegate void ChangedEventHandler();
        public event ChangedEventHandler ChangedEvent;
        public event MessageRecivedEventhandler MessageRecivedEvent;
        // Invoke the ChangedEvent  
        protected virtual void OnChangedEvent()
        {
            if (ChangedEvent != null)
            {
                ChangedEvent();
            }
        }

        // The following method is exposed to C/AL and will invoke the event trigger that is registered in the ChangedEvent variable.   
        public void FireEvent()
        {
            OnChangedEvent();
        }
        
        private static readonly log4net.ILog log = 
                    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private CommunicationTCPIP communication;
        public VPJClient(string host,int port)
        {
            this.communication = new CommunicationTCPIP(host,port);
            this.communication.MessageRecivedEvent += MessageRecived;
        }
        private  void MessageRecived(object sender, MessageEventArgs e)
        {
            log.Info("Event - Message recived: "+e.Data);
            OnMessageRecived(e);
        }
        private void OnMessageRecived(MessageEventArgs e)
        {
            try
            {
                MessageRecivedEvent?.Invoke(e);

            }
            catch (Exception ex)
            {

                log.Error(String.Format("PosID: {0}, {1}", e.UniqueID, ex));
            }
         
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

        
        internal List<String> sendMessageToVPJ(string messageString,string posID)
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
                log.Error(String.Format("PosID: {0}, {1}", posID, e));
            }
           
           // this.logger.info("Message to be send:\n" + (this.contentType.isXml() ? StringUtils.formatXml(messageString) : messageString));
            try
            {
               
                messageBytes = messageString.ToByteArrayUTF8();

            }
            catch (Exception e)
            {
                messageBytes = null ;
                log.Error(String.Format("PosID: {0}: {1}", posID, e));
            }
            byte[] requestLength = CommonMethods.intToBigEndian(messageBytes.Length);
            messageBytes = requestLength.Combine(messageBytes);
            try
            {

                messages = communication.SendData(messageBytes,posID);
            }
            catch (Exception e)
            {
                log.Error(String.Format("PosID: {0}, {1}",posID,e));
            }
            return messages;
        }

        List<MessageEventArgs> Messages = new List<MessageEventArgs>();
        
        public List<MessageEventArgs> GetMessage(string posID) {

            var msg = (from m in Messages where m.UniqueID == posID select m).ToList();
            if (msg != null)
                return msg;
            return new List<MessageEventArgs>();
        }
        public string SendAmout(string posID, double amount, CurrencyType curency)
        {
            log.Info(String.Format("PosID: {0}: Send Amount started",posID));
            FinancialTrxRequest data = new FinancialTrxRequest { posId = posID,
                trxData = new TransactionData {
                    amount = (ulong)amount,
                    currency = CurrencyType.Item978,
                    transactionType = 0 }
            };
            string xml = data.Serialize();
                sendMessageToVPJ(xml,posID);
            return xml;
        }
     
       
    }
}
