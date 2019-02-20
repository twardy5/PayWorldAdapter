using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace PayWorldAdapter
{
    [ComVisible(true)]
    [Guid("711d8262-44e4-447d-8184-bf8052360e7a")]
    [ClassInterface(ClassInterfaceType.None)]
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
    