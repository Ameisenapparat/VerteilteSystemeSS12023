using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VWW.Samples.Chat.Contracts;
using VWW.Samples.Chat.Contracts.DTOs;

namespace VWW.Samples.Chat.Client
{
    internal class Client : IDisposable, IChatClient, IClient
    {
        private List<string> user;
        private IChatService service;
        public IEnumerable<string> User { get { return user; } }
        public event EventHandler<MyEventArgs<string>> OnUserLoggedIn;
        public event EventHandler<MyEventArgs<string>> OnUserLoggedOff;
        public event EventHandler<MyEventArgs<Message>> OnMessageReceived;
        public string Username { get; private set; }

        #region IDisposable Support
        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this.service != null)
                    {
                        try
                        {
                            this.service.Logoff(this.Username);
                            ((System.ServiceModel.Channels.IChannel)this.service).Close();
                        }
                        catch (Exception) { }
                    }
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.

                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~Client() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        // Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public Client(string url, string username, string password)
        {
            string address = url;
            var binding = new WSDualHttpBinding();
            DuplexChannelFactory<IChatService> channelFactory = new DuplexChannelFactory<IChatService>(new InstanceContext(this), binding, address);
            IChatService service = channelFactory.CreateChannel();


            this.service = service;
            var response = service.Login(username, password);
            this.Username = response.Username;
            this.user = response.OnlineUser.ToList();
        }

        public void UserLogedIn(string username)
        {
            if (!this.user.Contains(username))
                this.user.Add(username);
            this.OnUserLoggedIn?.Invoke(this, new MyEventArgs<string>() { Data = username });
        }

        public void UserLogedOff(string username)
        {
            if (this.user.Contains(username))
                this.user.Remove(username);
            this.OnUserLoggedOff?.Invoke(this, new MyEventArgs<string>() { Data = username });
        }

        public void Receive(Contracts.DTOs.Message msg)
        {
            this.OnMessageReceived?.Invoke(this, new MyEventArgs<Message>()
            {
                Data = new Message()
                {
                    MessageText = msg.MessageText,
                    Sender = msg.Sender
                }
            });
        }

        public void SendBroadcast(string msg)
        {
            this.service.SendBroadcast(msg);
        }

        public void SendMessage(string message, IEnumerable<string> targets)
        {
            this.service.SendMessage(message, targets.ToArray());
        }

        public void UploadModule(string v)
        {
            this.service.UploadModule(v);
        }

        public int SystemStatusRequest()
        {
            return this.service.SystemStatusRequest();
        }
    }
}
