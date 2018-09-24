using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWorldConnection.Util
{
    class CommonMethods
    {
        public static byte[] intToBigEndian(int i)
        {
            byte[] ret = new byte[4];
            for (int x = 0; x < 4; x++)
            {
                ret[(3 - x)] = ((byte)(i >> 8 * x & 0xFF));
            }
            return ret;
        }
    }
}
