using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using platform.common;

namespace platform.dating
{
    class ZBW
    {
        String connection_string = "server = 45.32.82.133; user = ccol_user; database = chinese_chess_online; port = 3306; password = 123PengZiYu@";
        public Label redplayer = new Label();
        public Label blackplayer = new Label();
        public PictureBox chessboard = new PictureBox();
        //public PictureBox red_ready = new PictureBox();
        //public PictureBox black_ready = new PictureBox();
        public PictureBox redimage = new PictureBox();
        public PictureBox blackimage = new PictureBox();
        public uint lobby_num;
        public ZBW(FormDating form,int num)
        {
            int mywidth = form.Width; int mygap = 125 / 3 * 2;
            lobby_num = Convert.ToUInt32(num);

            redimage.Tag = lobby_num;
            blackimage.Tag = lobby_num;

            form.Controls.Add(chessboard);
            chessboard.Size = new Size(60 * 2 / 3, 60 * 2 / 3);
            chessboard.Location = new Point((num % 4 + 1) * mywidth / 4 - 170 * 2 / 3, num / 4 * mygap + 42 * 2 / 3);
            chessboard.BackgroundImageLayout = ImageLayout.Stretch;
            chessboard.Cursor = Cursors.Hand;
            chessboard.BorderStyle = BorderStyle.FixedSingle;
            chessboard.BackColor = Color.Transparent;

            redimage.Size = new Size(54 * 2 / 3, 54 * 2 / 3);
            redimage.Location = new Point((num % 4 + 1) * mywidth / 4 - 240 * 2 / 3, num / 4 * mygap + 56 * 2 / 3);
            redimage.BorderStyle = BorderStyle.Fixed3D;
            redimage.Cursor = Cursors.Hand;
            redimage.BackColor = Color.Transparent;
            form.Controls.Add(redimage);

            blackimage.Size = new Size(54 * 2 / 3, 54 * 2 / 3);
            blackimage.Location = new Point((num % 4 + 1) * mywidth / 4 - 96 * 2 / 3, num / 4 * mygap + 56 * 2 / 3);
            blackimage.BorderStyle = BorderStyle.Fixed3D;
            blackimage.Cursor = Cursors.Hand;
            blackimage.BackColor = Color.Transparent;
            form.Controls.Add(blackimage);

            //red_ready.Location  = new Point((num % 4 + 1) * mywidth / 4 - 174 * 2 / 3-20,num/4*mygap + 42*2/3+36);
            //red_ready.Size = new Size(30 * 2 / 3, 30 * 2 / 3);
            //black_ready.BackgroundImageLayout = ImageLayout.Stretch;
            ////panel.Controls.Add(red_ready);

            //black_ready.Location = new Point((num % 4 + 1) * mywidth / 4 - 75 * 2 / 3-20, num / 4 * mygap + 42 * 2 / 3+36);
            //black_ready.Size = new Size(30 * 2 / 3, 30 * 2 / 3);
            //black_ready.BackgroundImageLayout = ImageLayout.Stretch;
            //panel.Controls.Add(black_ready);

            redplayer.Location = new Point((num % 4 + 1) * mywidth / 4 - 240 * 2 / 3, num / 4 * mygap + 125 * 2 / 3);
            redplayer.Size = new Size(54 * 2 / 3, 18 * 2 / 3);
            redplayer.BackColor = Color.Transparent;
            form.Controls.Add(redplayer);

            blackplayer.Location = new Point((num % 4 + 1) * mywidth / 4 - 96 * 2 / 3, num / 4 * mygap + 125 * 2 / 3);
            blackplayer.Size = new Size(54 * 2 / 3, 18 * 2 / 3);
            blackplayer.BackColor = Color.Transparent;
            form.Controls.Add(blackplayer);
        }       

        public void setplayer(string e_mail)
        {
            string select = "SELECT pic from user" + $"WHERE email ="+e_mail;
            string select2 = "SELECT name from user " + $"WHERE email = " + e_mail;
            // SELECT AS中肯定不能有中文, 别的地方可能不能有中文
            // 当心恶意注入！
            using (MySqlConnection connection = new MySqlConnection(connection_string))
            // 这是文档中推荐的使用connection的方式, 在这段代码结束之后自动关闭connection, 无须程序猿来关闭.
            {
                connection.Open();
                DataTable dataTable = new DataTable();
                try
                {
                    using (MySqlCommand command = new MySqlCommand(select, connection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);                       
                        adapter.Fill(dataTable);
                    }
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("报错信息:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    using(MySqlCommand cmd = new MySqlCommand(select2, connection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        blackplayer.Text = cmd.ExecuteScalar().ToString(); 
                    }
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("报错信息:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MemoryStream myPic = null;
                byte[] mydata;
                if (dataTable.Rows.Count!=0)
                {                   
                    if (Convert.IsDBNull(dataTable.Rows[0].ItemArray[0]))//用户没存头像
                    {
                        try
                        {
                            blackimage.Image = Image.FromFile(".\\blank.jpg");
                        }
                        catch (Exception) { }
                        return;
                    }
                    //MessageBox.Show("here!");
                    mydata = (byte[])(dataTable.Rows[0].ItemArray[0]);
                    myPic = new MemoryStream(mydata);
                    blackimage.Image = Image.FromStream(myPic);
                }

            }
        }
       
    }
}
