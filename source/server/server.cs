using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace server
{
    public class Server
    {
        #region ' Constructors '

        public Server()
        {
            server_socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp)
            {
                ReceiveBufferSize = BUFSIZ,
                SendBufferSize = BUFSIZ
            };
            server_socket.Bind(end_point);
            server_socket.Listen(20);
        }

        #endregion

        #region 'Fields and Properties '

        private const String HOST = "45.32.82.133";
        private const Int32 PORT = 21567;
        private IPEndPoint end_point = new IPEndPoint(
            IPAddress.Parse(HOST), PORT);
        private const Int32 BUFSIZ = 1024 * 1024;

        private Socket server_socket { get; set; }

        #endregion

        #region ' Methods '

        public void mainloop()
        {
            while (true)
            {
                Socket client_socket = server_socket.Accept();
                ParameterizedThreadStart pts = 
                    new ParameterizedThreadStart(client_thread);
                Thread thread = new Thread(pts);
                thread.Start(client_socket);
            }
        }

        private void client_thread(object client_socket_para)
        {
            Socket client_socket = client_socket_para as Socket;
            User user = new User(client_socket);
            String ep = client_socket.RemoteEndPoint.ToString();
            try
            {
                while (true)
                {
                    Byte[] arr_data = new Byte[BUFSIZ];
                    Int32 length = client_socket.Receive(arr_data);
                    String str_data = Encoding.UTF8.GetString(
                        arr_data, 0, length);
                    if (str_data == null)
                    {
                        Console.WriteLine($"{ep} disconnected.");
                        break;
                    }
                    Int32 code = user.log_in("345238563@qq.com", "123pzy");
                    client_socket.Send(Encoding.UTF8.GetBytes(code.ToString()));
                }
            }
            catch(SocketException)
            {
                client_socket.Close();
            }
        }

        #endregion
    }
}
