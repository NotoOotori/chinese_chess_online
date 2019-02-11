using System;
using System.Net.Sockets;
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
            
            this.chessboard.reflect(ReflectionType.PointReflection);
        }

        public UInt32 lobby_id { get; } = 0;
        public Socket server_socket { get; }
    }
}
