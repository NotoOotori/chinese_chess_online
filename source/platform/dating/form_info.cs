using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using platform.common;

namespace platform.dating
{
    public partial class FormInfo : FormBase
    {
        List<Label> win_lose_info = new List<Label>();
        String connection_string = "server = 45.32.82.133; user = ccol_user; database = chinese_chess_online; port = 3306; password = 123PengZiYu@";
        string user_email;
        public FormInfo(string email)
        {
            user_email = email;
            text_exit = "";
            InitializeComponent();
        }

        private void form_info_Load(object sender, EventArgs e)
        {
            label_email.Text = user_email;
            label_none.Text = "0";
            //string elo, win_tot, lose_tot,win_ratio;
            List<Int32> red_results = new List<int>();
            List<Boolean> is_reds = new List<bool>();
            List<string> game_strings = new List<string>();
            List<Image> avatars = new List<Image>();
            List<string> usernames = new List<string>();
            List<string> email_addresses = new List<string>();
            string select_name;
            user_avatar.BackgroundImageLayout = ImageLayout.Stretch;
            using (MySqlConnection connection = new MySqlConnection(connection_string))
            {

                MySqlParameter e_address = new MySqlParameter("_email_address", MySqlDbType.String);
                MySqlParameter pic = new MySqlParameter("_avatar", MySqlDbType.MediumBlob);
                MySqlParameter name = new MySqlParameter("_username", MySqlDbType.String);
                MySqlCommand cmd1 = new MySqlCommand("procedure_get_username", connection);
                {
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
                }//头像
                {
                    name.Direction = ParameterDirection.Output;
                    cmd1.Parameters.Add(e_address);
                    cmd1.Parameters.Add(name);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.ExecuteNonQuery();

                    label_name.Text = name.Value.ToString();
                }//名字
                MySqlDataAdapter adapter = new MySqlDataAdapter("select elo from platform_user where email_address = '" + user_email + "'", connection);
                DataTable data = new DataTable();
                DataTable data1 = new DataTable();
                DataTable data2 = new DataTable();
                adapter.Fill(data);
                label_elo.Text = data.Rows[0].ItemArray[0].ToString();
                string select_win = "select count(result) from game_record where (red_email_address = '" + user_email + "' or black_email_address = '" + user_email + "') and result =2"; 
                string select_lose = "select count(result) from game_record where (red_email_address = '" + user_email + "' or black_email_address = '" + user_email + "') and result =0";
                MySqlDataAdapter adapter1 = new MySqlDataAdapter(select_win,connection);
                adapter1.Fill(data1);
                
                label_win.Text = data1.Rows[0].ItemArray[0].ToString();
                MySqlDataAdapter adapter2 = new MySqlDataAdapter(select_lose, connection);
                adapter2.Fill(data2);
                label_lose.Text = data2.Rows[0].ItemArray[0].ToString();
                label_ratio.Text = ((Convert.ToDouble(label_win.Text) / ((Convert.ToDouble(label_lose.Text))+ Convert.ToDouble(label_win.Text)))*100).ToString("0.00") +"%";
            }
            using (MySqlConnection connection = new MySqlConnection(connection_string)) {
                DataTable data = new DataTable();               
                select_name = "select * from game_record,platform_user where (email_address=red_email_address or email_address = black_email_address) and email_address = '" + user_email + "' order by game_id desc";
                MySqlDataAdapter adapter_name = new MySqlDataAdapter(select_name,connection);
                adapter_name.Fill(data);
                foreach(DataRow dr in data.Rows)
                {
                    game_strings.Add(dr["game_string"].ToString());
                    //usernames.Add(dr["username"].ToString());
                    red_results.Add(Convert.ToInt32(dr["result"]));
                    if (dr["email_address"].ToString() == dr["red_email_address"].ToString())//这局对局中是红方
                    {
                        is_reds.Add(true);
                        string select_user_name = "select username,avatar from platform_user where email_address = '" + dr["black_email_address"].ToString() + "'";
                        DataTable namedata = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(select_user_name,connection);
                        adapter.Fill(namedata);
                        usernames.Add(namedata.Rows[0].ItemArray[0].ToString());
                        email_addresses.Add(dr["black_email_address"].ToString());
                        if (Convert.IsDBNull(namedata.Rows[0].ItemArray[1]))
                        {
                            avatars.Add(Properties.Resources.default_avatar);
                        }
                        else
                        {
                            byte[] mydata; MemoryStream myPic = null;
                            mydata = (byte[])(namedata.Rows[0].ItemArray[1]);
                            myPic = new MemoryStream(mydata);
                            avatars.Add(Image.FromStream(myPic));
                        }
                    }
                    else
                    {
                        is_reds.Add(false);
                        string select_user_name = "select username,avatar from platform_user where email_address = '" + dr["red_email_address"].ToString() + "'";
                        DataTable namedata = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(select_user_name, connection);
                        adapter.Fill(namedata);
                        usernames.Add(namedata.Rows[0].ItemArray[0].ToString());
                        email_addresses.Add(dr["red_email_address"].ToString());
                        if (Convert.IsDBNull(namedata.Rows[0].ItemArray[1]))
                        {
                            avatars.Add(Properties.Resources.default_avatar);
                        }
                        else
                        {
                            byte[] mydata; MemoryStream myPic = null;
                            mydata = (byte[])(namedata.Rows[0].ItemArray[1]);
                            myPic = new MemoryStream(mydata);
                            avatars.Add(Image.FromStream(myPic));
                        }
                    }
                    
                }
            }
            new RecentGames(red_results, is_reds, game_strings, avatars, usernames, email_addresses)
            {
                Parent = this,
                Location = new Point(groupBox3.Location.X + 29, groupBox3.Location.Y + 22)
            }.Show();
            groupBox3.SendToBack();
        }
    }
}
