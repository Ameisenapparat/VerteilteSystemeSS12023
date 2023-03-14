using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VWW.Samples.Chat.Contracts.DTOs
{
    [DataContract]
    public class LoginResponse
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string[] OnlineUser { get; set; }
    }
}
