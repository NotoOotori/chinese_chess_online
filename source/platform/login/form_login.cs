using System;
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


namespace platform.login
{
    public partial class FormLogin : Form
    {
        String HOST = "45.32.82.133"; // IP地址
        Int32 PORT = 21567; // 端口
        Int32 BUFSIZ = 1024; // 缓冲区大小
        Socket socket_client = null;
        static int cin_count = 0;
        static int click_count = 0;
        static int captcha_state = 0;

        public FormLogin()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
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
        
        private void link_server()
        {
            //连接服务器
            //定义一个套字节监听  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IP地址
            IPAddress ip_address = IPAddress.Parse(HOST);
            //将ip地址和端口号绑定到网络节点end_point上
            IPEndPoint end_point = new IPEndPoint(ip_address, PORT);
            //这里客户端套接字连接到网络节点(服务端)用的方法是Connect 而不是Bind
            socket_client.Connect(end_point);
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

        private void prod_captcha()
        {
            captcha_state = 1;
            Label label4 = new Label();
            label4.Font = new Font("宋体", 15);
            label4.Location = new Point(109, 236);
            label4.Size = new Size(69, 20);
            label4.Text = "验证码";
            this.Controls.Add(label4);

            TextBox textBox3 = new TextBox();
            textBox3.Font = new Font("宋体", 15);
            textBox3.Location = new Point(184, 233);
            textBox3.Size = new Size(77, 30);
            this.Controls.Add(textBox3);

            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Location = new Point(306, 233);
            pictureBox1.Size = new Size(150, 30);
            //加入图片
            this.Controls.Add(pictureBox1);
            glossyButton3.Location = new Point(160, 290);
            checkBox1.Location = new Point(220, 270);
        }

        //检查验证码，0正确，1错误
        private int check_captcha()
        {
            if (true)
                return 0;
            //else
            //    return 1;
        }

        private void reaction(int state, Dictionary<string, string> di)
        {
            switch (state)
            {
                case 0:
                    MessageBox.Show("登录成功");
                    //登录成功
                    break;
                case 1:
                    label5.Text = "邮箱不存在";
                    break;
                case 2:
                    label6.Text = "邮箱或密码错误";
                    break;
                case 3:
                    prod_captcha();
                    break;
                default:
                    MessageBox.Show(di["response"]);
                    break;
            }
        }

        private GlossyButton glossyButton3 = new GlossyButton();
        private void FormLogin_Load(object sender, EventArgs e)
        {
            label5.Text = "";
            label6.Text = "";
            click_count = 0;
            cin_count = 0;         
            glossyButton3.Location = new Point(160, 240);
            glossyButton3.Text = "登录";
            glossyButton3.Font = new Font("Microsoft Sans Serif", 15);
            glossyButton3.Size = new Size(207, 40);
            glossyButton3.Click += new EventHandler(glossyButton3_Click);
            this.Controls.Add(glossyButton3);
            textBox2.PasswordChar = '*'; //设置文本框的PasswordChar属性为字符*                       
        }

        private void glossyButton3_Click(object sender, EventArgs e)
        {
            //判断是否输入用户名密码
            foreach (Control con in this.Controls)
            {
                if (con is TextBox)
                {
                    TextBox t = (TextBox)con as TextBox;
                    if (t.Text == "")
                    {
                       label5.Text = "用户名或密码为空";
                       return;
                    }
                }
            }
            label5.Text = "";
            label6.Text = "";
            for(int i=0;i<textBox1.Text.Length;i++)
                if(textBox1.Text.Substring(i,1)=="<"|| textBox1.Text.Substring(i, 1)==">"|| textBox1.Text.Substring(i, 1)=="/")
                {
                    MessageBox.Show("用户名中包含非法字符<>/");
                    return;
                }
            for (int i = 0; i < textBox2.Text.Length; i++)
                if (textBox2.Text.Substring(i, 1) == "<" || textBox2.Text.Substring(i, 1) == ">" || textBox2.Text.Substring(i, 1) == "/")
                {
                    MessageBox.Show("密码中包含非法字符<>/");
                    return;
                }
            //若产生验证码，进行检验
            if (captcha_state==1)
            {
                int captcha_result = check_captcha();
                if (captcha_result == 1)
                {
                    MessageBox.Show("验证码输入错误,请重新输入");
                    return;
                }
            }        
            link_server();//连接服务器
            Dictionary<String, String> dict = new Dictionary<String, String>();
            dict.Add("identifier", "login");
            dict.Add("email", textBox1.Text);
            dict.Add("password",textBox2.Text);            
            string send_string=DataEncoding.get_string(dict);
            //发送密码,用户名
            send_data(send_string);
            //接受信息
            Dictionary<String, String> dict_back = receive_data();
            int login_result = receive_dict_check(dict_back);
            if (cin_count == 3)
            {
                prod_captcha();
            }
            reaction(login_result, dict_back);            
        }

        private void label8_Click(object sender, EventArgs e)
        {
            form_signup f1 = new form_signup();
            this.Hide();
            f1.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            form_forgetpassword f1 = new form_forgetpassword();
            this.Hide();
            f1.Show();
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

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox2, "请勿输入<,>,/");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            click_count++;
            if (click_count % 2 != 0)
                textBox2.PasswordChar = new char();
            else
                textBox2.PasswordChar = '*';
        }
    }
}
