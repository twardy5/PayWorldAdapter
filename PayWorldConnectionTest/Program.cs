
using System;

using PayWorldAdapter;

namespace PayWorldConnectionTest
{
    class Program
    {
        static void Main(string[] args)
        {

            VPJClient client = new VPJClient();
            
            client.SetTimeout(3);
            client.OpenConnection("10.0.0.25", 50000);
            var posID = Guid.NewGuid().ToString();
            //client.MessageRecivedEvent += Client_MessageRecivedEvent;
             client.Ping(posID);
           // client.SendAmout(posID, 32323, 59, "123");
            ;
            //var retValue = client.Ping("12121213");
            //Ping ("12121213",1,59,"123123_TEST");
            
            /*
            byte[] bytes = BitConverter.GetBytes(257);
            //Console.WriteLine(bytes); 
            string hexStr = ByteArrayToHex(bytes,false);
            var i = BitConverter.ToInt32(bytes,0);
            BitArray myBA = new BitArray(BitConverter.GetBytes(257).ToArray());
            var bits_ = new BitArray(bytes);
            string bits = "";
            int k = 0;
            string prefix = "";
            foreach (var bit in myBA)€€
            {
                k++;
                bits = bits + prefix;
                if (Convert.ToBoolean( bit))
                    bits += "1";
                else bits += "0";

                if ((k % 8) == 0 && (k != 0))
                    prefix = " ";
                else prefix = "";

            }
            byte[] requestLength = PayWorldAdapter.Util.CommonMethods.intToBigEndian(257);
            hexStr = ByteArrayToHex(requestLength,true);
            i = BitConverter.ToInt32(requestLength, 0);
            */
        }
              
        private static void Client_MessageRecivedEvent(object sender, EventArgs e)
        {
           
        }
     
        public static string ByteArrayToHex(byte[] data,bool bigEndian)
        {
            char[] c = new char[data.Length * 2];
            byte b;
            if (!bigEndian)
            {
                //read the byte array in reverse
                for (int y = data.Length - 1, x = 0; y >= 0; --y, ++x)
                {
                    b = ((byte)(data[y] >> 4));
                    c[x] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                    b = ((byte)(data[y] & 0xF));
                    c[++x] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                }
            }
            else
            {
                for (int y = 0, x = 0; y < data.Length; ++y, ++x)
                {
                    b = ((byte)(data[y] >> 4));
                    c[x] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                    b = ((byte)(data[y] & 0xF));
                    c[++x] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                }
            }
            return String.Concat("0x", new string(c));
        }
       
    }
}
