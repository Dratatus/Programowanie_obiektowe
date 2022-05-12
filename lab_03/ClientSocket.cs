using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace lab_03
{
    class ClientSocket
    {
        private bool disposed;

        private Socket socket;

        public ClientSocket(string host, int port)
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            disposed = true;
            socket.Connect(host, port);
        }
        ~ClientSocket()
        {
            this.Dispose();
        }
        public void Send(byte[] buffer)
        {
            socket.Send(buffer, SocketFlags.None);
        }
        public void Send(byte[] buffer, int offset, int size)
        {
            socket.Send(buffer, offset, size, SocketFlags.None);
        }
        public void Receive(byte[] buffer)
        {
            socket.Receive(buffer, SocketFlags.None);
        }
        public void Receive(byte[] buffer, int offset, int size)
        {
            socket.Receive(buffer, offset, size, SocketFlags.None);
        }
        public void Close()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        public void Dispose()
        {
            if (!disposed)
            {
                socket.Dispose();
                disposed = true;
            }
        }
    }
}
