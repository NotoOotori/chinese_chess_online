using platform.common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace platform.chess_lobby
{
    public partial class ChessLobby : Form
    {
        /// <summary>
        /// 建立<see cref="ChessLobby"/>类的新实例.
        /// </summary>
        /// <param name="lobby_id">房间id</param>
        /// <param name="server_socket">服务器的socket</param>
        public ChessLobby(UInt32 lobby_id, Socket server_socket)
        {
            this.lobby_id = lobby_id;
            this.server_socket = server_socket;

            InitializeComponent();

            #region ' Set Control Styles '

            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor, true);

            #endregion

            Thread thread = new Thread(listening_thread);
            thread.Start();
        }

        private const String HOST = "45.32.82.133";
        private const Int32 PORT = 21567;
        private IPEndPoint end_point = new IPEndPoint(
            IPAddress.Parse(HOST), PORT);
        private const Int32 BUFSIZ = 1024 * 1024;

        public UInt32 lobby_id { get; } = 0;
        public Socket server_socket { get; }

        public ChessColour colour
        {
            get
            {
                return chessboard_container.chessboard.colour;
            }
            set
            {
                chessboard_container.chessboard.colour = value;
            }
        }

        /// <summary>
        /// 旋转<see cref="ChessboardContainer"/>的<see cref="Chessboard"/>.
        /// </summary>
        /// <param name="type"></param>
        delegate void ReflectionTypeArgReturningVoidDelegate(ReflectionType type);
        /// <summary>
        /// 点击<see cref="ChessboardContainer"/>的<see cref="Chessboard"/>的棋子.
        /// </summary>
        /// <param name="cdn"></param>
        /// <param name="from_server"></param>
        delegate void CoordinateArgBooleanArgReturningVoidDelegate(
            Coordinate click, Boolean from_server);

        #region ' Methods '

        private void listening_thread()
        {
            try
            {
                while (true)
                {
                    Byte[] arr_data = new Byte[BUFSIZ];
                    Int32 length = server_socket.Receive(arr_data);
                    String str_data = Encoding.UTF8.GetString(
                        arr_data, 0, length);
                    if (str_data == null)
                    {
                        break;
                    }
                    Dictionary<String, String> dict =
                        DataEncoding.get_dictionary(str_data);
                    switch (dict["identifier"])
                    {
                        default:
                            throw new DataEncodingException("Invalid identifier.");
                        case "lobby_chessmove":
                            check_chessmove_request(dict);
                            break;
                        case "lobby_gamestart":
                            check_gamestart_request(dict);
                            break;
                    }
                }
            }
            catch(DataEncodingException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void check_chessmove_request(Dictionary<String, String> dict)
        {
            String move = dict["move"];
            Coordinate start = new Coordinate(move.Substring(0, 2));
            Coordinate end = new Coordinate(move.Substring(2, 2));
            chessboard_on_click(start.reflect(
                chessboard_container.reflection), true);
            chessboard_on_click(end.reflect(
                chessboard_container.reflection), true);
        }

        private void check_gamestart_request(Dictionary<String, String> dict)
        {
            Char colour_str = dict["colour"][0];
            colour = colour_str.to_chess_colour();
            ReflectionType reflection = colour == ChessColour.RED ?
                ReflectionType.None : ReflectionType.PointReflection;
            chessboard_reflect(
                chessboard_container.reflection ^ reflection);
        }

        private void chessboard_on_click(Coordinate click, Boolean from_server)
        {
            if (chessboard_container.InvokeRequired)
            {
                CoordinateArgBooleanArgReturningVoidDelegate d =
                    new CoordinateArgBooleanArgReturningVoidDelegate(
                        chessboard_on_click);
                this.Invoke(d, new object[] { click, from_server });
            }
            else
            {
                this.chessboard_container.chessboard.on_click(click, from_server);
            }
        }

        private void chessboard_reflect(ReflectionType reflection)
        {
            if (chessboard_container.InvokeRequired)
            {
                ReflectionTypeArgReturningVoidDelegate d =
                    new ReflectionTypeArgReturningVoidDelegate(
                        chessboard_reflect);
                this.Invoke(d, new object[] { reflection });
            }
            else
            {
                this.chessboard_container.reflect(reflection);
            }
        }

        #endregion
    }
}
