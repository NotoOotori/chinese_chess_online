using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace server
{
    public class Lobby
    {
        #region ' Constructors '

        /// <summary>
        /// 建立<see cref="Lobby"/>类的新实例.
        /// </summary>
        public Lobby(UInt32 lobby_id)
        { this.lobby_id = lobby_id; }

        #endregion

        #region ' Properties '

        private String CONNECTION_STRING =
            "server = localhost; " +
            "user = ccol_user; " +
            "database = chinese_chess_online; " +
            "port = 3306; " +
            "password = 123PengZiYu@";

        public UInt32 lobby_id { get; }

        public User seat_1 { get { return seats[Seat.ONE]; } }
        public User seat_2 { get { return seats[Seat.TWO]; } }
        public Dictionary<Seat, User> seats { get; } =
            new Dictionary<Seat, User>()
            {
                [Seat.ONE] = null,
                [Seat.TWO] = null
            };
        public Int32 user_count
        {
            get
            {
                Int32 count = 0;
                foreach (User user in seats.Values)
                    if (user != null)
                        count++;
                return count;
            }
        }

        #endregion

        #region ' Methods '

        /// <summary>
        /// Broadcast to all clients in the lobby.
        /// </summary>
        /// <param name="data"></param>
        public void broadcast(Byte[] data)
        {
            foreach (User user in seats.Values)
            {
                try
                {
                    user.socket.Send(data);
                }
                catch (NullReferenceException)
                {; }
            }
        }

        /// <summary>
        /// Broadcast to all clients in the lobby.
        /// </summary>
        /// <param name="dict"></param>
        public void broadcast(Dictionary<String, String> dict)
        {
            foreach (User user in seats.Values)
            {
                try
                {
                    user.socket.Send(dict);
                }
                catch (NullReferenceException)
                {; }
            }
        }

        /// <summary>
        /// Broadcast to all clients except the one in the specified seat.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="seat"></param>
        public void broadcast(Byte[] data, Seat seat)
        {
            foreach (var pair in seats)
            {
                if (pair.Key == seat)
                    continue;
                try
                {
                    pair.Value.socket.Send(data);
                }
                catch (NullReferenceException)
                {; }
            }
        }

        /// <summary>
        /// Broadcast to all clients except the one in the specified seat.
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="seat"></param>
        public void broadcast(Dictionary<String, String> dict, Seat seat)
        {
            foreach (var pair in seats)
            {
                if (pair.Key == seat)
                    continue;
                try
                {
                    pair.Value.socket.Send(dict);
                }
                catch (NullReferenceException)
                {; }
            }
        }
        
        public void try_start_game()
        {
            if (count_ready_users() < 2)
                return;
            Int32 count = 0;
            foreach (User user in seats.Values)
            {
                Socket socket = user.socket;
                socket.Send(new Dictionary<String, String>()
                {
                    ["identifier"] = "lobby_gamestart",
                    ["colour"] = "rb".Substring(count++, 1)
                });
            }
        }

        private Int32 count_ready_users()
        {
            Int32 count = 0;
            foreach (User user in seats.Values)
                if (user.ready)
                    count++;
            return count;
        }

        /// <summary>
        /// If successful then returns 0.
        /// If user is currently in another lobby then returns 1.
        /// If the seat is occupied then returns 2.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="seat"></param>
        /// <returns></returns>
        public Int32 try_enter(User user, Seat seat)
        {
            if (user.lobby != null)
                return 1;
            if (seats[seat] != null)
                return 2;
            Console.WriteLine($"System: User {user.email_address} entered into " +
                $"lobby #{lobby_id}.");
            seats[seat] = user;
            return 0;
        }

        public void try_quit(User user, Seat seat)
        {
            if (seats[seat] == user)
                seats[seat] = null;
            Console.WriteLine($"System: User {user.email_address} quit " +
                $"lobby #{lobby_id}.");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="result"><see cref="Seat"/>1上玩家的结果</param>
        public void end_game(String result)
        {
            String red_elo_change = "0";
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

                    MySqlParameter _red_email_address = new MySqlParameter(
                        "_red_email_address", MySqlDbType.VarString, 254)
                    {
                        Value = seat_1.email_address,
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _black_email_address = new MySqlParameter(
                        "_black_email_address", MySqlDbType.VarString, 254)
                    {
                        Value = seat_2.email_address,
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _game_string = new MySqlParameter(
                        "_game_string", MySqlDbType.VarString, 500)
                    {
                        Value = "c2e2", //TODO
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _result = new MySqlParameter(
                        "_result", MySqlDbType.Byte)
                    {
                        Value = result,
                        Direction = ParameterDirection.Input
                    };
                    MySqlParameter _red_elo_change = new MySqlParameter(
                        "_red_elo_change", MySqlDbType.Int16)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(_red_email_address);
                    command.Parameters.Add(_black_email_address);
                    command.Parameters.Add(_game_string);
                    command.Parameters.Add(_result);
                    command.Parameters.Add(_red_elo_change);

                    #endregion

                    connection.Open();
                    command.ExecuteNonQuery();

                    red_elo_change = _red_elo_change.Value.ToString();
                }
            }
            broadcast(new Dictionary<String, String>()
            {
                ["identifier"] = "lobby_gameend",
                ["result"] = result,
                ["elo_change"] = red_elo_change
            });
            this.initialize();
        }

        public void initialize()
        {
            foreach (User user in seats.Values)
            {
                user.lobby_init();
            }
        }

        #endregion
    }
}

public enum Seat
{
    NONE = 0,
    ONE = 1,
    TWO = 2
}

public class LobbyException : Exception
{
    public LobbyException()
    {; }

    public LobbyException(String message)
        : base(message)
    {; }
}

public class LobbyEnterException : LobbyException
{
    public LobbyEnterException()
    {; }

    public LobbyEnterException(String message)
        : base(message)
    {; }
}
