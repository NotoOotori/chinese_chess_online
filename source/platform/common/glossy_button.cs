using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Drawing.Text;

namespace platform
{
    public partial class GlossyButton : UserControl
    {
        public GlossyButton()
        {
            InitializeComponent();
            
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            //this.label1.MouseDown += new MouseEventHandler(mymousedown);
            NormalStyle();
            setFont();
            label1.MouseEnter += onMouseEnter;
            label1.MouseLeave += onMouseLeave;
            label1.MouseDown += onMouseDown;
            label1.MouseUp += onMouseUp;
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int LeftRect, int topRect, int rightRect, int bottomRect, int wEllipse, int hEllipse);
        Pen p = new Pen(Color.Red);
        [Description("The text assoicated with the control")]
        [Category("Appearance")]
        public new string Text
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
        public new Font Font
        {
            get
            { return label1.Font; }
            set
            {
                label1.Font = value;
                label1.Location = new Point(this.Width / 2 - label1.Width / 2, this.Height / 2 - label1.Height / 2);
            }
        }
        public void setFont()
        {
            string AppPath = Application.StartupPath;
            try
            {
                PrivateFontCollection font = new PrivateFontCollection();
                font.AddFontFile(AppPath + @"\HYS1GFM.TTF");
                Font myfont = new Font(font.Families[0].Name, 9f, FontStyle.Regular, GraphicsUnit.Millimeter);
                this.label1.Font = myfont;
            }
            catch(FileNotFoundException) { ; }
        }

        public Color EnterColor { get; set; } = Color.Pink;
        public Color DownColor { get; set; } = Color.Blue;
        public Color NormalColor { get; set; } = Color.Orange;
        private void onMouseEnter(object sender, EventArgs e)
        {
            p.Color = EnterColor;
            this.Cursor = Cursors.Hand;
            this.BackColor = EnterColor; //this.DoubleBuffered = true; this.label1.Refresh(); this.Refresh();
        }
        private void onMouseLeave(object sender, EventArgs e)
        {
            NormalStyle();
        }
        private void onMouseDown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            p.Color = DownColor;
            this.BackColor = DownColor ; //this.DoubleBuffered = true; this.label1.Refresh(); this.Refresh();
        }
        private void onMouseUp(object sender, EventArgs e)
        {
            NormalStyle();
        }
        private void NormalStyle()
        {
            this.Cursor = Cursors.Arrow;
            p.Color = NormalColor; this.BackColor = NormalColor; //this.DoubleBuffered = true; this.label1.Refresh(); this.Refresh();
        }
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 3, 3));
        //    LinearGradientBrush lb = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), Color.FromArgb(150, Color.White), Color.FromArgb(50, Color.White), LinearGradientMode.Vertical);
        //    //MessageBoxBase.Show(p.Color.ToString());
        //    e.Graphics.FillRectangle(lb, 2, 2, this.Width - 6, this.Height / 2);
        //    e.Graphics.DrawRectangle(p, 0, 0, this.Width - 3, this.Height - 3);
        //}
        GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 3, 3));
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);

            GraphPath.CloseFigure();
            return GraphPath;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            base.OnPaint(e);
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            GraphicsPath GraphPath = GetRoundPath(Rect, 30);

            this.Region = new Region(GraphPath);
            using (Pen pen = new Pen(Color.Transparent,0))
            {
                pen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen, GraphPath);
                LinearGradientBrush lb = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), Color.FromArgb(150, Color.White), Color.FromArgb(50, Color.White), LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(lb, 2, 2, this.Width - 6, this.Height / 2);
                
            }
        }
        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, this.Height / 2 - label1.Height / 2);

        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            onMouseEnter(this, e);
        }

        
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            onMouseLeave(this, e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            onMouseDown(this, e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            onMouseUp(this, e);
        }

    }
}
