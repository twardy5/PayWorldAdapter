using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWorldAdapter
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string _myData,string mUniqueID)
        {
            Data = _myData;
            UniqueID = mUniqueID;
        } // eo ctor
        public string UniqueID { get;}
        public string Data { get; }

    }
}
