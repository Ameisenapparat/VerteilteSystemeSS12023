using System;

namespace VWW.Samples.Chat.Client
{
    public class MyEventArgs<T> : EventArgs
    {
        public T Data { get; set; }
    }
}