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

        #region ' Properties '

        private GridPanelTag tag { get { return this.Tag as GridPanelTag; } }
        /// <summary>
        /// 该格点的坐标
        /// </summary>
        public Coordinate coordinate { get { return this.tag.coordinate; } }
        /// <summary>
        /// 该格点上的棋子
        /// </summary>
        public Piece piece { get { return this.tag.piece; } }

        #endregion

        #region ' Methods '

        protected override void OnClick(EventArgs e)
        {
            if (this.piece == null)
                MessageBox.Show("NULL!");
            else
            {
                MessageBox.Show(this.piece.ToString());
                this.piece.masked = true;
                this.refresh_image();
            }
            base.OnClick(e);
        }

        /// <summary>
        /// 反射棋子
        /// </summary>
        /// <param name="type">反射类型</param>
        public void reflect(ReflectionType type)
        {
            this.tag.coordinate.reflect(type);
        }

        /// <summary>
        /// 刷新棋子图像
        /// </summary>
        public void refresh_image()
        {
            if (this.piece == null)
                return;
            this.BackgroundImage = this.piece.bitmap;
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
        public Piece piece { get; set; }

        #endregion

        #region ' Methods '
        
        #endregion
    }
}
