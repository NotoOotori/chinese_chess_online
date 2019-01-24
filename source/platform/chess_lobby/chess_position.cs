using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace platform.chess_lobby
{
    /// <summary>
    /// 记录棋局状态的类
    /// </summary>
    public class ChessPosition : Dictionary<Coordinate, Piece>
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="ChessPosition"/>的新实例
        /// </summary>
        /// <param name="fen">棋局的FEN串</param>
        public ChessPosition(String fen = InitialFEN.value)
        {
            String[] strings = fen.Split(' ');
            if (strings.Length != 6)
                throw new ArgumentOutOfRangeException("FEN值长度错误!");
            if (!Regex.IsMatch(strings[1], "[br]"))
                throw new ArgumentOutOfRangeException("走子方字符长度有误!");
            this._current_player = MyConvert.to_chess_colour(strings[1][0]);
            this.noncapture_moves = Int32.Parse(strings[4]);
            this.total_rounds = Int32.Parse(strings[5]);
            String[] rows = strings[0].Split('/');

            Int32 y = 9;
            foreach (String row in rows)
            {
                Int32 x = 0;
                foreach (Char c in row)
                {
                    if (y < 0)
                        throw new ArgumentOutOfRangeException("纵坐标越界!");
                    if (Char.IsDigit(c))
                    {
                        for (int i = 0; i < Char.GetNumericValue(c); i++)
                        {
                            if (x > 8)
                                throw new ArgumentOutOfRangeException(
                                    "横坐标越界!");
                            this.Add(new Coordinate(x++, y), new Piece());
                        }
                        continue;
                    }
                    if (Char.IsLetter(c))
                    {
                        if (x > 8)
                            throw new ArgumentOutOfRangeException("横坐标越界!");
                        this.Add(new Coordinate(x++, y), new Piece(c));
                        continue;
                    }
                    throw new ArgumentOutOfRangeException("字符错误!");
                }
                y--;
            }
        }

        /// <summary>
        /// 初始化<see cref="ChessPosition"/>的新实例
        /// </summary>
        /// <param name="chess_position"></param>
        public ChessPosition(ChessPosition chess_position)
            : this(chess_position.ToString())
        {
            ;
        }

        #endregion

        #region ' Properties '

        /// <summary>
        /// 最近一次吃子或者进兵后棋局进行的步数(半回合数)
        /// </summary>
        private Int32 noncapture_moves { get; set; }
        /// <summary>
        /// 棋局的回合数
        /// </summary>
        private Int32 total_rounds { get; set; }
        /// <summary>
        /// 轮到走子的那一方
        /// </summary>
        private ChessColour _current_player { get; set; }
        /// <summary>
        /// 轮到走子的那一方
        /// </summary>
        public ChessColour current_player { get { return this._current_player; } }
        /// <summary>
        /// 棋局的fen串
        /// </summary>
        public String fen { get { return this.ToString(); } }

    #endregion

    #region ' Methods '

    /// <summary>
    /// 判断棋步是否合法
    /// </summary>
    /// <param name="start">起始坐标</param>
    /// <param name="end">终止坐标</param>
    /// <returns></returns>
    public Boolean is_move(Coordinate start, Coordinate end)
        {
            return true;
        }

        /// <summary>
        /// 完成棋子的移动以及回合数的更新
        /// </summary>
        /// <param name="start">起始坐标</param>
        /// <param name="end">终止坐标</param>
        /// <returns></returns>
        public ChessPosition move(Coordinate start, Coordinate end)
        {
            ChessPosition new_position = new ChessPosition(this);

            #region ' Update the Stats '

            new_position._current_player =
                ChessColour.NONE ^ new_position._current_player;
            if (this[end] == null)
                new_position.noncapture_moves += 1;
            else
                new_position.noncapture_moves = 0;
            if (this._current_player == ChessColour.BLACK)
                new_position.total_rounds += 1;

            #endregion

            #region ' Move the Piece ' 

            new_position[end] = this[start];
            new_position[start] = new Piece();

            #endregion

            return new_position;
        }

        /// <summary>
        /// 返回棋局的FEN串
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            String fen_0 = "";
            for (Int32 y = 9; y >= 0; y--)
            {
                Int32 empty = 0;
                for (Int32 x = 0; x < 9; x++)
                {
                    Coordinate cdn = new Coordinate(x, y);
                    if (this[cdn].type == PieceType.NONE)
                    {
                        empty++;
                        continue;
                    }
                    if (empty != 0)
                    {
                        fen_0 += empty.ToString();
                        empty = 0;
                    }
                    fen_0 += this[cdn].ToChar().ToString();
                }
                if (empty != 0)
                    fen_0 += empty.ToString();
                if (y != 0)
                    fen_0 += "/";
            }
            return $"{fen_0} {Char.ToLower(this.current_player.ToString()[0])} " +
                $"- - {this.noncapture_moves} {this.total_rounds}";
        }

        #endregion
    }
    
    /// <summary>
    /// 初始局面FEN值
    /// </summary>
    public class InitialFEN
    {
        public const String value  = "rnbakabnr/9/1c5c1/p1p1p1p1p/" +
            "9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR r - - 0 1";
    }
}
