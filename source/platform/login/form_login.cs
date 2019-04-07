﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using platform.common;
using System.IO;

namespace platform.login
{
    public partial class FormLogin : FormBase
    {
        String HOST = "45.32.82.133"; // IP地址
        Int32 PORT = 21567; // 端口
        Int32 BUFSIZ = 1024; // 缓冲区大小
        Socket socket_client = null;
        static int cin_count = 0;
        static int click_count = 0;

        public FormLogin()
        {
            InitializeComponent();
            Text = "";

            button_login.Location = new Point(105, 265);
            button_login.Text = "登录";
            button_login.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold);
            button_login.Size = new Size(207, 40);
            button_login.Click += new EventHandler(button_login_click);
            button_login.label1.Click += button_login_click;
            this.Controls.Add(button_login);

            ToolTip tool_tip = new ToolTip();
            tool_tip.AutoPopDelay = 2500;
            tool_tip.InitialDelay = 125;
            tool_tip.ReshowDelay = 125;
            tool_tip.ShowAlways = true;
            tool_tip.SetToolTip(textBox2, "请勿输入<,>,/");

            pictureBox_scroll.SendToBack();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
            base.OnFormClosing(e);
        }

        /// <summary>
        /// 接收服务端发来信息
        /// </summary>
        private Dictionary<String, String> receive_data()
        {
            //定义一个内存缓冲区 用于临时性存储接收到的信息
            byte[] arr_data = new byte[BUFSIZ];
            //将客户端套接字接收到的数据存入内存缓冲区, 并获取其长度
            socket_client.Receive(arr_data);
            return DataEncoding.get_dictionary(arr_data);
        }

        /// <summary>
        /// 向服务器发送信息
        /// </summary>
        /// <param name="str_data">发送的字符串信息</param>
        private void send_data(string str_data)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组
            byte[] arr_data = Encoding.UTF8.GetBytes(str_data);
            //调用客户端套接字发送字节数组
            socket_client.Send(arr_data);
        }
        
        private Boolean link_server()
        {
            //连接服务器
            //定义一个套字节监听  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IP地址
            IPAddress ip_address = IPAddress.Parse(HOST);
            //将ip地址和端口号绑定到网络节点end_point上
            IPEndPoint end_point = new IPEndPoint(ip_address, PORT);
            //这里客户端套接字连接到网络节点(服务端)用的方法是Connect 而不是Bind
            try
            {
                socket_client.Connect(end_point);
            }
            catch(SocketException)
            {
                MessageBoxBase.Show("与服务器连接出现问题，请稍后再试！");
                this.Cursor = Cursors.Arrow;
                return false;
            }
            return true;
        }


        // 判断receive的字符串状态,0登录成功,1邮箱不存在,2邮箱或密码错误,3非常用登录ip请输入验证码,-1服务器出现错误
        private int receive_dict_check(Dictionary<string, string> dict)
        {
            string state = dict["identifier"];
            int num;
            if (state == "error")
                return -1;
            else
            {
                num = int.Parse(dict["response"]);
                if (num == 1 || num == 2)
                    cin_count++;
                return num;
            }
        }

        //private void prod_captcha()
        //{
        //    //captcha_state = 1;
            
        //    richTextBox1.Text = "请输入验证码！";
        //    richTextBox1.Visible = true;           
        //    button_login.Location = new Point(160, 290);            
        //}

        //检查验证码，0正确，1错误
        //private int check_captcha(string str)
        //{
        //    if (richTextBox1.Text == str)
        //        return 0;
        //    else
        //        return 1;
        //}

        private void reaction(int state, Dictionary<string, string> di,string email)
        {
            switch (state)
            {
                case 0:
                    {
                        platform.dating.FormDating f1 = new dating.FormDating(socket_client,email);
                        f1.Show(); 
                        this.Hide();
                    }
                    break;
                case 1:
                    label5.Text = "邮箱不存在";
                    cin_count++;
                    break;
                case 2:
                    label6.Text = "邮箱或密码错误";
                    cin_count++;
                    break;
                case 3:
                    //prod_captcha();
                    break;
                case 4:
                    MessageBoxBase.Show("该用户已经登录!");
                    break;
                default:
                    //MessageBoxBase.Show(di["response"]);
                    break;
            }
        }

        private GlossyButton button_login = new GlossyButton();
        private void FormLogin_Load(object sender, EventArgs e)
        {
            label5.Text = "";
            label6.Text = "";
            click_count = 0;
            cin_count = 0;
            textBox2.PasswordChar = '*'; //设置文本框的PasswordChar属性为字符*
        }

        private void button_login_click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            //判断是否输入用户名密码
            if(textBox1.Text == "admin"&&textBox2.Text == "123456")
            {
                Admin admin = new Admin();
                admin.Show();
                this.Hide();
                return;
            }       
            foreach (Control con in this.Controls)
            {
                if (con is TextBox)
                {
                    TextBox t = (TextBox)con as TextBox;
                    if (t.Text == "")
                    {
                       label5.Text = "用户名或密码为空";
                       cin_count++;
                       return;
                    }
                }
            }
            label5.Text = "";
            label6.Text = "";
            //MessageBox.Show(cin_count.ToString());
            for(int i=0;i<textBox1.Text.Length;i++)
                if(textBox1.Text.Substring(i,1)=="<"|| textBox1.Text.Substring(i, 1)==">"|| textBox1.Text.Substring(i, 1)=="/")
                {
                    MessageBoxBase.Show("用户名中包含非法字符<>/");
                    return;
                }
            for (int i = 0; i < textBox2.Text.Length; i++)
                if (textBox2.Text.Substring(i, 1) == "<" || textBox2.Text.Substring(i, 1) == ">" || textBox2.Text.Substring(i, 1) == "/")
                {
                    MessageBoxBase.Show("密码中包含非法字符<>/");
                    return;
                }
            //产生验证码，进行检验
            //if (cin_count >= 3)
            //{
            //    ValidateCode vc = new ValidateCode();
            //    string s = vc.CreateValidateCode(4);
            //    MemoryStream stream = new MemoryStream();
            //    stream = vc.CreateValidateGraphic(s);
            //    pictureBox1.Image = Image.FromStream(stream);
            //    prod_captcha();
            //    int captcha_result = check_captcha(s);
            //    if (captcha_result == 1)
            //    {
            //        MessageBoxBase.Show("验证码输入错误,请重新输入");
            //        return;
            //    }                
            //}
            //连接服务器
            if (!link_server())
                return;
            Dictionary<String, String> dict = new Dictionary<String, String>();
            dict.Add("identifier", "login");
            dict.Add("email", textBox1.Text);
            dict.Add("password",textBox2.Text);            
            string send_string=DataEncoding.get_string(dict);
            //发送密码,用户名
            send_data(send_string);
            //接受信息
            try
            {
                Dictionary<String, String> dict_back = receive_data();
                int login_result = receive_dict_check(dict_back);
                reaction(login_result, dict_back, textBox1.Text);
            }
            catch (SocketException ex)
            { MessageBoxBase.Show(ex.Message); }
            Cursor = Cursors.Arrow;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            FormSignup form_signup = new FormSignup(this);
            this.Hide();
            form_signup.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            MessageBoxBase.Show("请联系管理员！");
            // form_forgetpassword f1 = new form_forgetpassword();
            // this.Hide();
            // f1.Show();
        }

        private void label7_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void label8_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            click_count++;
            if (click_count % 2 != 0)
                textBox2.PasswordChar = new char();
            else
                textBox2.PasswordChar = '*';
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_login_click(button_login, new EventArgs());
            }
        }
    }
}
