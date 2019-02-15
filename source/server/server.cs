using System;
using System.Collections.Generic;
using System.Linq;
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

        #region ' Fields and Properties '

        private const String HOST = "45.32.82.133";
        private const Int32 PORT = 21567;
        private IPEndPoint end_point = new IPEndPoint(
            IPAddress.Parse(HOST), PORT);
        private const Int32 BUFSIZ = 1024 * 1024;

        private Socket server_socket { get; set; }

        private Dictionary<UInt32, Lobby> lobbies { get; } =
            new Dictionary<uint, Lobby>()
            {
                [1] = new Lobby(1)
            };

        #endregion

        #region ' Methods '

        public void mainloop()
        {
            Console.WriteLine("Server started.");
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
            Console.WriteLine($"System: {ep} connected.");
            try
            {
                while (true)
                {
                    Byte[] arr_data = new Byte[BUFSIZ];
                    Int32 length = client_socket.Receive(arr_data);
                    String str_data = Encoding.UTF8.GetString(
                        arr_data, 0, length);
                    Console.WriteLine($"{ep}: {str_data}");
                    if (str_data == null)
                    {
                        break;
                    }
                    Dictionary<String, String> dict =
                        DataEncoding.get_dictionary(str_data);
                    switch(dict["identifier"])
                    {
                        default:
                            throw new DataEncodingException("Invalid identifier.");
                        case "login":
                            check_login_request(user, dict);
                            break;
                        case "lobby_enter":
                            check_lobby_enter_request(user, dict);
                            break;
                        case "lobby_chessmove":
                            check_lobby_chessmove_request(user, dict);
                            break;
                    }
                }
            }
            catch(SocketException) {; }
            catch(ObjectDisposedException) {; }
            catch(DataEncodingException e)
            {
                send_error(client_socket, e.Message);
            }
            catch(LobbyException e)
            {
                send_error(client_socket, e.Message);
            }
            catch(UserNotLoggedInException e)
            {
                send_error(client_socket, e.Message);
            }

            user.try_log_out();
            client_socket.Close();
            Console.WriteLine($"System: {ep} disconnected.");
        }

        #region ' Sending '

        private void send(Socket client, Byte[] arr_data)
        {
            client.Send(arr_data);
        }

        private void send(Socket client, String str_data)
        {
            client.Send(Encoding.UTF8.GetBytes(str_data));
        }

        private void send(Socket client, Dictionary<String, String> dict)
        {
            client.Send(DataEncoding.get_bytes(dict));
        }

        private void send_error(Socket client, String message)
        {
            send(client, new Dictionary<String, String>()
            {
                ["identifier"] = "error",
                ["message"] = message
            });
        }

        #endregion

        #region ' Login '

        private void check_login_request(
            User user, Dictionary<String, String> dict)
        {
            Socket client_socket = user.socket;
            String email = dict["email"];
            String password = dict["password"];
            Int32 code = user.log_in(email, password);
            send(client_socket, new Dictionary<String, String>()
            {
                ["identifier"] = "login",
                ["response"] = code.ToString()
            });
            switch(code)
            {
                default:
                    Console.WriteLine($"{email}({user.client_end_point}) " +
                        "failed to log in.");
                    user.socket.Close(10);
                    break;
                case 0:
                    Console.WriteLine($"{email}({user.client_end_point}) " +
                        "logged in successfully.");
                    break;
            }
        }

        #endregion

        #region ' Lobby '

        private void check_lobby_enter_request(
            User user, Dictionary<String, String> dict)
        {
            if (!user.is_logged_in)
                throw new UserNotLoggedInException();
            Socket client_socket = user.socket;
            UInt32 lobby_id = UInt32.Parse(dict["lobby_id"]);
            Lobby lobby = lobbies[lobby_id];
            Seat seat = (Seat)Int32.Parse(dict["seat"]);
            Int32 code = user.enter_lobby(lobby, seat);
            send(client_socket, new Dictionary<String, String>()
            {
                ["identifier"] = "lobby_enter",
                ["response"] = code.ToString()
            });
        }

        private void check_lobby_chessmove_request(
            User user, Dictionary<String, String> dict)
        {
            if (!user.is_logged_in)
                throw new UserNotLoggedInException();
            Lobby lobby = user.lobby;
            Seat seat = user.seat;
            lobby.broadcast(dict, seat);
        }

        #endregion

        #endregion
    }
}
