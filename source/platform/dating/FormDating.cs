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
using MySql.Data.MySqlClient;
using System.IO;
using platform.common;

namespace platform.dating
{
    public partial class FormDating : Form
    {
        int tot_board = 10;
        uint num = 0, seat = 1;//记录进入的桌号和椅子
        Thread thread_client;
        String connection_string = "server = 45.32.82.133; user = ccol_user; database = chinese_chess_online; port = 3306; password = 123PengZiYu@";
        String HOST = "45.32.82.133"; // IP地址
        Int32 PORT = 21567; // 端口
        Int32 BUFSIZ = 1024; // 缓冲区大小
        //Socket socket_client = null;
        Socket socket_server = null;
        // Modify the constructor
        List<ZBW> ZBWs = new List<ZBW>();
        public FormDating(Socket server_socket)
        {
            InitializeComponent();
            socket_server = server_socket;
        }

        void server_send_renew(Socket socket_server)
        {
            byte[] renew;
            renew = DataEncoding.get_bytes(new Dictionary<string, string>()
            {
                ["identifier"] = "plaza_renew"
            });
            socket_server.Send(renew);
        }

        delegate void renew_controls();

        public void red_onclick(object sender, EventArgs arg)
        {
            num = Convert.ToUInt32((sender as PictureBox).Tag);
            server_send(socket_server, Convert.ToUInt32((sender as PictureBox).Tag), 1);
            seat = 1;
        }

        void black_onclick(object sender, EventArgs arg)
        {
            num = Convert.ToUInt32((sender as PictureBox).Tag);
            server_send(socket_server, Convert.ToUInt32((sender as PictureBox).Tag), 2);
            seat = 2;
        }

        void server_send(Socket socket_server, uint num, uint seat)
        {
            byte[] enter;
            enter = DataEncoding.get_bytes(new Dictionary<string, string>()
            { ["identifier"] = "lobby_enter", ["lobby_id"] = (num+1).ToString(), ["seat"] = seat.ToString() });
            socket_server.Send(enter);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            socket_server.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //platform.GlossyButton glossy = new platform.GlossyButton();
            //glossy.EnterCOlor = Color.Green;
            //this.Controls.Add(glossy);
            //try { this.pictureBox1.Image = Image.FromFile("D:\\greenmushroom.gif"); pictureBox1.Enabled = false; }
            //catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            {                
                thread_client = new Thread(receive_data);
                //将窗体线程设置为与后台同步
                thread_client.IsBackground = true;
                //启动线程
                thread_client.Start();
            }

            for (uint i = 0; i < 10; i++)
            {
                ZBW myzbw = new ZBW(this, panel1,Convert.ToInt32(i));                
                myzbw.blackimage.Click += new EventHandler(black_onclick);
                myzbw.redimage.Click += new EventHandler(red_onclick);
                ZBWs.Add(myzbw);
            }//显示桌子椅子 onclick
        }

        private void button_renew_Click(object sender, EventArgs e)
        {
            server_send_renew(socket_server);
        }

        delegate void num_socket_arg_returning_void(uint num, uint seat, Socket socket_server);

        void show_form_lobby(UInt32 num, uint seat, Socket socket_server)
        {
            new chess_lobby.ChessLobby(num, seat, socket_server).Show();
        }

