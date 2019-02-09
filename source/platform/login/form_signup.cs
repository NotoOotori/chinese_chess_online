using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platform.login
{
    public partial class form_signup : Form
    {
        public form_signup()
        {
            InitializeComponent();
        }

        private void form_signup_Load(object sender, EventArgs e)
        {
            label12.Text = "";
            textBox3.PasswordChar = '*';
            textBox4.PasswordChar = '*';
            label10.Text = "用户名不能包含<,>,/";
            label10.ForeColor = Color.Black;
            int i;
            for (i = 1950; i <= 2015; i++)
                comboBox2.Items.Add(i.ToString());
            for (i = 1; i < 13; i++)
                comboBox3.Items.Add(i.ToString());
            for (i = 1; i < 32; i++)
                comboBox4.Items.Add(i.ToString());
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            string user_name=textBox1.Text;            
            for(int i=0;i<user_name.Length;i++)
            {
                if(user_name.Substring(i,1)=="<"|| user_name.Substring(i, 1) == ">"|| user_name.Substring(i, 1) == "/")
                {
                    label10.ForeColor = Color.Red;
                    label10.Text = "用户名包含<>/,请重新输入";
                    return;
                }
            }
            if (textBox3.Text != textBox4.Text)
            {
                label12.Text = "密码输入错误，请重新输入";
                return;
            }                
            //连接服务器，录入数据库

        }
    }
}
