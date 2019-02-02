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
            this.socket = socket;
        }

        #endregion

        #region ' Properties '

        private String CONNECTION_STRING =
            "server = localhost; " +
            "user = ccol_user; " +
            "database = chinese_chess_online; " +
            "port = 3306; " +
            "password = 123PengZiYu@";

        private Socket socket { get; set; }
        public IPEndPoint client_end_point
        {
            get { return socket.RemoteEndPoint as IPEndPoint; }
        }
        public IPEndPoint server_end_point
        {
            get { return socket.LocalEndPoint as IPEndPoint; }
        }

        #region ' Login '

        /// <summary>
        /// If login is failed, then set the value to 0.
        /// </summary>
        private UInt32 _login_id { get; set; }
        /// <summary>
        /// If login is failed, then set the value to 0.
        /// </summary>
        public UInt32 login_id { get { return _login_id; } }

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
                            return 0;
                    }

                    #endregion
                }
            }
        }

        #endregion

        #endregion
    }
}
