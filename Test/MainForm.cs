using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using PayWorldAdapter;

namespace Test
{
    public partial class MainForm : Form
    {
        
        //CommunicationTCPIP TCPIPcommunication;
    
      
        public MainForm()
        {
            InitializeComponent();
          //  MessageTB.Text = "<?xml version=""1.0"" encoding="UTF - 8"?><vcs-pos:pingRequest xmlns:vcs-pos="http://www.vibbek.com/pos" />"
           
            TCPIPCheckBox.Checked = true;
            StopRecivingBtn.Enabled = false;
            //TCPIPcommunication = new CommunicationTCPIP();
            //server = new AsynchronousSocketListener();
            //<posId>2003</posId><trxData><amount>1</amount><currency>978</currency><transactionType>0</transactionType></trxData>
            //EURO - 978
          //  FinancialTrxRequest data = new FinancialTrxRequest {posId = "2003", trxData = new TransactionData { amount = 1,currency=CurrencyType.Item978,transactionType = 0 } };
            //string xml = data.Serialize();
            
        }

      

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (TCPIPCheckBox.Checked) {

           //     TCPIPcommunication.SendByTCPIP(@"C:\Data\Payworld Interface\sendXML.xml", comboBox1.Text, Convert.ToInt32(comboBox2.Text));

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //TCPIPcommunication.StopReciving();
            //StartReciveBtn.Enabled = true;
            //StopRecivingBtn.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //TCPIPcommunication.StartReceiving(comboBox3.Text);
            //SynchronousSocketListener.Start(comboBox1.Text,Convert.ToInt32(comboBox3.Text));
            //TCPIPcommunication = new CommunicationTCPIP(comboBox1.Text, Convert.ToInt32( comboBox3.Text));
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //CommunicationTCPIP communication = new CommunicationTCPIP(comboBox1.Text.ToString(), Convert.ToInt32(comboBox2.Text));
           
            //StartReciveBtn.Enabled = false;
            //StopRecivingBtn.Enabled = true;
            //VPJClient client = new VPJClient(comboBox1.Text.ToString(), Convert.ToInt32(comboBox2.Text));
            //client.sendFileToVPJ(@"C:\Data\Payworld Interface\ping.xml");
        }
        private string VCS = @"http://5.1.113.136:61613";
        private string vibbekPOS = "http://www.vibbek.com/pos";
        private void button2_Click(object sender, EventArgs e)
        {
         
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VPJClient client = new VPJClient("192.168.2.96", 50000);
            client.SendAmout("2003", 1, CurrencyType.Item978);
        }
    }
}
