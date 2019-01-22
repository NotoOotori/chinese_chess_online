using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace platform.chess_lobby
{
    /// <summary>
    /// 作为格点的<see cref="Panel"/>
    /// </summary>
    public class GridPanel : Panel
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.GridPanel"/>类的新实例
        /// </summary>
        public GridPanel()
        {
            ;
        }

        #endregion

        #region ' Methods '

        protected override void OnClick(EventArgs e)
        {
            MessageBox.Show((this.Tag as GridPanelTag).coordinate.ToString());
            base.OnClick(e);
        }

        #endregion
    }

    /// <summary>
    /// <see cref="GridPanel"/>的标签, 标注坐标以及棋子
    /// </summary>
    /// <returns></returns>
    public class GridPanelTag
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.GridPanelTag"/>类的新实例
        /// </summary>
        /// <param name="coordinate">格子的坐标</param>
        public GridPanelTag(Coordinate coordinate)
        {
            this.coordinate = coordinate;
        }

        /// <summary>
        /// 初始化<see cref="chess_lobby.GridPanelTag"/>类的新实例
        /// </summary>
        /// <param name="coordinate_str">格子的坐标字符串</param>
        public GridPanelTag(String coordinate_str)
        {
            this.coordinate = new Coordinate(coordinate_str);
        }

        /// <summary>
        /// 初始化<see cref="chess_lobby.GridPanelTag"/>类的新实例
        /// </summary>
        /// <param name="col_num">格子的横坐标数字</param>
        /// <param name="row_num">格子的纵坐标数字</param>
        public GridPanelTag(Int32 col_num, Int32 row_num)
        {
            this.coordinate = new Coordinate(col_num, row_num);
        }

        #endregion

        #region ' Properties '

        public Coordinate coordinate { get; }

        /// <summary>
        /// 格子上棋子的简化字符格式('[bw][abcknpr]')
        /// </summary>
        public String piece_str
        {
            get { return this._piece_str; }
            set { this._piece_str = piece_str; }
        }

        private String _piece_str { get; set; }

        #endregion
    }
}
