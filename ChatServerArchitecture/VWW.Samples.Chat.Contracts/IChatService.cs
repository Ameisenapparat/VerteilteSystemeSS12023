using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VWW.Samples.Chat.Contracts.DTOs;

namespace VWW.Samples.Chat.Contracts
{
    [ServiceContract(CallbackContract = typeof(IChatClient))]
    public interface IChatService
    {

        [OperationContract]
        [ServiceKnownType(typeof(LoginResponse))]
        LoginResponse Login(string username, string password);
        [OperationContract]
        bool Logoff(string username);
        [OperationContract(IsOneWay = true)]
        void SendBroadcast(string msg);
        [OperationContract]
        int SystemStatusRequest();
        [OperationContract]
        string[] SendMessage(string message, string[] targets);
        [OperationContract]
        void UploadModule(string v);
    }
}
