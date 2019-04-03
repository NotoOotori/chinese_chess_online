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
    public partial class form_info : FormBase
    {
        List<Label> win_lose_info = new List<Label>();
        String connection_string = "server = 45.32.82.133; user = ccol_user; database = chinese_chess_online; port = 3306; password = 123PengZiYu@";
        string user_email;
        public form_info(string email)
        {
            user_email = email;
            InitializeComponent();
        }

        private void form_info_Load(object sender, EventArgs e)
        {
            label_email.Text = user_email;
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
        }
    }
}
