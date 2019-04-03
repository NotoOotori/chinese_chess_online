using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platform.common
{
    public partial class MessageFormBase : FormBase
    {
        public MessageFormBase()
        {
            InitializeComponent();
        }

        private Size default_size = new Size(100, 50);
        private Font default_button_font = new Font("Comic Sans MS", 14F);
        private Font default_text_font = new Font("KaiTi", 14F);

        public MessageFormBase(String text, String caption,
            MessageBoxButtons buttons)
        {
            InitializeComponent();
            this.add_buttons(buttons);
            this.label_text.Text = text;
            this.label_text.Font = default_text_font;
            this.label_text.SendToBack();
            this.Text = caption;
            this.text_exit = "";
        }

        private void add_buttons(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                default:
                    throw new ArgumentOutOfRangeException("不支持该buttons!");
                case MessageBoxButtons.OK:
                    add_button_ok(new Point(150, 225));
                    break;
                case MessageBoxButtons.OKCancel:
                    add_button_ok(new Point(75, 225));
                    add_button_cancel(new Point(225, 225));
                    break;
            }
        }

        private void add_button_ok(Point location)
        {
            GlossyButton button_ok = new GlossyButton()
            {
                Font = default_button_font,
                Text = "OK",
                Size = default_size,
                Location = location,
            };
            button_ok.Click += button_ok_click;
            button_ok.label1.Click += button_ok_click;
            this.Controls.Add(button_ok);
        }

        private void add_button_cancel(Point location)
        {
            GlossyButton button_cancel = new GlossyButton()
            {
                Font = default_button_font,
                Text = "Cancel",
                Size = default_size,
                Location = location,
            };
            button_cancel.Click += button_cancel_click;
            button_cancel.label1.Click += button_cancel_click;
            this.Controls.Add(button_cancel);
        }

        private void button_ok_click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_cancel_click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    public static class MessageBoxBase
    {
        public static DialogResult Show(String text, String caption = "",
            MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            using (var form = new MessageFormBase(text, caption, buttons))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                return form.ShowDialog();
            }
        }
    }
}
