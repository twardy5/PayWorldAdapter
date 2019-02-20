
using PayWorldAdapter.Extenssion;
using PayWorldAdapter.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;


namespace PayWorldAdapter
{
   
    public delegate void MessageRecivedEventhandler(MessageEventArgs e);

    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("26ef940d-20db-4148-9315-7f88d280d538")]
    [ComVisible(true)]
    public interface IVPJClient
    {
        bool Ping(string posID);
        bool SendAmout(string posID, double amount, int curency,string AddPaymentInfo);
        event EventHandler MessageRecivedEvent;
        int Port { get; set; }
        string Host { get; set; }
        void OpenConnection(string host, int port);
        void CloseConnection();
        void SetTimeout(int timeout);
    }
    
    [ComVisible(true)]
    [Guid("0170282e-2f11-42ee-b59f-7ba4f326b8ef")]
    [ClassInterface(ClassInterfaceType.None)]
    public class VPJClient: IVPJClient
    {
        public event MessageRecivedEventhandler CustomMessageRecivedEvent;
        public event EventHandler MessageRecivedEvent;
        // Invoke the ChangedEvent  
       // private static readonly log4net.ILog log = 
        //            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private CommunicationTCPIP communication;
        public VPJClient(string host,int port)
        {
            this.communication = new CommunicationTCPIP(host,port, Timeout);
            this.communication.MessageRecivedEvent += MessageRecived;
        }
        public void OpenConnection(string host, int port)
        {

            if (communication != null) {
                communication.StopReciving();
                this.communication.MessageRecivedEvent -= MessageRecived;
            }
            this.communication = new CommunicationTCPIP(host, port, Timeout);
            this.communication.MessageRecivedEvent += MessageRecived;
        }
        private  void MessageRecived(object sender, MessageEventArgs e)
        {
            //log.Info("Event - Message recived: "+e.Data);
            OnMessageRecived(e);
        }
        private void OnMessageRecived(MessageEventArgs e)
        {
            try
            {
                MessageRecivedEvent?.Invoke(this, e);
                CustomMessageRecivedEvent?.Invoke(e);
            }
            catch (Exception ex)
            {

                //log.Error(String.Format("PosID: {0}, {1}", e.UniqueID, ex));
            }
        }
        public int Timeout { get; set; } = 30;
        public  VPJClient()
        {
        }
        private int nPort;
        public void ReConnect(string host, int port)
        {
            if (communication != null)
            {
                this.communication.StopReciving();
                this.communication.MessageRecivedEvent -= MessageRecived;
            }
            this.communication = new CommunicationTCPIP(host, port, Timeout);
            this.communication.MessageRecivedEvent += MessageRecived;
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
        internal bool sendMessageToVPJ(string messageString,string posID,string messageToRecive)
        {
            bool retVal = false;
            try
            {
                if (communication != null)
                {
                    communication.StopReciving();
                    byte[] messageBytes;
                    List<String> messages = new List<String>();
                    try
                    {
                        //messageString = generateMessageString(posMessage, contentType);
                    }
                    catch (Exception e)
                    {
                        messageString = "";
                        //log.Error(String.Format("PosID: {0}, {1}", posID, e));
                    }

                    // this.logger.info("Message to be send:\n" + (this.contentType.isXml() ? StringUtils.formatXml(messageString) : messageString));
                    try
                    {
                        messageBytes = messageString.ToByteArrayUTF8();
                    }
                    catch (Exception e)
                    {
                        messageBytes = null;
                        //log.Error(String.Format("PosID: {0}: {1}", posID, e));
                    }
                    byte[] requestLength = CommonMethods.intToBigEndian(messageBytes.Length);
                    messageBytes = requestLength.Combine(messageBytes);
                    try
                    {

                        retVal = communication.SendData(messageBytes, posID, messageToRecive);
                    }
                    catch (Exception e)
                    {
                        //log.Error(String.Format("PosID: {0}, {1}",posID,e));
                    }
                }

            }
            catch (Exception)
            {

            }
            return retVal;
        }

        List<MessageEventArgs> Messages = new List<MessageEventArgs>();
        
        public List<MessageEventArgs> GetMessage(string posID) {

            var msg = (from m in Messages where m.UniqueID == posID select m).ToList();
            if (msg != null)
                return msg;
            return new List<MessageEventArgs>();
        }
        public bool SendAmout(string posID, double amount, int pCurency, string pAddPaymentInfo)
        {
            //log.Info(String.Format("PosID: {0}: Send Amount started",posID));
            CurrencyType currency = (CurrencyType)pCurency;
            FinancialTrxRequest data = new FinancialTrxRequest { posId = posID,
                trxData = new TransactionData {
                    amount = (ulong)amount,
                    additionalMerchantData = pAddPaymentInfo,
                    currency = CurrencyType.Item978,
                    transactionType = 0 }
            };
            string xml = data.Serialize();
            return sendMessageToVPJ(xml,posID, "<vcs-pos:financialTrxResponse");
         
        }
        public bool Ping(string posID) {
            PingRequest data = new PingRequest();
            string xml = data.Serialize();
            return sendMessageToVPJ(xml, posID, "<vcs-pos:pingResponse");
            
        }

        public void SetPort(int pPort)
        {
            Port = pPort;
        }

        public void CloseConnection()
        {
            try
            {
                if (communication != null)
                    communication.StopReciving();
            }
            catch (Exception e)
            {

                
            }
        }

        public void SetTimeout(int timeout)
        {
            Timeout = timeout;
            try
            {
                if (communication != null)
                    communication.Timeout = Timeout;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
