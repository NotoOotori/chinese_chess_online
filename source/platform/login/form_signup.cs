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
    public partial class form_signup : Form
    {
        public form_signup()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        String HOST = "45.32.82.133"; // IP地址
        Int32 PORT = 21567; // 端口
        Int32 BUFSIZ = 1024; // 缓冲区大小
        Socket socket_client = null;
        Thread thread_client = null; // 线程
        static string receive_string;

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
                receive_string = str_data;
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

        

        private void form_signup_Load(object sender, EventArgs e)
        {
            label12.Text = "";
            label10.Text = "";
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

        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox1, "请勿输入<,>,/");
        }
    }
}
