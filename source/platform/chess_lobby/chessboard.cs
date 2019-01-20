using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platform.chess_lobby
{
    public partial class Chessboard : UserControl
    {
        #region ' Constructors '
 
        public Chessboard()
        {
            InitializeComponent();
        }

        #endregion

        #region ' Properties '

        private Int32 gird_side_length
        {
            get
            {
                return Math.Min(this.Width / 9, this.Height / 10);
            }
        }

        /// <summary>
        /// 存储了每一个grid_point坐标的二维数组
        /// </summary>
        private Point[,] grid_points
        {
            get
            {
                return this._grid_points;
            }
        }

        /// <summary>
        /// 存储了frame四个角坐标的二维数组
        /// </summary>
        private Point[,] edge_points
        {
            get
            {
                return this._edge_points;
            }
        }
        
        /// <summary>
        /// 普通线的宽度
        /// </summary>
        private float thin_pen_width { get { return 1.0F; } }
        /// <summary>
        /// 兵炮位置标记线的宽度
        /// </summary>
        private float medium_pen_width { get { return 2.0F; } }
        /// <summary>
        /// 边框线的宽度
        /// </summary>
        private float fat_pen_width { get { return 4.0F; } }
        /// <summary>
        /// 边框线与普通线的距离
        /// </summary>
        private Int32 frame_gap { get { return 6; } }
        /// <summary>
        /// 兵炮位置标记线与普通线的距离
        /// </summary>
        private Int32 cross_gap { get { return 4; } }
        /// <summary>
        /// 兵炮位置标记线的长度
        /// </summary>
        private Int32 cross_length { get { return 10; } }


        #endregion

        #region ' Intermediate Properties '

        private Point[,] _grid_points
        {
            get
            {
                Point[,] points = new Point[9, 10];
                for (Int32 x = 0; x < 9; x++)
                    for (Int32 y = 0; y < 10; y++)
                        points[x, y] = new Point(this._grid_xs[x], this._grid_ys[y]);
                return points;
            }
        }

        private Int32[] _grid_xs
        {
            get
            {
                Int32[] xs = new Int32[9];
                xs[0] = (this.Width - this.gird_side_length * 8) / 2;
                for(Int32 i = 1; i < 9; i++)
                {
                    xs[i] = xs[i - 1] + this.gird_side_length;
                }
                return xs;
            }
        }

        private Int32[] _grid_ys
        {
            get
            {
                Int32[] ys = new Int32[10];
                ys[0] = (this.Height - this.gird_side_length * 9) / 2;
                for (Int32 i = 1; i < 10; i++)
                {
                    ys[i] = ys[i - 1] + this.gird_side_length;
                }
                Array.Reverse(ys);
                return ys;
            }
        }

        private Point[,] _edge_points
        {
            get
            {
                Point[,] points = new Point[2, 2];
                points[0, 0] = new Point(
                    this.grid_points[0, 0].X - this.frame_gap,
                    this.grid_points[0, 0].Y + this.frame_gap);
                points[0, 1] = new Point(
                    this.grid_points[0, 9].X - this.frame_gap,
                    this.grid_points[0, 9].Y - this.frame_gap);
                points[1, 0] = new Point(
                    this.grid_points[8, 0].X + this.frame_gap,
                    this.grid_points[8, 0].Y + this.frame_gap);
                points[1, 1] = new Point(
                    this.grid_points[8, 9].X + this.frame_gap,
                    this.grid_points[8, 9].Y - this.frame_gap);
                return points;
            }
        }

        #endregion

        #region ' Painting '

        public void on_paint(object sender, PaintEventArgs pe)
        {
            Graphics graphics = pe.Graphics;
            this.paint_board(graphics);
        }

        private void paint_board(Graphics graphics)
        {
            #region ' Declare Pens '

            Pen slim_pen = new Pen(Brushes.Black)
            {
                Width = this.thin_pen_width,
                LineJoin = LineJoin.Miter,
            };

            Pen medium_pen = new Pen(Brushes.Black)
            {
                Width = this.medium_pen_width,
                LineJoin = LineJoin.Miter,
            };

            Pen fat_pen = new Pen(Brushes.Black)
            {
                Width = this.fat_pen_width,
                LineJoin = LineJoin.Miter,
            };

            #endregion

            #region ' Paint Horizontal Lines '

            for (Int32 y = 0; y < 10; y++)
                graphics.DrawLine(slim_pen, grid_points[0, y], grid_points[8, y]);

            #endregion

            #region ' Paint Vertical Lines '

            for (Int32 x = 0; x < 9; x++)
                if (x == 0 || x == 8)
                    graphics.DrawLine(slim_pen, grid_points[x, 0], grid_points[x, 9]);
                else
                {
                    graphics.DrawLine(slim_pen, grid_points[x, 0], grid_points[x, 4]);
                    graphics.DrawLine(slim_pen, grid_points[x, 5], grid_points[x, 9]);
                }

            #endregion

            #region ' Paint Palaces '

            graphics.DrawLine(slim_pen, grid_points[3, 0], grid_points[5, 2]);
            graphics.DrawLine(slim_pen, grid_points[5, 0], grid_points[3, 2]);
            graphics.DrawLine(slim_pen, grid_points[3, 9], grid_points[5, 7]);
            graphics.DrawLine(slim_pen, grid_points[5, 9], grid_points[3, 7]);

            #endregion

            #region ' Paint the Frame '

            graphics.DrawLines(fat_pen,
                new Point[6]
                {
                    edge_points[0, 0],
                    edge_points[0, 1],
                    edge_points[1, 1],
                    edge_points[1, 0],
                    edge_points[0, 0],
                    edge_points[0, 1]
                });

            #endregion

            #region ' Paint Crosses '

            for (Int32 x = 0; x <= 8; x += 2)
                for (Int32 y = 3; y <= 6; y += 3)
                {
                    if (x != 0)
                    {
                        graphics.DrawLines(medium_pen,
                            new Point[3]
                            {
                                new Point(
                                    grid_points[x, y].X - this.cross_gap - this.cross_length,
                                    grid_points[x, y].Y - this.cross_gap),
                                new Point(
                                    grid_points[x, y].X - this.cross_gap,
                                    grid_points[x, y].Y - this.cross_gap),
                                new Point(
                                    grid_points[x, y].X - this.cross_gap,
                                    grid_points[x, y].Y - this.cross_gap - this.cross_length)
                            });
                        graphics.DrawLines(medium_pen,
                            new Point[3]
                            {
                                new Point(
                                    grid_points[x, y].X - this.cross_gap - this.cross_length,
                                    grid_points[x, y].Y + this.cross_gap),
                                new Point(
                                    grid_points[x, y].X - this.cross_gap,
                                    grid_points[x, y].Y + this.cross_gap),
                                new Point(
                                    grid_points[x, y].X - this.cross_gap,
                                    grid_points[x, y].Y + this.cross_gap + this.cross_length)
                            });
                    }
                    if (x != 8)
                    {
                        graphics.DrawLines(medium_pen,
                            new Point[3]
                            {
                                new Point(
                                    grid_points[x, y].X + this.cross_gap + this.cross_length,
                                    grid_points[x, y].Y - this.cross_gap),
                                new Point(
                                    grid_points[x, y].X + this.cross_gap,
                                    grid_points[x, y].Y - this.cross_gap),
                                new Point(
                                    grid_points[x, y].X + this.cross_gap,
                                    grid_points[x, y].Y - this.cross_gap - this.cross_length)
                            });
                        graphics.DrawLines(medium_pen,
                            new Point[3]
                            {
                                new Point(
                                    grid_points[x, y].X + this.cross_gap + this.cross_length,
                                    grid_points[x, y].Y + this.cross_gap),
                                new Point(
                                    grid_points[x, y].X + this.cross_gap,
                                    grid_points[x, y].Y + this.cross_gap),
                                new Point(
                                    grid_points[x, y].X + this.cross_gap,
                                    grid_points[x, y].Y + this.cross_gap + this.cross_length)
                            });
                    }
                }

            for (Int32 x = 1; x <= 7; x += 6)
                for (Int32 y = 2; y <= 7; y += 5)
                {
                    graphics.DrawLines(medium_pen,
                        new Point[3]
                        {
                            new Point(
                                grid_points[x, y].X - this.cross_gap - this.cross_length,
                                grid_points[x, y].Y - this.cross_gap),
                            new Point(
                                grid_points[x, y].X - this.cross_gap,
                                grid_points[x, y].Y - this.cross_gap),
                            new Point(
                                grid_points[x, y].X - this.cross_gap,
                                grid_points[x, y].Y - this.cross_gap - this.cross_length)
                        });
                    graphics.DrawLines(medium_pen,
                        new Point[3]
                        {
                            new Point(
                                grid_points[x, y].X - this.cross_gap - this.cross_length,
                                grid_points[x, y].Y + this.cross_gap),
                            new Point(
                                grid_points[x, y].X - this.cross_gap,
                                grid_points[x, y].Y + this.cross_gap),
                            new Point(
                                grid_points[x, y].X - this.cross_gap,
                                grid_points[x, y].Y + this.cross_gap + this.cross_length)
                        });
                    graphics.DrawLines(medium_pen,
                        new Point[3]
                        {
                            new Point(
                                grid_points[x, y].X + this.cross_gap + this.cross_length,
                                grid_points[x, y].Y - this.cross_gap),
                            new Point(
                                grid_points[x, y].X + this.cross_gap,
                                grid_points[x, y].Y - this.cross_gap),
                            new Point(
                                grid_points[x, y].X + this.cross_gap,
                                grid_points[x, y].Y - this.cross_gap - this.cross_length)
                        });
                    graphics.DrawLines(medium_pen,
                        new Point[3]
                        {
                            new Point(
                                grid_points[x, y].X + this.cross_gap + this.cross_length,
                                grid_points[x, y].Y + this.cross_gap),
                            new Point(
                                grid_points[x, y].X + this.cross_gap,
                                grid_points[x, y].Y + this.cross_gap),
                            new Point(
                                grid_points[x, y].X + this.cross_gap,
                                grid_points[x, y].Y + this.cross_gap + this.cross_length)
                        });
                }

            #endregion

            slim_pen.Dispose();
            medium_pen.Dispose();
            fat_pen.Dispose();
        }

        #endregion
    }
}
