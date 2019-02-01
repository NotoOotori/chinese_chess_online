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
        /// 初始化<see cref="Coordinate"/>的新实例.
        /// </summary>
        /// <param name="cdn_str">坐标的字符串格式</param>
        public Coordinate(String cdn_str)
            : base(new ColumnCoordinate(cdn_str.Substring(0, 1)),
                   new RowCoordinate(cdn_str.Substring(1, 1)))
        {
            ;
        }

        /// <summary>
        /// 初始化<see cref="chess_lobby.Coordinate"/>的新实例.
        /// </summary>
        /// <param name="col">横坐标</param>
        /// <param name="row">纵坐标</param>
        public Coordinate(ColumnCoordinate col, RowCoordinate row)
            : base(col, row)
        {
            ;
        }

        /// <summary>
        /// 初始化<see cref="chess_lobby.Coordinate"/>的新实例.
        /// </summary>
        /// <param name="col_num">横坐标的数字</param>
        /// <param name="row_num">纵坐标的数字</param>
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
                return this.Item1;
            }
        }
        
        /// <summary>
        /// 所在行的坐标
        /// </summary>
        public RowCoordinate y
        {
            get
            {
                return this.Item2;
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

        /// <summary>
        /// 反射坐标
        /// </summary>
        /// <param name="type">反射类型</param>
        public Coordinate reflect(ReflectionType type)
        {
            return new Coordinate(this.x.reflect(type), this.y.reflect(type));
        }

        /// <summary>
        /// 返回2位坐标字符串.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return this.x.ToString() + this.y.ToString();
        }

        /// <summary>
        /// 返回横坐标和纵坐标数值.
        /// </summary>
        /// <returns></returns>
        public (Int32, Int32) ToValueTuple()
        {
            return (this.x.value, this.y.value);
        }

        /// <summary>
        /// 返回遍历所有坐标的<see cref="IEnumerable{T}"/>
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Coordinate> get_coordinates()
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            for (Int32 x = 0; x < 9; x++)
                for (Int32 y = 0; y < 10; y++)
                    coordinates.Add(new Coordinate(x, y));
            return coordinates;
        }

        public static CoordinateDelta operator -(Coordinate end, Coordinate start)
        {
            return new CoordinateDelta(end, start);
        }

        public static Coordinate operator +(Coordinate start, CoordinateDelta delta)
        {
            return new Coordinate(start.x + delta.x, start.y + delta.y);
        }


        /// <summary>
        /// Returns a value that indicates whether the current
        /// <see cref="Coordinate"/> object is equal to a specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            Coordinate cdn = obj as Coordinate;
            if (this.x.value == cdn.x.value && this.y.value == cdn.y.value)
                return true;
            return false;
        }

        /// <summary>
        /// Returns the hash code for the current
        /// <see cref="Coordinate"/> object.
        /// </summary>
        /// <returns></returns>
        public override Int32 GetHashCode()
            {
                return this.x.value + this.y.value * 10;
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
        public Int32 value { get { return _col_num; } }

        #endregion

        #region ' Methods '

        /// <summary>
        /// 反射棋子
        /// </summary>
        /// <param name="type">反射类型</param>
        public ColumnCoordinate reflect(ReflectionType type)
        {
            if ((type & ReflectionType.HorizontalReflection)
                != ReflectionType.HorizontalReflection)
                return new ColumnCoordinate(this._col_num);
            return new ColumnCoordinate(8 - this._col_num);
        }

        public override String ToString()
        {
            return Convert.ToChar(this.value + 'a').ToString();
        }

        public static Int32 operator -(ColumnCoordinate end, ColumnCoordinate start)
        {
            return end.value - start.value;
        }

        public static ColumnCoordinate operator +(ColumnCoordinate start, Int32 delta)
        {
            return new ColumnCoordinate(start.value + delta);
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

        /// <summary>
        /// 反射棋子
        /// </summary>
        /// <param name="type">反射类型</param>
        public RowCoordinate reflect(ReflectionType type)
        {
            if ((type & ReflectionType.VerticalReflection)
                   != ReflectionType.VerticalReflection)
                return new RowCoordinate(this._row_num);
            return new RowCoordinate(9 - this._row_num);
        }

        public override String ToString()
        {
            return this.value.ToString();
        }

        public static Int32 operator -(RowCoordinate end, RowCoordinate start)
        {
            return end.value - start.value;
        }

        public static RowCoordinate operator +(RowCoordinate start, Int32 delta)
        {
            return new RowCoordinate(start.value + delta);
        }

        #endregion
    }

    /// <summary>
    /// 表示坐标之间差的类
    /// </summary>
    public class CoordinateDelta : Tuple<Int32, Int32>
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="CoordinateDelta"/>的新实例.
        /// </summary>
        /// <param name="delta_x">横坐标之差</param>
        /// <param name="delta_y">纵坐标之差</param>
        public CoordinateDelta(Int32 delta_x, Int32 delta_y)
            : base(delta_x, delta_y)
        {; }

        /// <summary>
        /// 初始化<see cref="CoordinateDelta"/>的新实例.
        /// </summary>
        /// <param name="end">终点坐标</param>
        /// <param name="start">起点坐标</param>
        public CoordinateDelta(Coordinate end, Coordinate start)
            : base(end.x - start.x, end.y - start.y)
        {; }

        #endregion

        #region ' Properties '

        private new Int32 Item1 { get { return base.Item1; } }
        private new Int32 Item2 { get { return base.Item2; } }

        public Int32 x { get { return this.Item1; } }
        public Int32 y { get { return this.Item2; } }

        #endregion

        #region ' Methods '

        public CoordinateDelta abs()
        {
            return new CoordinateDelta(Math.Abs(this.x), Math.Abs(this.y));
        }
        
        /// <summary>
        /// 仅从偏移量角度来筛选出不合法走子,
        /// 不考虑子所在的位置, 不考虑兵的前进后退.
        /// </summary>
        /// <param name="delta">偏移量</param>
        /// <param name="piece">棋子种类</param>
        /// <returns></returns>
        public static Boolean is_valid(CoordinateDelta delta, PieceType piece)
        {
            switch (piece)
            {
                default:
                    throw new ArgumentOutOfRangeException("棋子种类越界!");
                case PieceType.ADVISOR:
                    if (delta.abs().Equals(new CoordinateDelta(1, 1)))
                        return true;
                    return false;
                case PieceType.BISHOP:
                    if (delta.abs().Equals(new CoordinateDelta(2, 2)))
                        return true;
                    return false;
                case PieceType.CANNON:
                case PieceType.ROOK:
                    if (delta.x == 0 || delta.y == 0)
                        return true;
                    return false;
                case PieceType.KING:
                case PieceType.PAWN:
                    if (delta.abs().Equals(new CoordinateDelta(1, 0))
                        || delta.abs().Equals(new CoordinateDelta(0, 1)))
                        return true;
                    return false;
                case PieceType.KNIGHT:
                    if (delta.abs().Equals(new CoordinateDelta(2, 1))
                        || delta.abs().Equals(new CoordinateDelta(1, 2)))
                        return true;
                    return false;
            }
        }

        public static CoordinateDelta operator /(CoordinateDelta delta, Int32 divisor)
        {
            return new CoordinateDelta(delta.x / divisor, delta.y / divisor);
        }

        /// <summary>
        /// Returns a value that indicates whether the current
        /// <see cref="CoordinateDelta"/> object is equal to a specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            CoordinateDelta delta = obj as CoordinateDelta;
            if (this.x == delta.x && this.y == delta.y)
                return true;
            return false;
        }

        /// <summary>
        /// Returns the hash code for the current
        /// <see cref="CoordinateDelta"/> object.
        /// </summary>
        /// <returns></returns>
        public override Int32 GetHashCode()
        {
            return 8 + this.x + this.y * 17;
        }

        #endregion
    }
}
