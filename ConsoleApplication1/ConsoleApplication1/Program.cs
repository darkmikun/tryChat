using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Chat chat = new Chat();
            Thread ListenThread = new Thread(new ThreadStart(chat.Listen));
            ListenThread.Start();
            string data = Console.ReadLine();
            chat.SendMessage(data);
        }

       
    }
}
