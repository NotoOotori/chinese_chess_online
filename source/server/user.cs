using System;
using System.Data;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;

namespace server
{
    public class User
    {
        #region ' Constructors '

        public User(Socket socket)
        {
            this._socket = socket;
        }

        #endregion

        #region ' Properties '

        private String CONNECTION_STRING =
            "server = localhost; " +
            "user = ccol_user; " +
            "database = chinese_chess_online; " +
            "port = 3306; " +
            "password = 123PengZiYu@";

        private Socket _socket { get; set; }
        public Socket socket { get { return _socket; } }
        public IPEndPoint client_end_point
        {
            get { return _socket.RemoteEndPoint as IPEndPoint; }
        }
        public IPEndPoint server_end_point
        {
            get { return _socket.LocalEndPoint as IPEndPoint; }
        }

        #region ' Login '

        public UInt32 login_id { get { return _login_id; } }
        public Boolean is_logged_in { get { return login_id != 0; } }
        public String email_address { get { return _email_address; } }

        private UInt32 _login_id { get; set; } = 0;
        private String _email_address { get; set; }

        #endregion

        #region ' Lobby '

        public Lobby lobby { get { return _lobby; } }
        public Seat seat { get { return _seat; } }
        public Boolean ready { get { return _ready; } }

        private Lobby _lobby { get; set; }
        private Seat _seat { get; set; } = Seat.NONE;
        private Boolean _ready { get; set; } = false;

        #endregion

        #endregion

        #region ' Methods '

        #region ' Login '

        public Int32 log_in(String email_address, String password)
        {
            using (MySqlConnection connection =
                new MySqlConnection(CONNECTION_STRING))
            {
                using (MySqlCommand command = new MySqlCommand(
                    "procedure_log_in", connection)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    #region ' Add Parameters '

                    MySqlParameter _email_address = new MySqlParameter(
                        "_email_address", MySqlDbType.VarString, 254)
                    {
                        Value = email_address,
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _password = new MySqlParameter(
                        "_unencrypted_password", MySqlDbType.VarString, 256)
                    {
                        Value = password,
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _server_hostname = new MySqlParameter(
                        "_server_hostname", MySqlDbType.String, 15)
                    {
                        Value = server_end_point.Address.ToString(),
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _server_port = new MySqlParameter(
                        "_server_port", MySqlDbType.UInt16)
                    {
                        Value = server_end_point.Port,
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _user_hostname = new MySqlParameter(
                        "_user_hostname", MySqlDbType.String, 15)
                    {
                        Value = client_end_point.Address.ToString(),
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _user_port = new MySqlParameter(
                        "_user_port", MySqlDbType.UInt16)
                    {
                        Value = client_end_point.Port,
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _status_code = new MySqlParameter(
                        "_status_code", MySqlDbType.UByte)
                    {
                        Direction = ParameterDirection.Output
                    };
                    MySqlParameter _login_id = new MySqlParameter(
                        "_login_id", MySqlDbType.UInt32)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(_email_address);
                    command.Parameters.Add(_password);
                    command.Parameters.Add(_server_hostname);
                    command.Parameters.Add(_server_port);
                    command.Parameters.Add(_user_hostname);
                    command.Parameters.Add(_user_port);
                    command.Parameters.Add(_status_code);
                    command.Parameters.Add(_login_id);

                    #endregion

                    #region ' Execute '

                    connection.Open();
                    command.ExecuteNonQuery();
                    UInt16 status_code = UInt16.Parse(
                        _status_code.Value.ToString());
                    switch (status_code)
                    {
                        default:
                            return status_code;
                        case 0:
                            this._login_id = UInt32.Parse(
                                _login_id.Value.ToString());
                            this._email_address = email_address;
                            return 0;
                    }

                    #endregion
                }
            }
        }

        public void try_log_out()
        {
            if (login_id == 0)
                return;
            using (MySqlConnection connection =
                  new MySqlConnection(CONNECTION_STRING))
            {
                using (MySqlCommand command = new MySqlCommand(
                    "procedure_log_out", connection)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    #region ' Add Parameters '

                    MySqlParameter _email_address = new MySqlParameter(
                        "_email_address", MySqlDbType.VarString, 254)
                    {
                        Value = this._email_address,
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _login_id = new MySqlParameter(
                        "_login_id", MySqlDbType.UInt32)
                    {
                        Value = this.login_id,
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(_email_address);
                    command.Parameters.Add(_login_id);

                    #endregion

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            quit_lobby();
            try
            {
                Console.WriteLine($"System: {email_address}({client_end_point}) " +
                    $"logged out successfully.");
            }
            catch (ObjectDisposedException)
            {; }
        }

        #endregion

        #region ' Lobby '

        public Int32 enter_lobby(Lobby lobby, Seat seat)
        {
            Int32 code = lobby.try_enter(this, seat);
            if (code == 0)
            {
                this._lobby = lobby;
                this._seat = seat;
            }
            return code;
        }

        public void quit_lobby()
        {
            try
            {
                lobby.try_quit(this, seat);
            }
            catch (NullReferenceException)
            {; }
            this._lobby = null;
            this._seat = Seat.NONE;
        }

        public Int32 try_ready()
        {
            _ready = true;
            Console.WriteLine($"System: User {email_address} got ready in " +
                $"lobby #{lobby.lobby_id}.");
            return 0;
        }

        public void lobby_init()
        {
            _ready = false;
        }

        #endregion

        #endregion
    }
}

public class UserNotLoggedInException : Exception
{
    public UserNotLoggedInException()
        : base("The user hasn't logged in yet.")
    {; }
}
