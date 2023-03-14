using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VWW.Samples.Chat.Contracts.DTOs
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Sender { get; set; }
        [DataMember]
        public bool IsBroadcast { get; set; }
        [DataMember]
        public string MessageText { get; set; }
    }
}
