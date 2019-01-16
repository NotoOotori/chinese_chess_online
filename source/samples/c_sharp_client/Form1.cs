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

namespace c_sharp_client
{
    public partial class Form1 : Form
    {
        String HOST = "45.32.82.133"; // IP地址
        Int32 PORT = 21567; // 端口
        Int32 BUFSIZ = 1024; // 缓冲区大小
        Socket socket_client = null;
        Thread thread_client = null; // 线程

        public Form1()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 接收服务端发来信息的方法
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
                //将发送的信息追加到聊天内容文本框中
                label_response.Text = str_data;
            }
        }

        /// <summary>
        /// 向服务器发送信息的方法
        /// </summary>
        /// <param name="str_data">发送的字符串信息</param>
        private void send_data(string str_data)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组
            byte[] arr_data = Encoding.UTF8.GetBytes(str_data);
            //调用客户端套接字发送字节数组
            socket_client.Send(arr_data);
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
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

        private void button_send_Click(object sender, EventArgs e)
        {
            send_data(textBox_send.Text.Trim());
        }

        private void textBox_send_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //则调用客户端向服务端发送信息的方法
                send_data(textBox_send.Text.Trim());
            }
        }
    }
}
