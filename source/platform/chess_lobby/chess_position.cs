using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public ChessPosition(String fen=InitialFEN.value)
        {
            String[] strings = fen.Split(' ');
            if (strings.Length != 6)
                throw new ArgumentOutOfRangeException("FEN值长度错误!");
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
                            this.Add(new Coordinate(x++, y), null);
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

        #endregion

        #region ' Methods '

        public ChessPosition move(Coordinate start, Coordinate end)
        {
            return this;
        }

        /// <summary>
        /// 返回棋局的FEN串
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return base.ToString();
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
