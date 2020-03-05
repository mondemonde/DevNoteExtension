using EFCoreTransactionsReceiver.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationEvents.Events.DevNote
{
    public class DevNoteIntegrationEvent : IntegrationEvent
    {
        public DevNoteIntegrationEvent(){
            GuidId = Guid.NewGuid().ToString();
            EventParameters = new Dictionary<string, string>();
        }   

        public DevNoteIntegrationEvent(Guid id, 
            string eventName, Dictionary<string, string> parameters)
        {
            //GuidId = id;
            GuidId = id.ToString();
            EventParameters = parameters;
            EventName = eventName;//vl
        }

        string _id;
        //public new Guid Id
        //{
        //    get
        //    {
        //        return _id;
        //    }
        //    set
        //    {
        //        _id = value;
        //    }
        //}

        public  string GuidId {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }


        public string EventName { get; set; }

        public string OuputResponse { get; set; }

        public Dictionary<string, string> EventParameters { get; set; }

        public int RetryCount { get; set; }

        public string ErrorCode { get; set; }






    }
}
