using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWorldAdapter
{
    public class EventsNoArgs
    {
        public delegate void ChangedEventHandler();
        public event ChangedEventHandler ChangedEvent;

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
    }
}
