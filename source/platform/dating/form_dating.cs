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
using MySql.Data.MySqlClient;
using System.IO;
using platform.common;

namespace platform.dating
{
    public partial class FormDating : FormBase
    {
        int tot_board = 9;
        uint num = 0, seat = 1;//记录进入的桌号和椅子
        Thread thread_client;
        String connection_string = "server = 45.32.82.133; user = ccol_user; database = chinese_chess_online; port = 3306; password = 123PengZiYu@";
        string user_email;
        Int32 BUFSIZ = 1024; // 缓冲区大小
        //Socket socket_client = null;
        Socket socket_server = null;
        // Modify the constructor
        List<ZBW> ZBWs = new List<ZBW>();
        ToolTip tip = new ToolTip();
        public FormDating(Socket server_socket, string email)
        {
            InitializeComponent();
            socket_server = server_socket;
            Text = "中国象棋对战大厅";
            user_email = email;
            tip.AutoPopDelay = 2500;
            tip.InitialDelay = 500;
            tip.ReshowDelay = 250;
            tip.ShowAlways = true;
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

        void avatar_onclick(object sender, EventArgs arg)
        {
            Cursor = Cursors.WaitCursor;
            new FormInfo(user_email).ShowDialog();
            Cursor = Cursors.Arrow;
        }

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
            { ["identifier"] = "lobby_enter", ["lobby_id"] = (num + 1).ToString(), ["seat"] = seat.ToString() });
            socket_server.Send(enter);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PictureBox user_avatar = new PictureBox();
            int user_avatar_size = 40;
            user_avatar.Size = new Size(user_avatar_size, user_avatar_size);
            user_avatar.Location = new Point(this.Width - user_avatar_size, this.Height - user_avatar_size);
            user_avatar.BackgroundImageLayout = ImageLayout.Stretch;
            user_avatar.Cursor = Cursors.Hand;
            user_avatar.Click += avatar_onclick;
            tip.SetToolTip(user_avatar, "查看个人信息");
            this.Controls.Add(user_avatar);

            using (MySqlConnection connection = new MySqlConnection(connection_string))
            {
                MySqlParameter e_address = new MySqlParameter("_email_address", MySqlDbType.String);
                MySqlParameter pic = new MySqlParameter("_avatar", MySqlDbType.MediumBlob);
                e_address.Value = user_email;
                e_address.Direction = ParameterDirection.Input;
                pic.Direction = ParameterDirection.Output;
                MySqlCommand cmd = new MySqlCommand("procedure_get_avatar", connection);
                cmd.Parameters.Add(e_address);
                cmd.Parameters.Add(pic);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException) { }
                MemoryStream myPic = null;
                byte[] mydata;
                if (Convert.IsDBNull(pic.Value))
                {
                    user_avatar.BackgroundImage = Properties.Resources.default_avatar;
                }
                else
                {
                    {
                        mydata = (byte[])pic.Value;
                        myPic = new MemoryStream(mydata);
                        user_avatar.BackgroundImage = Image.FromStream(myPic);
                    }

                }
            }

            for (uint i = 0; i < tot_board; i++)
            {
                ZBW myzbw = new ZBW(this, Convert.ToInt32(i));
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
            new chess_lobby.ChessLobby(this, num, seat, socket_server).Show();
        }

        private void button_renew_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.button_renew, "点击刷新大厅");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
            base.OnFormClosing(e);
        }

        public void receive_data()
        {
            while (true)
            {
                byte[] arr_data = new byte[BUFSIZ];
                try
                {
                    int length = socket_server.Receive(arr_data);
                }
                catch (SocketException)
                {
                    MessageBoxBase.Show("服务器掉线了！");
                    Application.Exit();
                }
                Dictionary<String, String> mydic;
                try
                {
                    mydic = DataEncoding.get_dictionary(arr_data);
                }
                catch (DataEncodingException)
                {
                    MessageBoxBase.Show("服务器内部错误，请稍后重试");
                    continue;
                }
                String identifier = mydic["identifier"];
                //MessageBoxBase.Show(DataEncoding.get_string(mydic));
                if (identifier == "plaza_renew")
                {
                    for (int i = 0; i < tot_board; i++)
                    {
                        bool flag = false;
                        for (int seat = 1; seat < 3; seat++)
                        {
                            String key = $"{i + 1}-{seat}";
                            string person = mydic[key];
                            //MessageBoxBase.Show(person);
                            if (person == "0")
                            {
                                if (seat == 1)
                                {
                                    ZBWs[i].redimage.BackgroundImage = null;
                                    ZBWs[i].redplayer.Text = "";
                                }
                                else
                                {
                                    ZBWs[i].blackimage.BackgroundImage = null;
                                    ZBWs[i].blackplayer.Text = "";
                                }
                                //没有人 重置为空值
                            }
                            else
                            {
                                flag = true;
                                //有人
                                void renew()
                                {
                                    pictureBox1.Enabled = true;
                                    pictureBox1.Visible = true;
                                    this.Cursor = Cursors.WaitCursor;
                                    ZBWs[i].chessboard.BackgroundImage = global::platform.Properties.Resources.chessboard;
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
                                        catch (MySqlException)
                                        {
                                            MessageBoxBase.Show("与服务器连接失败，请检查连接！");
                                        }
                                        this.Cursor = Cursors.Arrow;
                                        pictureBox1.Visible = false;
                                    };

                                    // 这是文档中推荐的使用connection的方式, 在这段代码结束之后自动关闭connection, 无须程序猿来关闭.

                                    MemoryStream myPic = null;
                                    byte[] mydata;
                                    if (Convert.IsDBNull(pic.Value))
                                    {
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
                                        //MessageBoxBase.Show(s);
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
                            if (!flag)
                            {
                                ZBWs[i].chessboard.BackgroundImage = null;
                            }
                        }
                    }
                }
                if (identifier == "lobby_enter")
                {
                    string get_response = mydic["response"];
                    if (get_response == "0")//没有人 正常进入
                    {
                        num_socket_arg_returning_void d = new num_socket_arg_returning_void(show_form_lobby);
                        this.Invoke(d, new object[] { num + 1, seat, socket_server });
                        this.Hide();
                        //thread_client.Abort();
                        //this.Close();
                    }
                    if (get_response == "1")//已经在lobby中
                    {
                        MessageBoxBase.Show("您已经进入房间，不能重复进入");
                    }
                    if (get_response == "2")//座位有人
                    {
                        MessageBoxBase.Show("啊哦，好像有人了");
                    }
                }
            }
        }

        public new void Show()
        {
            thread_client = new Thread(receive_data);
            thread_client.IsBackground = true;
            thread_client.Start();
            base.Show();
            button_renew_Click(this, new EventArgs());
        }

        public new void Hide()
        {
            base.Hide();
            thread_client.Abort();
        }
    }
}
