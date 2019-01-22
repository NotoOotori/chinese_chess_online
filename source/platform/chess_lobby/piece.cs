using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="piece_char">棋子字符(1位)</param>
        public Piece(Char piece_char)
        {
            if (!Char.IsLetter(piece_char))
                throw new ArgumentOutOfRangeException("棋子字符不是字母!");
            if (Char.IsUpper(piece_char))
                this._colour = PlayerColour.RED;
            else
                this._colour = PlayerColour.BLACK;
            switch (Char.ToLower(piece_char))
            {
                default:
                    throw new ArgumentOutOfRangeException("棋子种类错误!");
                case 'k':
                    this._type = PieceType.KING;
                    break;
                case 'a':
                    this._type = PieceType.ADVISOR;
                    break;
                case 'b':
                    this._type = PieceType.BISHOP;
                    break;
                case 'n':
                    this._type = PieceType.KNIGHT;
                    break;
                case 'r':
                    this._type = PieceType.ROOK;
                    break;
                case 'c':
                    this._type = PieceType.CANNON;
                    break;
                case 'p':
                    this._type = PieceType.PAWN;
                    break;
            }
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
            switch (piece_str[0])
            {
                default:
                    throw new ArgumentOutOfRangeException("棋子颜色错误!");
                case 'r':
                    this._colour = PlayerColour.RED;
                    break;
                case 'b':
                    this._colour = PlayerColour.BLACK;
                    break;
            }
            switch (piece_str[1])
            {
                default:
                    throw new ArgumentOutOfRangeException("棋子种类错误!");
                case 'k':
                    this._type = PieceType.KING;
                    break;
                case 'a':
                    this._type = PieceType.ADVISOR;
                    break;
                case 'b':
                    this._type = PieceType.BISHOP;
                    break;
                case 'n':
                    this._type = PieceType.KNIGHT;
                    break;
                case 'r':
                    this._type = PieceType.ROOK;
                    break;
                case 'c':
                    this._type = PieceType.CANNON;
                    break;
                case 'p':
                    this._type = PieceType.PAWN;
                    break;
            }
        }

        /// <summary>
        /// 初始化<see cref="Piece"/>类的新实例
        /// </summary>
        /// <param name="colour">棋子颜色</param>
        /// <param name="type">棋子种类</param>
        public Piece(PlayerColour colour, PieceType type)
        {
            this._colour = colour;
            this._type = type;
        }

        #endregion

        #region ' Properties '

        private PlayerColour _colour { get; set; }
        private PieceType _type { get; set; }

        /// <summary>
        /// 棋子颜色
        /// </summary>
        public PlayerColour colour { get { return this._colour; } }
        /// <summary>
        /// 棋子种类
        /// </summary>
        public PieceType type { get { return this._type; } }
        /// <summary>
        /// 棋子图片
        /// </summary>
        public Bitmap bitmap
        {
            get
            {
                return (Bitmap)Properties.Resources.
                    ResourceManager.GetObject(this.ToString());
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
            Char c = this.type.ToString()[0];
            if (this.type == PieceType.KNIGHT)
                c = 'N';
            return this.colour == PlayerColour.RED ? c : Char.ToLower(c);
        }

        /// <summary>
        /// 输出棋子的2位字符串表示
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return (this.colour.ToString().Substring(0, 1) +
                this.ToChar()).ToLower();
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
        KING = 1,
        /// <summary>
        /// 仕/士
        /// </summary>
        ADVISOR = 2,
        /// <summary>
        /// 相/象
        /// </summary>
        BISHOP = 3,
        /// <summary>
        /// 傌/馬
        /// </summary>
        KNIGHT = 4,
        /// <summary>
        /// 俥/車
        /// </summary>
        ROOK = 5,
        /// <summary>
        /// 炮/砲
        /// </summary>
        CANNON = 6,
        /// <summary>
        /// 兵/卒
        /// </summary>
        PAWN = 7
    }
}
