using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace platform.chess_lobby
{
    /// <summary>
    /// 表示格点坐标的类
    /// </summary>
    public class Coordinate : Tuple<ColumnCoordinate, RowCoordinate>
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.Coordinate"/>的新实例
        /// </summary>
        /// <param name="cdn_str">坐标的字符串格式</param>
        public Coordinate(String cdn_str)
            : base(new ColumnCoordinate(cdn_str.Substring(0, 1)),
                   new RowCoordinate(cdn_str.Substring(1, 1)))
        {
            ;
        }

        /// <summary>
        /// 初始化<see cref="chess_lobby.Coordinate"/>的新实例
        /// </summary>
        /// <param name="col_num">横坐标的数字</param>
        /// <param name="row_num">列坐标的数字</param>
        public Coordinate(Int32 col_num, Int32 row_num)
            : base(new ColumnCoordinate(col_num),
                   new RowCoordinate(row_num))
        {
            ;
        }

        #endregion

        #region ' Properties '

        /// <summary>
        /// 所在列的坐标
        /// </summary>
        public ColumnCoordinate x
        {
            get
            {
                return base.Item1;
            }
        }

        /// <summary>
        /// 所在行的坐标
        /// </summary>
        public RowCoordinate y
        {
            get
            {
                return base.Item2;
            }
        }

        private new ColumnCoordinate Item1
        {
            get
            {
                return base.Item1;
            }
        }
        private new RowCoordinate Item2
        {
            get
            {
                return base.Item2;
            }
        }

        #endregion

        #region ' Methods '

        public override String ToString()
        {
            return this.x.ToString() + this.y.ToString();
        }

        public (Int32, Int32) ToTuple()
        {
            return (this.x.value, this.y.value);
        }

        #endregion
    }

    /// <summary>
    /// 表示格点横坐标的类
    /// </summary>
    public class ColumnCoordinate
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.ColumnCoordinate"/>的新实例
        /// </summary>
        /// <param name="col_str">横坐标的字符</param>
        public ColumnCoordinate(String col_str)
        {
            if(Regex.IsMatch(col_str, "[a-i]"))
                this._col_num = Char.Parse(col_str) - 'a';
            else
                throw new ArgumentOutOfRangeException(
                    $"横坐标字符不正确, 现为{col_str}, 应为\"[a-i]\".");
        }

        /// <summary>
        /// 初始化<see cref="chess_lobby.ColumnCoordinate"/>的新实例
        /// </summary>
        /// <param name="col_num">横坐标的数字</param>
        public ColumnCoordinate(Int32 col_num)
        {
            if (col_num >= 0 && col_num < 9)
                this._col_num = col_num;
            else
                throw new ArgumentOutOfRangeException(
                    $"横坐标数字不正确, 现为{col_num}, 应在[0, 9)中.");
        }

        #endregion

        #region ' Properties '

        private Int32 _col_num { get; set; }
        /// <summary>
        /// 坐标数字
        /// </summary>
        public Int32 value { get { return this._col_num; } }

        #endregion

        #region ' Methods '

        public override String ToString()
        {
            return Convert.ToChar(this._col_num + 'a').ToString();
        }

        #endregion
    }

    /// <summary>
    /// 表示格点纵坐标的类
    /// </summary>
    public class RowCoordinate
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.RowCoordinate"/>的新实例
        /// </summary>
        /// <param name="row_str">纵坐标的字符</param>
        public RowCoordinate(String row_str)
        {
            if(Regex.IsMatch(row_str, "[0-9]"))
                this._row_num = Int32.Parse(row_str);
            else
                throw new ArgumentOutOfRangeException(
                    $"纵坐标字符不正确, 现为{row_str}, 应为\"[0-9]\".");
        }

        /// <summary>
        /// 初始化<see cref="chess_lobby.RowCoordinate"/>的新实例
        /// </summary>
        /// <param name="row_num">横坐标的数字</param>
        public RowCoordinate(Int32 row_num)
        {
            if (row_num >= 0 && row_num < 10)
                this._row_num = row_num;
            else
                throw new ArgumentOutOfRangeException(
                    $"横坐标数字不正确, 现为{row_num}, 应在[0, 10)中.");
        }

        #endregion

        #region ' Properties '

        private Int32 _row_num { get; set; }
        /// <summary>
        /// 坐标数字
        /// </summary>
        public Int32 value { get { return this._row_num; } }

        #endregion

        #region ' Methods '

        public override String ToString()
        {
            return this._row_num.ToString();
        }

        #endregion
    }
}
