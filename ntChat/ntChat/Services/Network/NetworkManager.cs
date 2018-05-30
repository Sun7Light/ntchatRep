using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ntChat.Services.Network
{
    public class NetworkManager
    {
        private TcpListener tcpListener;
        private List<IPAddress> ips;
        private object sync;

        public NetworkManager()
        {
            ips = new List<IPAddress>();
            sync = new object();
        }

        public void StartListen()
        {
            if (tcpListener != null)
                tcpListener.Stop();

            var tsk = Task.Run(() => Listen());
        }

        public void StopListen()
        {
            tcpListener?.Stop();
        }

        public Task SendMsgAsync(IPAddress ip, int port, JObject sendMsg)
        {
            return Task.Run(() => SendMsg(ip, port, sendMsg));
        }

        private void SendMsg(IPAddress ip, int port, JObject sendMsg)
        {

        }

        private void Listen()
        {
            AppSettings appSettings = AppSettings.GetInstance();
            int port = appSettings.DefaultPort;
            int backlog = appSettings.Backlog;
            int receiveTimeout = appSettings.ReceiveTimeout;
            int sendTimeout = appSettings.SendTimeout;

            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start(backlog);

            tcpListener = listener;

            while (true)
            {
                TcpClient connection = listener.AcceptTcpClient();
                connection.ReceiveTimeout = receiveTimeout;
                connection.SendTimeout = sendTimeout;

                var tsk = Task.Run(() => HandleConnection(connection));
            }
        }

        private void HandleConnection(TcpClient connection)
        {

        }
    }
}
