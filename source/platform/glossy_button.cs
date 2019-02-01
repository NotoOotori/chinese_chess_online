using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Configuration;

namespace platform
{
    public partial class GlossyButton : UserControl
    {
        public GlossyButton()
        {
            InitializeComponent();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int LeftRect, int topRect, int rightRect, int bottomRect, int wEllipse, int hEllipse);
        Pen p = new Pen(Color.Coral);
        [Description("The text assoicated with the control")]
        [Category("Appearance")]
        public string BtnText
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
                label1.Location = new Point(this.Width / 2 - label1.Width / 2, this.Height / 2 - label1.Height / 2);
            }
        }
        public Font BtnFont
        {
            get
            { return label1.Font; }
            set
            {
                label1.Font = value;
                label1.Location = new Point(this.Width / 2 - label1.Width / 2, this.Height / 2 - label1.Height / 2);
            }
        }
        protected void onMouseEnter()
        {
            p.Color = Color.Red;
            this.BackColor = Color.Firebrick; this.Invalidate();
        }
        protected void onMouseDown()
        {
            this.BackColor = Color.Maroon; this.Invalidate();
        }
        protected void NormalStyle()
        {
            p.Color = Color.Coral; this.BackColor = Color.Aqua;
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 3, 3));
            LinearGradientBrush lb = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), Color.FromArgb(150, Color.White), Color.FromArgb(50, Color.White), LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lb, 2, 2, this.Width - 6, this.Height / 2);
            e.Graphics.DrawRectangle(p, 0, 0, this.Width - 3, this.Height - 3);

        }
        //GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        //{
        //    float r2 = radius / 2f;
        //    int a = 0;
        //    GraphicsPath GraphPath = new GraphicsPath();
        //    this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 3, 3));
        //    GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
        //    GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
        //    GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
        //    GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
        //    GraphPath.AddArc(Rect.X + Rect.Width - radius,
        //                     Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
        //    GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
        //    GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
        //    GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);

        //    GraphPath.CloseFigure();
        //    return GraphPath;
        //}V
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    Graphics graphics = e.Graphics;
        //    graphics.SmoothingMode = SmoothingMode.HighQuality;

        //    base.OnPaint(e);
        //    RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
        //    GraphicsPath GraphPath = GetRoundPath(Rect, 30);

        //    this.Region = new Region(GraphPath);
        //    using (Pen pen = new Pen(Color.CadetBlue, 1.75f))
        //    {
        //        pen.Alignment = PenAlignment.Inset;
        //        e.Graphics.DrawPath(pen, GraphPath);
        //    }
        //}

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, this.Height / 2 - label1.Height / 2);

        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            onMouseEnter();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            NormalStyle();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            onMouseDown();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            onMouseEnter();
        }

    }
}
