using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWW.Samples.Chat.Client
{
    public interface IClient : IDisposable
    {
        IEnumerable<string> User { get; }
        event EventHandler<MyEventArgs<string>> OnUserLoggedIn;
        event EventHandler<MyEventArgs<string>> OnUserLoggedOff;
        event EventHandler<MyEventArgs<Message>> OnMessageReceived;
        string Username { get; }

        void SendBroadcast(string msg);
        void SendMessage(string message, IEnumerable<string> targets);
        int SystemStatusRequest();
        void UploadModule(string v);
    }
}
