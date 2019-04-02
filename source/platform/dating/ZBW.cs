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
        public Label board_id = new Label();
        public PictureBox redimage = new PictureBox();
        public PictureBox blackimage = new PictureBox();
        public uint lobby_num;
        public ZBW(FormDating form,int num)
        {
            int mywidth = form.Width;
            int my_ver_gap = 125;
            int my_hor_gap = mywidth / 3;
            int board_size = 60;
            int image_size = 60;
            int chess_init = mywidth / 8;
            int chess_gap = board_size + 10;
            int ver_init = form.Height / 6;
            int ver_chess_gap = 10;
            int ver_play_gap = image_size + 10;

            lobby_num = Convert.ToUInt32(num);

            redimage.Tag = lobby_num;
            blackimage.Tag = lobby_num;

            form.Controls.Add(chessboard);
            chessboard.Size = new Size(board_size, board_size);
            chessboard.Location = new Point((num % 3) * my_hor_gap+chess_init, num / 3 * my_ver_gap + ver_init);
            chessboard.BackgroundImageLayout = ImageLayout.Stretch;
            //chessboard.Cursor = Cursors.Hand;
            chessboard.BorderStyle = BorderStyle.FixedSingle;
            chessboard.BackColor = Color.Transparent;

            board_id.Size = new Size(board_size, 18);
            board_id.BackColor = Color.Transparent;
            board_id.Location = new Point((num % 3) * my_hor_gap + chess_init, num / 3 * my_ver_gap + ver_init+ver_chess_gap+board_size);
            board_id.Text = "- "+(num+1).ToString()+" -";
            board_id.TextAlign = ContentAlignment.MiddleCenter;
            board_id.Font = new Font("Consolas", 12f);
            form.Controls.Add(board_id);
            //board_id.BringToFront();

            redimage.Size = new Size(image_size, image_size);
            redimage.Location = new Point((num % 3) * my_hor_gap+chess_init-chess_gap, num / 3 * my_ver_gap + ver_init+ver_chess_gap);
            redimage.BorderStyle = BorderStyle.Fixed3D;
            redimage.Cursor = Cursors.Hand;
            redimage.BackgroundImageLayout = ImageLayout.Stretch;
            redimage.BackColor = Color.Transparent;
            form.Controls.Add(redimage);

            blackimage.Size = new Size(image_size, image_size);
            blackimage.Location = new Point((num % 3) * my_hor_gap + chess_init + chess_gap, num / 3 * my_ver_gap + ver_init + ver_chess_gap);
            blackimage.BorderStyle = BorderStyle.Fixed3D;
            blackimage.Cursor = Cursors.Hand;
            blackimage.BackColor = Color.Transparent;
            blackimage.BackgroundImageLayout = ImageLayout.Stretch;
            form.Controls.Add(blackimage);

            redplayer.Location = new Point((num % 3) * my_hor_gap + chess_init - chess_gap, num / 3 * my_ver_gap + ver_init + ver_chess_gap+ver_play_gap);
            redplayer.Size = new Size(image_size, 18);
            redplayer.BackColor = Color.Transparent;
            redplayer.Font = new Font("Comic Sans MS", 12f);
            form.Controls.Add(redplayer);

            blackplayer.Location = new Point((num % 3) * my_hor_gap + chess_init + chess_gap, num / 3 * my_ver_gap + ver_init + ver_chess_gap + ver_play_gap);
            blackplayer.Size = new Size(image_size , 18);
            blackplayer.BackColor = Color.Transparent;
            blackplayer.Font = new Font("Comic Sans MS", 12f);
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
                    //MessageBoxBase.Show("here!");
                    mydata = (byte[])(dataTable.Rows[0].ItemArray[0]);
                    myPic = new MemoryStream(mydata);
                    blackimage.Image = Image.FromStream(myPic);
                }

            }
        }
       
    }
}