        public void receive_data()
        {
            while (true)
            {
                byte[] arr_data = new byte[BUFSIZ];
                int length = socket_server.Receive(arr_data);
                Dictionary<String, String> mydic;
                try
                {
                    mydic = DataEncoding.get_dictionary(arr_data);

                    String identifier = mydic["identifier"];
                    //MessageBox.Show(DataEncoding.get_string(mydic));
                    if (identifier == "plaza_renew")
                    {
                        for (int i = 0; i < 10; i++)
                            for (int seat = 1; seat < 3; seat++)
                            {
                                String key = $"{i + 1}-{seat}";
                                string person = mydic[key];
                                MessageBox.Show(person);
                                if (person == "0")
                                {
                                    //没有人 do nothing
                                }
                                else
                                {
                                    //有人
                                    void renew()
                                    {
                                        MySqlParameter e_address = new MySqlParameter("_email_address", MySqlDbType.String);
                                        MySqlParameter pic = new MySqlParameter("_avatar", MySqlDbType.MediumBlob);
                                        MySqlParameter name = new MySqlParameter("_username", MySqlDbType.String);
                                        e_address.Value = person;
                                        e_address.Direction = ParameterDirection.Input;
                                        pic.Direction = ParameterDirection.Output;
                                        name.Direction = ParameterDirection.Output;

                                        using (MySqlConnection connection = new MySqlConnection(connection_string))
                                        {
                                            try
                                            {
                                                connection.Open();
                                                MySqlCommand cmd = new MySqlCommand("procedure_get_avatar", connection);
                                                cmd.Parameters.Add(e_address);
                                                cmd.Parameters.Add(pic);
                                                cmd.CommandType = CommandType.StoredProcedure;

                                                { cmd.ExecuteNonQuery(); }

                                                MySqlCommand cmd1 = new MySqlCommand("procedure_get_username", connection);
                                                cmd1.Parameters.Add(e_address);
                                                cmd1.Parameters.Add(name);
                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                //try
                                                { cmd1.ExecuteNonQuery(); }
                                            }
                                            catch(MySqlException ex)
                                            {
                                                MessageBox.Show("与服务器连接失败，请检查连接！");
                                            }
                                        };

                                        // 这是文档中推荐的使用connection的方式, 在这段代码结束之后自动关闭connection, 无须程序猿来关闭.

                                        MemoryStream myPic = null;
                                        byte[] mydata;
                                        if (Convert.IsDBNull(pic.Value))
                                        {
                                            //MessageBox.Show("NULLPIC");
                                            //try
                                            {
                                                if (seat == 1)
                                                    ZBWs[i].redimage.BackgroundImage = global::platform.Properties.Resources.default_avatar;
                                                else
                                                    ZBWs[i].blackimage.BackgroundImage = global::platform.Properties.Resources.default_avatar;
                                            }
                                            //catch { }
                                        }
                                        else
                                        {
                                            //MessageBox.Show("NOTNULLPIC");
                                            //try
                                            {
                                                mydata = (byte[])pic.Value;
                                                myPic = new MemoryStream(mydata);
                                                if (seat == 1)
                                                    ZBWs[i].redimage.BackgroundImage = Image.FromStream(myPic);
                                                else
                                                    ZBWs[i].blackimage.BackgroundImage = Image.FromStream(myPic);
                                            }
                                            //catch { }
                                        }
                                        //头像

                                        if (seat == 1) //try
                                        {
                                            string s = name.Value.ToString();
                                            MessageBox.Show(s);
                                            ZBWs[i].redplayer.Text = name.Value.ToString();
                                        }
                                        //catch { }
                                        else try { ZBWs[i].blackplayer.Text = name.Value.ToString(); } catch { }
                                        //名字          
                                    }
                                    if (this.InvokeRequired)
                                    {
                                        renew_controls rc = new renew_controls(renew);
                                        this.Invoke(rc);
                                    }
                                    else
                                        renew();
                                }
                            }
                    }
                    if (identifier == "lobby_enter")
                    {
                        string get_response = mydic["response"];
                        if (get_response == "0")//没有人 正常进入
                        {
                            num_socket_arg_returning_void d = new num_socket_arg_returning_void(show_form_lobby);
                            this.Invoke(d, new object[] { num, seat, socket_server });
                            this.Hide();
                            break;
                            //thread_client.Abort();
                            //this.Close();
                        }
                        if (get_response == "1")//已经在lobby中
                        {
                            MessageBox.Show("您已经进入房间，不能重复进入");
                        }
                        if (get_response == "2")//座位有人
                        {
                            MessageBox.Show("啊哦，好像有人了");
                        }
                    }
                }
                catch (DataEncodingException ex)
                {
                    MessageBox.Show("服务器内部错误，请稍后重试");
                }
            }
        }
    }
}
