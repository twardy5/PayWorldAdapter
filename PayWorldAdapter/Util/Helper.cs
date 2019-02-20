using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace PayWorldAdapter.Util
{
   
    public static class Helper
    {

        public static byte[] ToByteArrayUTF8(this string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }
       
        public static byte[] Combine(this byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
        public static Int16 ToInt16( byte[] data, int offset)
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToInt16(BitConverter.IsLittleEndian ? data.Skip(offset).Take(2).Reverse().ToArray() : data, 0);
            return BitConverter.ToInt16(data, offset);
        }
        public static Int32 ToInt32( byte[] data, int offset)
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToInt32(BitConverter.IsLittleEndian ? data.Skip(offset).Take(4).Reverse().ToArray() : data, 0);
            return BitConverter.ToInt32(data, offset);
        }
        public static Int64 ToInt64( byte[] data, int offset)
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToInt64(BitConverter.IsLittleEndian ? data.Skip(offset).Take(8).Reverse().ToArray() : data, 0);
            return BitConverter.ToInt64(data, offset);
        }
    }
    
}
