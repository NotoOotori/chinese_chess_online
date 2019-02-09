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


namespace platform.login
{
    public partial class FormLogin : Form
    {
        String HOST = "45.32.82.133"; // IP地址
        Int32 PORT = 21567; // 端口
        Int32 BUFSIZ = 1024; // 缓冲区大小
        Socket socket_client = null;
        Thread thread_client = null; // 线程
        static string receive_string;
        static int cin_count = 0;
        static int click_count = 0;

        public FormLogin()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 接收服务端发来信息
        /// </summary>
        private void receive_data()
        {
            while (true) //持续监听服务端发来的消息
            {
                //定义一个内存缓冲区 用于临时性存储接收到的信息
                byte[] arr_data = new byte[BUFSIZ];
                //将客户端套接字接收到的数据存入内存缓冲区, 并获取其长度
                int length = socket_client.Receive(arr_data);
                //将套接字获取到的字节数组转换为人可以看懂的字符串
                string str_data = Encoding.UTF8.GetString(arr_data, 0, length);
                receive_string=str_data;
            }
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

        /// <summary>
        /// 判断receive的字符串状态,1代表成功，0代表失败
        /// </summary>
        private int receive_string_check(string s)
        {
            if (true)
                return 1;
            else
                return 0;
        }

        public GlossyButton glossyButton3 = new GlossyButton();

        private void FormLogin_Load(object sender, EventArgs e)
        {
            label5.Text = "";
            label6.Text = "";
            cin_count = 0;         
            glossyButton3.Location = new Point(80, 120);
            glossyButton3.BtnText = "登录";
            glossyButton3.Font = new Font("Microsoft Sans Serif", 15);
            glossyButton3.Size = new Size(207, 40);
            glossyButton3.Click += new EventHandler(glossyButton3_Click);
            glossyButton3.MouseEnter += new EventHandler(glossyButton3_MouseEnter);
            glossyButton3.MouseLeave += new EventHandler(glossyButton3_MouseLeave);
            this.Controls.Add(glossyButton3);
            textBox2.PasswordChar = '*'; //设置文本框的PasswordChar属性为字符*                       
        }

        private void glossyButton3_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;           
        }

        private void glossyButton3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void glossyButton3_Click(object sender, EventArgs e)
        {

            label5.Text = "";
            label6.Text = "";
            if (textBox1.Text == "")
            {
                label5.Text = "请输入用户名";
                return;
            }

            if (textBox2.Text == "")
            {
                label6.Text = "请输入密码";
                return;
            }
            //连接服务器
            //定义一个套字节监听  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IP地址
            IPAddress ip_address = IPAddress.Parse(HOST);
            //将ip地址和端口号绑定到网络节点end_point上
            IPEndPoint end_point = new IPEndPoint(ip_address, PORT);
            //这里客户端套接字连接到网络节点(服务端)用的方法是Connect 而不是Bind
            socket_client.Connect(end_point);
            //创建一个线程 用于监听服务端发来的消息
            thread_client = new Thread(receive_data);
            //将窗体线程设置为与后台同步
            thread_client.IsBackground = true;
            //启动线程
            thread_client.Start();
            //判断是否输入用户名密码
            if (textBox1.Text == "")
            {
                label5.Text = "请输入用户名";
                return;
            }

            if (textBox2.Text == "")
            {
                label6.Text = "请输入密码";
                return;
            }

            //发送用户名
            send_data(textBox1.Text.Trim());
            //发送密码
            send_data(textBox2.Text.Trim());
            //接受信息
            receive_data();
            int login_result = receive_string_check(receive_string);
            //login_result = 0;
            if (login_result == 0)
            {
                label6.Text = "输入的用户名或密码有误";
                cin_count++;
                //cin_count = 3;
                if (cin_count >= 3)
                {
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
                    glossyButton3.Location = new Point(160,290);
                    checkBox1.Location = new Point(220, 270);
                }
                return;
            }              
            else
            {
                //登陆成功，链接主程序
            }
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
