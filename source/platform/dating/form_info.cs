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
        private FormDating dating { get; }
        public FormInfo(FormDating dating, string email)
        {
            this.dating = dating;
            user_email = email;
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
            dating.Show();
        }

        private void form_info_Load(object sender, EventArgs e)
        {
            label_email.Text = user_email;
            string elo, win_tot, lose_tot,win_ratio;
            List<Int32> redresults = new List<int>();
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
            }
            using (MySqlConnection connection = new MySqlConnection(connection_string)) {
                DataTable data = new DataTable();               
                select_name = "select * from game_record,platform_user where (email_address=red_email_address or email_address = black_email_address) and email_address = '" + user_email + "' order by game_id desc";
                MySqlDataAdapter adapter_name = new MySqlDataAdapter(select_name,connection);
                adapter_name.Fill(data);
                foreach(DataRow dr in data.Rows)
                {
                    game_strings.Add(dr["game_string"].ToString());
                    usernames.Add(dr["username"].ToString());
                    redresults.Add(Convert.ToInt32(dr["result"]));
                    if (dr["email_address"].ToString() == dr["red_email_address"].ToString())
                    {
                        is_reds.Add(true);
                        email_addresses.Add(dr["black_email_address"].ToString());
                    }
                    else
                    {
                        is_reds.Add(false);
                        email_addresses.Add(dr["red_email_address"].ToString());
                    }
                    redresults.Add(Convert.ToInt32( dr["result"]));
                    if (Convert.IsDBNull(dr["avatar"]))
                    {
                        avatars.Add(Properties.Resources.default_avatar);
                    }
                    else
                    {
                        byte[] mydata; MemoryStream myPic = null;
                        mydata = (byte[])dr["avatar"];
                        myPic = new MemoryStream(mydata);
                        avatars.Add( Image.FromStream(myPic));
                    }
                }
            }
        }
    }
}
