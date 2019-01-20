using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace platform.chess_lobby
{
    /// <summary>
    /// <see cref="chess_lobby.GridPanel"/>的标签, 标注坐标以及棋子
    /// </summary>
    /// <returns></returns>
    class GridPanelTag
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.GridPanelTag"/>类的新实例
        /// </summary>
        /// <param name="coordinate_str">格子在棋盘上的坐标</param>
        /// <param name="piece_str">格子上棋子的简化字符格式('[bw][abcknpr]')</param>
        public GridPanelTag(String coordinate_str, String piece_str="")
        {
            this.coordinate = new Coordinate(coordinate_str);
            this._piece_str = piece_str;
        }

        #endregion

        #region ' Properties '

        public Coordinate coordinate { get; }

        public String piece_str
        {
            get { return this._piece_str; }
            set { this._piece_str = piece_str; }
        }
        
        private String _piece_str { get; set; }

        #endregion
    }
}
