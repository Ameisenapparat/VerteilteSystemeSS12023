using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VWW.Samples.Chat.Contracts;
using VWW.Samples.Chat.Contracts.DTOs;

namespace VWW.Samples.Chat.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ChatService : IChatService
    {
        public LoginResponse Login(string username, string password)
        {
            return new LoginResponse()
            {
                Username = ServiceManager.Instance.TryRegisterClient(username, OperationContext.Current.GetCallbackChannel<IChatClient>()),
                OnlineUser = ServiceManager.Instance.GetOnlineUser().ToArray()
            };
        }

        public bool Logoff(string username)
        {
            return ServiceManager.Instance.LogOff(username);
        }

        public void SendBroadcast(string msg)
        {
            ServiceManager.Instance.SendMessage(msg, OperationContext.Current.GetCallbackChannel<IChatClient>());
        }

        public string[] SendMessage(string message, string[] targets)
        {
            return ServiceManager.Instance.SendMessage(message, targets, OperationContext.Current.GetCallbackChannel<IChatClient>()).ToArray();
        }
    }
}
