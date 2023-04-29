using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VWW.Samples.Chat.Contracts;
using VWW.Samples.Chat.Contracts.DTOs;

namespace VWW.Samples.Chat.Service
{
    sealed class ServiceManager
    {
        private static readonly Lazy<ServiceManager> instance = new Lazy<ServiceManager>(() => new ServiceManager());

        public static ServiceManager Instance { get { return instance.Value; } }

        private readonly object SyncRoot;
        private readonly Dictionary<string, IChatClient> clients;

        private ServiceManager()
        {
            this.SyncRoot = new object();
            this.clients = new Dictionary<string, IChatClient>();
        }

        internal IEnumerable<string> GetOnlineUser()
        {
            return this.clients.Keys;
        }

        public string TryRegisterClient(string username, IChatClient client)
        {
            lock (SyncRoot)
            {
                string u = username;
                int i = 1;
                while(this.clients.ContainsKey(u))
                    u = username + "_" + i++;
                this.clients.Values.AsParallel().ForAll(f => f.UserLogedIn(u));
                this.clients.Add(u, client);
                return u;
            }
        }

        internal IEnumerable<string> SendMessage(string message, IEnumerable<string> targets, IChatClient client)
        {
            var sender = this.clients.SingleOrDefault(s => s.Value == client).Key;
            if (string.IsNullOrWhiteSpace(sender))
                return new string[0];
            var m = new Message()
            {
                IsBroadcast = false,
                MessageText = message,
                Sender = sender
            };
            var results = this.clients.Where(w => targets.Contains(w.Key));
            results.Select(s=>s.Value).AsParallel().ForAll(f => f.Receive(m));
            return results.Select(s => s.Key);
        }

        internal void SendMessage(string msg, IChatClient client)
        {
            var sender = this.clients.SingleOrDefault(s => s.Value == client).Key;
            if (string.IsNullOrWhiteSpace(sender))
                return;
            var m = new Message()
            {
                IsBroadcast = true,
                MessageText = msg,
                Sender = sender
            };
            this.clients.Values.AsParallel().ForAll(f => f.Receive(m));
        }

        internal bool LogOff(string username)
        {
            lock (SyncRoot)
            {
                if (this.clients.ContainsKey(username))
                {
                    this.clients.Remove(username);
                    this.clients.Values.AsParallel().ForAll(f => f.UserLogedOff(username));
                }
            }
            return true;
        }
    }
}
