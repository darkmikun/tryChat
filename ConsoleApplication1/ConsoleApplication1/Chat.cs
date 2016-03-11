using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Chat
    {
        private UdpClient udpclient;
        private IPAddress multicastaddress;
        private IPEndPoint remoteep;
        public void SendMessage(string data)
        {
            Byte[] buffer = Encoding.UTF8.GetBytes(data);

            udpclient.Send(buffer, buffer.Length, remoteep);
        }
        public void Listen()
        {
            UdpClient client = new UdpClient();

            client.ExclusiveAddressUse = false;
            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 2222);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            client.JoinMulticastGroup(multicastaddress);

            Console.WriteLine("\tListening started");

            string formatted_data;

            while (true)
            {
                Byte[] data = client.Receive(ref localEp);
                formatted_data = Encoding.UTF8.GetString(data);
                Console.WriteLine(formatted_data);
            }
        }

        public Chat()
        {
            multicastaddress = IPAddress.Parse("239.0.0.222"); // один из зарезервированных для локальных нужд UDP адресов
            udpclient = new UdpClient();
            udpclient.JoinMulticastGroup(multicastaddress);
            remoteep = new IPEndPoint(multicastaddress, 2222);
        }
    }
}
