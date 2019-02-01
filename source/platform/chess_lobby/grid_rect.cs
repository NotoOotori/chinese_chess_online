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
    public class GridRect : Rect
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.GridRect"/>类的新实例
        /// </summary>
        public GridRect(Int32 width, Int32 height):
            base(width, height)
        {
            ;
        }

        #endregion

        #region ' Properties '

        private Image _image { get; set; }

        /// <summary>
        /// 该格点的图片
        /// </summary>
        private Image image
        {
            set
            {
                if (value == _image)
                {
                    ;
                }
                else if (value == null)
                {
                    this.piece_box = null;
                }
                else
                {
                    this.piece_box = new PieceBox()
                    {
                        Width = Convert.ToInt32(this.width),
                        Height = Convert.ToInt32(this.height),
                        Top = Convert.ToInt32(this.top),
                        Left = Convert.ToInt32(this.left),
                        BackColor = Color.Transparent,
                        SizeMode = PictureBoxSizeMode.CenterImage,
                        Image = value,
                        Parent = parent,
                    };
                    this.piece_box.GridClick += this.parent.Chessboard_MouseClick;
                    this.piece_box.BringToFront();
                }
                this._image = value;
            }
        }

        /// <summary>
        /// 该格点上的<see cref="PictureBox"/>.
        /// </summary>
        private PieceBox piece_box
        {
            get { return this._piece_box; }
            set
            {
                try
                {
                    this._piece_box.Dispose();
                }
                catch(NullReferenceException)
                {
                    ;
                }
                this._piece_box = value;
            }
        }

        private PieceBox _piece_box { get; set; }

        /// <summary>
        /// 父控件<see cref="Chessboard"/>.
        /// </summary>
        public Chessboard parent { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public GridRectTag tag { get; set; }
        /// <summary>
        /// 该格点的坐标
        /// </summary>
        public Coordinate coordinate { get { return this.tag.coordinate; } }
        /// <summary>
        /// 该格点上的棋子
        /// </summary>
        public Piece piece { get { return this.tag.piece; } }
        /// <summary>
        /// 棋子是否为masked状态
        /// </summary>
        public Boolean masked {
            get { return this.piece.masked; }
            set { this.piece.masked = value; this.refresh_image(); }
        }

        #endregion

        #region ' Methods '

        /// <summary>
        /// 刷新棋子图像
        /// </summary>
        public void refresh_image()
        {
            this.image = this.piece.bitmap;
        }

        #endregion
    }

    /// <summary>
    /// <see cref="GridRect"/>的标签, 标注坐标以及棋子
    /// </summary>
    /// <returns></returns>
    public class GridRectTag
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.GridRectTag"/>类的新实例
        /// </summary>
        /// <param name="coordinate">格子的坐标</param>
        public GridRectTag(Coordinate coordinate)
        {
            this.coordinate = coordinate;
        }

        /// <summary>
        /// 初始化<see cref="chess_lobby.GridRectTag"/>类的新实例
        /// </summary>
        /// <param name="coordinate_str">格子的坐标字符串</param>
        public GridRectTag(String coordinate_str)
        {
            this.coordinate = new Coordinate(coordinate_str);
        }

        /// <summary>
        /// 初始化<see cref="chess_lobby.GridRectTag"/>类的新实例
        /// </summary>
        /// <param name="col_num">格子的横坐标数字</param>
        /// <param name="row_num">格子的纵坐标数字</param>
        public GridRectTag(Int32 col_num, Int32 row_num)
        {
            this.coordinate = new Coordinate(col_num, row_num);
        }

        #endregion

        #region ' Properties '

        public Coordinate coordinate { get; }

        /// <summary>
        /// 格子上棋子的简化字符格式('[bw][abcknpr]')
        /// </summary>
        public Piece piece { get; set; } = new Piece();

        #endregion

        #region ' Methods '
        
        #endregion
    }
}
