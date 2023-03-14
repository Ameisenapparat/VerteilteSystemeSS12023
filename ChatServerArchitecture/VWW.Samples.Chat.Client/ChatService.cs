using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWW.Samples.Chat.Client
{
    public class ChatService
    {
        public static IClient GetClient(string url, string username, string password)
        {
            return new Client(url, username, password);
        }
    }
}
