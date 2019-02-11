using System;
using System.Net.Sockets;
using System.Collections.Generic;

namespace server {
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

        public UInt32 lobby_id { get; }

        public User seat_1 { get { return seats[Seat.ONE]; } }
        public User seat_2 { get { return seats[Seat.TWO]; } }
        public Dictionary<Seat, User> seats { get; } =
            new Dictionary<Seat, User>()
            {
                [Seat.ONE] = null,
                [Seat.TWO] = null
            };

        #endregion

        #region ' Methods '

        public void broadcast(Byte[] data)
        {; }

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
            Socket client_socket = user.socket;
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
