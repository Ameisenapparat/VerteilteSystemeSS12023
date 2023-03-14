using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VWW.Samples.Chat.Contracts.DTOs;

namespace VWW.Samples.Chat.Contracts
{
    public interface IChatClient
    {
        [OperationContract(IsOneWay = true)]
        void UserLogedIn(string username);
        [OperationContract(IsOneWay = true)]
        void UserLogedOff(string username);
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Message))]
        void Receive(Message msg);
    }
}
