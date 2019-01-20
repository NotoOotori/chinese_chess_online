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
    class Coordinate
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.Coordinate"/>的新实例
        /// </summary>
        /// <param name="coordinate_str">坐标的字符串格式</param>
        public Coordinate(String coordinate_str)
        {
            if (Regex.IsMatch(coordinate_str, "[a-i][0-9]"))
                this._coordinate_str = coordinate_str;
            else
                throw new ArgumentOutOfRangeException(
                    $"坐标字符串不正确, 现为{coordinate_str}, 应为\"[a-i][0-9]\"");
        }

        #endregion

        #region ' Properties '

        public String coordinate_str
        {
            get { return this._coordinate_str; }
            set { this._coordinate_str = coordinate_str; }
        }

        private String _coordinate_str { get; set; }

        #endregion
    }
}
