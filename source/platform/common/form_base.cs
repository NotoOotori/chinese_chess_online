using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platform.common
{
    public partial class FormBase : Form
    {
        Point mouseoff;
        bool leftflag;

        public FormBase()
        {
            TextBox.CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();

            picture_box_icon.Click += picture_box_icon_click;
            button_min.Click += button_min_Click;
            button_exit.Click += button_exit_Click;
            MouseUp += form_mouse_up;
            MouseDown += form_mouse_down;
            MouseMove += form_mouse_move;

            Icon = Properties.Resources.chess_icon;

            ToolTip tool_tip = new ToolTip();
            tool_tip.AutoPopDelay = 2500;
            tool_tip.InitialDelay = 500;
            tool_tip.ReshowDelay = 250;
            tool_tip.ShowAlways = true;
            tool_tip.SetToolTip(this.picture_box_icon, "Visit our repository.");
        }

        public new String Text
        {
            get { return label_title.Text; }
            set { label_title.Text = value; }
        }

        public String text_exit { get; set; } = "请问您确定要关闭窗口吗?";

        #region ' Minimize and Exit '

        private void button_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            if (text_exit == "")
            {
                this.Close();
                return;
            }
            if (MessageBoxBase.Show(
                text_exit,
                "关闭窗口",
                MessageBoxButtons.OKCancel) == DialogResult.OK
            )
            {
                this.Close();
                return;
            }
        }

        private void picture_box_icon_click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/NotoOotori/chinese_chess_online");
        }

        #endregion

        #region ' 窗口拖动 '

        private void form_mouse_down(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseoff = new Point(-e.X, -e.Y);
                leftflag = true;
            }
        }

        private void form_mouse_move(object sender, MouseEventArgs e)
        {
            if (leftflag)
            {
                Point mouseset = Control.MousePosition;
                mouseset.Offset(mouseoff.X, mouseoff.Y);
                Location = mouseset;
            }
        }

        private void form_mouse_up(object sender, MouseEventArgs e)
        {
            if (leftflag)
                leftflag = false;
        }
        #endregion

        private void FormBase_Load(object sender, EventArgs e)
        {
            button_min.Location = new Point(ClientSize.Width - 96, 0);
            button_exit.Location = new Point(ClientSize.Width - 45, 0);
        }
    }
}
