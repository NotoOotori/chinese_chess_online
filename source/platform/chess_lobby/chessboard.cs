using System;
using System.Collections;
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
    /// <summary>
    /// 装有<see cref="Chessboard"/>的<see cref="UserControl"/>.
    /// </summary>
    public partial class ChessboardContainer : UserControl
    {
        #region ' Constructors '
 
        public ChessboardContainer()
        {
            InitializeComponent();

            #region ' Set Control Styles '

            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor, true);

            #endregion

            #region ' Prepaint the Board '
            
            Bitmap bitmap = new Bitmap(this.Size.Width, this.Size.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // 高质量
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                // 高像素偏移质量
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                this.paint_board(graphics);
            }

            #endregion

            #region ' Initialize the Chessboard '

            this.SuspendLayout();

            this.grid_panels = new Chessboard(
                this.grid_side_length, this.grid_points)
            {
                Size = this.Size,
                Location = new Point(0, 0),
                BackgroundImage = global::platform.Properties.Resources.wood_grain,
                BackgroundImageLayout = ImageLayout.Tile,
                Image = bitmap
            };
            this.Controls.Add(this.grid_panels);
            this.grid_panels.BringToFront();
            
            this.ResumeLayout(true);

            #endregion
        }

        #endregion

        #region ' Properties '

        #region ' Children '

        /// <summary>
        /// 装有<see cref="GridPanel"/>们的控件
        /// </summary>
        private Chessboard grid_panels { get; set; }

        #endregion

        private Int32 grid_side_length
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
                xs[0] = (this.Width - this.grid_side_length * 8) / 2;
                for (Int32 i = 1; i < 9; i++)
                {
                    xs[i] = xs[i - 1] + this.grid_side_length;
                }
                return xs;
            }
        }

        private Int32[] _grid_ys
        {
            get
            {
                Int32[] ys = new Int32[10];
                ys[0] = (this.Height - this.grid_side_length * 9) / 2;
                for (Int32 i = 1; i < 10; i++)
                {
                    ys[i] = ys[i - 1] + this.grid_side_length;
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

        #endregion

        #region ' Methods '

        /// <summary>
        /// 反射棋盘.
        /// </summary>
        /// <param name="type">反射类型</param>
        public void reflect(ReflectionType type)
        {
            this.grid_panels.reflect(type);
        }

        #region ' Painting '

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

            #region ' Dispose Pens ' 

            slim_pen.Dispose();
            medium_pen.Dispose();
            fat_pen.Dispose();

            #endregion
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 背景+图片是棋盘, 棋盘上每个格点都是<see cref="GridPanel"/>.
    /// </summary>
    public class Chessboard : PictureBox, IDictionary<Coordinate, GridPanel>
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.Chessboard"/>类的新实例
        /// </summary>
        /// <param name="grid_side_length">格子的边长</param>
        /// <param name="grid_points">格点的坐标</param>
        public Chessboard(
            Int32 grid_side_length, Point[,] grid_points)
        {
            #region ' Set Control Styles '

            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor, true);

            #endregion

            this.grid_side_length = grid_side_length;
            this.grid_points = grid_points;

            #region ' Initialize GridPanels and the Position '

            for (Int32 x = 0; x < 9; x++)
                for (Int32 y = 0; y < 10; y++)
                {
                    Coordinate coordinate = new Coordinate(x, y);
                    GridPanel grid_panel = new GridPanel()
                    {
                        Size = new Size(
                            this.grid_side_length, this.grid_side_length),
                        Location = new Point(
                            this.grid_points[x, y].X - this.grid_side_length / 2,
                            this.grid_points[x, y].Y - this.grid_side_length / 2),
                        BackColor = Color.Transparent,
                        BackgroundImageLayout = ImageLayout.Center,
                        Tag = new GridPanelTag(x, y),
                    };
                    this.Controls.Add(grid_panel);
                    grid_panel.BringToFront();
                    this.Add(coordinate, grid_panel);
                }

            #endregion

            #region ' Initialize Pieces '
            
            this.chess_positions = new List<ChessPosition>();
            ChessPosition chess_position = new ChessPosition();
            this.chess_positions.Add(chess_position);
            this.refresh_pieces();

            #endregion
        }

        #endregion

        #region ' Properties '

        private List<ChessPosition> chess_positions { get; set; }
        private ChessPosition chess_position { get
            { return this.chess_positions[this.turns]; } }

        private Int32 grid_side_length { get; set; }
        private Point[,] grid_points { get; set; }

        #region ' Reflection '

        private ReflectionType _reflection { get; set; } = ReflectionType.None;
        public ReflectionType reflection
        {
            get
            {
                return this._reflection;
            }
            set
            {
                this._reflection = value;
                this.refresh_pieces();
            }
        }

        #endregion

        #region ' On Click '

        /// <summary>
        /// 上一个有效点击的子的原始坐标, 在成功move时清空.
        /// 成功click的时候, 若该值为空, 则清理所有mask.
        /// </summary>
        private Coordinate last_click { get; set; }
        /// <summary>
        /// 存储被mask的<see cref="GridPanel"/>的原始坐标.
        /// </summary>
        private List<Coordinate> masked_panels { get; } = new List<Coordinate>();

        #endregion

        /// <summary>
        /// 已经进行的半回合数.
        /// </summary>
        private Int32 turns { get; set; } = 0;
        /// <summary>
        /// 轮到走子的那一方
        /// </summary>
        private ChessColour current_player { get
            { return this.chess_position.current_player; } }

        #region ' Decorator '

        private IDictionary<Coordinate, GridPanel> dict { get; set; }
            = new Dictionary<Coordinate, GridPanel>();
        public GridPanel this[Coordinate key]
        {
            get
            {
                return this.dict[key];
            }
            set
            {
                this.dict[key] = this[key];
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the
        /// <see cref="ICollection{T}"/>.
        /// </summary>
        public Int32 Count
        {
            get
            {
                return this.dict.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the
        /// <see cref="ICollection{T}"/> is read-only.
        /// </summary>
        public Boolean IsReadOnly
        {
            get
            {
                return this.dict.IsReadOnly;
            }
        }

        /// <summary>
        /// Gets a <see cref="ICollection{T}"/> containing the keys of the
        /// <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        public ICollection<Coordinate> Keys
        {
            get
            {
                return this.dict.Keys;
            }
        }

        /// <summary>
        /// Gets a <see cref="ICollection{T}"/> containing the values in the
        /// <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        public ICollection<GridPanel> Values
        {
            get
            {
                return this.dict.Values;
            }
        }

        #endregion

        #endregion

        #region ' Methods '

        public void move(Coordinate start, Coordinate end)
        {
            this.chess_positions =
                this.chess_positions.Take(this.turns + 1).Append(
                    this.chess_positions[this.turns].move(start, end)).ToList();
            this.turns++;
            this.last_click = null;
            this.refresh_pieces();
        }

        /// <summary>
        /// 响应<see cref="GridPanel"/>的Click事件./>
        /// </summary>
        /// <param name="click">实际坐标</param>
        public void on_child_click(Coordinate click)
        {
            Coordinate abs_click = click.reflect(this.reflection);
            if (this.last_click == null)
            { 
                // 如果点击合法则清空mask, 并添加新的mask, 否则直接return.
                if (this[click].piece.colour != this.current_player)
                    return;
                foreach (Coordinate cdn in new List<Coordinate>(this.masked_panels))
                {
                    this.remove_mask(cdn);
                }
                this.set_mask(abs_click);
            }
            else
            {
                // 如果是自己的棋子则清除上一个mask, 并添加新的mask.
                if (this[click].piece.colour == this.current_player)
                {
                    this.remove_mask(last_click);
                    this.set_mask(abs_click);
                }
                // 如果是对方的棋子或空格, 则判断是否是move.
                // 是, 则move后return. 不是, 则直接return.
                else
                {
                    if(this.chess_position.is_move(this.last_click, abs_click))
                        this.move(this.last_click, abs_click);
                    return;
                }
            }
            this.last_click = click.reflect(this.reflection);
        }

        /// <summary>
        /// 反射棋盘.
        /// </summary>
        /// <param name="type">反射类型</param>
        public void reflect(ReflectionType type)
        {
            this._reflection = this._reflection ^ type;
            this.refresh_pieces();
        }

        #region ' Masks '

        /// <summary>
        /// 设置mask
        /// </summary>
        /// <param name="cdn">原始坐标</param>
        private void set_mask(Coordinate cdn)
        {
            this[cdn.reflect(this.reflection)].masked = true;
            this.masked_panels.Add(cdn);
        }

        /// <summary>
        /// 解除mask
        /// </summary>
        /// <param name="cdn">原始坐标</param>
        private void remove_mask(Coordinate cdn)
        {
            this[cdn.reflect(this.reflection)].masked = false;
            this.masked_panels.Remove(cdn);
        }

        #endregion

        #region ' Refreshing '

        /// <summary>
        /// 刷新棋子的图像
        /// </summary>
        private void refresh_pieces()
        {
            this.refresh_pieces(this.chess_position);
        }

        /// <summary>
        /// 刷新格点的Tag.
        /// </summary>
        /// <param name="chess_position">当前棋局</param>
        private void refresh_pieces(ChessPosition chess_position)
        {
            foreach (Coordinate coordinate in this.Keys)
            {
                Coordinate reflected_cdn = coordinate.reflect(this.reflection);
                (this[reflected_cdn].Tag as GridPanelTag).piece =
                    chess_position[coordinate];
                this[reflected_cdn].refresh_image();
            }
        }

        #endregion

        #region ' Decorator '

        /// <summary>
        /// Adds the specified key and value to the
        /// <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(Coordinate key, GridPanel value)
        {
            this.dict.Add(key, value);
        }

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="pair"></param>
        public void Add(KeyValuePair<Coordinate, GridPanel> item)
        {
            this.dict.Add(item);
        }

        /// <summary>
        /// Removes all items from
        /// <see cref="ICollection{T}"/>.
        /// </summary>
        public void Clear()
        {
            this.dict.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="ICollection{T}"/>
        /// contains a specific value.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public Boolean Contains(KeyValuePair<Coordinate, GridPanel> item)
        {
            return this.dict.Contains(item);
        }

        /// <summary>
        /// Determines whether the <see cref="IDictionary{TKey, TValue}"/>
        /// contains the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Boolean ContainsKey(Coordinate key)
        {
            return this.dict.ContainsKey(key);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection{T}"/>
        /// to an <see cref="Array"/>, starting at a particular
        /// <see cref="Array"/> index.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="array_index"></param>
        public void CopyTo(KeyValuePair<Coordinate, GridPanel>[] array, Int32 array_index)
        {
            this.dict.CopyTo(array, array_index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<Coordinate, GridPanel>> GetEnumerator()
        {
            return this.dict.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.dict.GetEnumerator();
        }

        /// <summary>
        /// Removes the value with the specified key from the
        /// <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Boolean Remove(Coordinate key)
        {
            return this.dict.Remove(key);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object
        /// from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Boolean Remove(KeyValuePair<Coordinate, GridPanel> item)
        {
            return this.dict.Remove(item);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Boolean TryGetValue(Coordinate key, out GridPanel value)
        {
            return this.dict.TryGetValue(key, out value);
        }

        #endregion

        #endregion
    }
}
