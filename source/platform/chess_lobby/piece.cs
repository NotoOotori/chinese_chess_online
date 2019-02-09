using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace platform.chess_lobby
{
    /// <summary>
    /// 棋子类.
    /// </summary>
    public class Piece
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="Piece"/>类的新实例
        /// </summary>
        public Piece()
        {
            ;
        }

        /// <summary>
        /// 初始化<see cref="Piece"/>类的新实例
        /// </summary>
        /// <param name="piece_char">棋子字符(1位)</param>
        public Piece(Char piece_char)
        {
            if (!Char.IsLetter(piece_char))
                throw new ArgumentOutOfRangeException("棋子字符不是字母!");
            this._colour = MyConvert.to_chess_colour(Char.IsUpper(piece_char));
            Char piece_lower = Char.ToLower(piece_char);
            if (!Enum.IsDefined(typeof(PieceType), Convert.ToInt32(piece_lower)))
                throw new ArgumentOutOfRangeException("棋子字母错误!");
            this._type = (PieceType)piece_lower;
        }

        /// <summary>
        /// 初始化<see cref="Piece"/>类的新实例
        /// </summary>
        /// <param name="piece_str">棋子字符串(2位小写)</param>
        public Piece(String piece_str)
        {
            piece_str = piece_str.ToLower();
            if (piece_str.Length != 2)
                throw new ArgumentOutOfRangeException("棋子字符串长度不匹配!");
            if(!Regex.IsMatch(piece_str.Substring(0, 1), "[br]"))
                throw new ArgumentOutOfRangeException("棋子颜色错误!");
            this._colour = MyConvert.to_chess_colour(piece_str[0] == 'r');
            if (!Enum.IsDefined(typeof(PieceType), Convert.ToInt32(piece_str[1])))
                throw new ArgumentOutOfRangeException("棋子字母错误!");
            this._type = (PieceType)piece_str[1];
        }

        /// <summary>
        /// 初始化<see cref="Piece"/>类的新实例
        /// </summary>
        /// <param name="colour">棋子颜色</param>
        /// <param name="type">棋子种类</param>
        public Piece(ChessColour colour, PieceType type)
        {
            this._colour = colour;
            this._type = type;
        }

        #endregion

        #region ' Properties '

        private ChessColour _colour { get; set; } = ChessColour.NONE;
        private PieceType _type { get; set; } = PieceType.NONE;

        /// <summary>
        /// 棋子颜色
        /// </summary>
        public ChessColour colour { get { return this._colour; } }
        /// <summary>
        /// 棋子种类
        /// </summary>
        public PieceType type { get { return this._type; } }
        public Boolean masked { get; set; } = false;
        /// <summary>
        /// 棋子图片(可能为空)
        /// </summary>
        public Bitmap bitmap
        {
            get
            {
                Bitmap mask = (Bitmap)Properties.Resources.
                    ResourceManager.GetObject("mm");
                try
                {
                    Bitmap piece = (Bitmap)Properties.Resources.
                        ResourceManager.GetObject(this.ToString());
                    if (!this.masked)
                        return piece;
                    using (Graphics graphics = Graphics.FromImage(piece))
                    {
                        // 高质量
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        // 高像素偏移质量
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        graphics.DrawImage(mask, 0, 0, mask.Width, mask.Height);
                    }
                    return piece;
                }
                catch(ArgumentNullException)
                {
                    if (this.masked)
                        return mask;
                    return null;
                }
            }
        } 

        #endregion

        #region ' Methods '

        //public List<Coordinate> get_moves(ChessPosition position);
        //public Boolean is_move(ChessPosition position);
        
        /// <summary>
        /// 输出棋子的1位字符表示
        /// </summary>
        /// <returns></returns>
        public Char ToChar()
        {
            Char c = Convert.ToChar(this.type);
            return this.colour == ChessColour.RED ? Char.ToUpper(c)
                : Char.ToLower(c);
        }

        /// <summary>
        /// 输出棋子的2位字符串表示
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return (this.colour.ToString().Substring(0, 1) +
                this.ToChar().ToString()).ToLower();
        }

        #endregion
    }

    /// <summary>
    /// 棋子种类
    /// </summary>
    public enum PieceType
    {
        /// <summary>
        /// 帥/將
        /// </summary>
        KING = 'k',
        /// <summary>
        /// 仕/士
        /// </summary>
        ADVISOR = 'a',
        /// <summary>
        /// 相/象
        /// </summary>
        BISHOP = 'b',
        /// <summary>
        /// 傌/馬
        /// </summary>
        KNIGHT = 'n',
        /// <summary>
        /// 俥/車
        /// </summary>
        ROOK = 'r',
        /// <summary>
        /// 炮/砲
        /// </summary>
        CANNON = 'c',
        /// <summary>
        /// 兵/卒
        /// </summary>
        PAWN = 'p',
        /// <summary>
        /// NULL
        /// </summary>
        NONE = 0
    }

    /// <summary>
    /// 呈现棋子的<see cref="PictureBox"/>.
    /// </summary>
    public class PieceBox : PictureBox
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="PieceBox/>的新实例
        /// </summary>
        public PieceBox()
        {
            this.MouseClick += PieceBox_MouseClick;
        }

        #endregion

        #region ' Fields and Properties '

        public event MouseEventHandler GridClick;

        #endregion

        #region ' Methods '

        protected virtual void OnGridClick(MouseEventArgs e)
        {
            GridClick?.Invoke(this, e);
        }
        private void PieceBox_MouseClick(object sender, MouseEventArgs e)
        {
            OnGridClick(e);
        }

        #endregion
    }

    /// <summary>
    /// 前中后一二三四五
    /// </summary>
    public enum PieceIdentifier
    {
        NONE = 0,
        ONE = 1,
        TWO = 2,
        THREE = 3,
        FOUR = 4,
        FIVE = 5,
        FRONT = 6,
        MIDDLE = 7,
        REAR = 8
    }

    public static class ChessLobbyExtension
    {
        public static String to_audio_string(this PieceIdentifier id)
        {
            switch (id)
            {
                default:
                    throw new InvalidEnumArgumentException();
                case PieceIdentifier.FRONT:
                case PieceIdentifier.MIDDLE:
                case PieceIdentifier.REAR:
                    return id.ToString().Substring(0, 2).ToLower();
                case PieceIdentifier.ONE:
                case PieceIdentifier.TWO:
                case PieceIdentifier.THREE:
                case PieceIdentifier.FOUR:
                case PieceIdentifier.FIVE:
                    return ((Int32)id).ToString();
                case PieceIdentifier.NONE:
                    return "";
            }
        }

        public static String to_chinese_format(
            this CoordinateDelta delta, ChessColour player)
        {
            Int32 value = Math.Abs(delta.y);
            if (player == ChessColour.RED)
                return "一二三四五六七八九".Substring(value - 1, 1);
            return "１２３４５６７８９".Substring(value - 1, 1);
        }

        public static String to_audio_string(this MoveDirection direction)
        {
            switch (direction)
            {
                default:
                    throw new InvalidEnumArgumentException();
                case MoveDirection.FORWARD:
                    return "ad";
                case MoveDirection.SIDEWARD:
                    return "tr";
                case MoveDirection.BACKWARD:
                    return "wi";
            }
        }
    }
}
