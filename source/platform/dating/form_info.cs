﻿using System;
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
            ForeColor = Color.DarkCyan;
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
                        mydata = (byte[])pic.Value;
                        myPic = new MemoryStream(mydata);
                        user_avatar.BackgroundImage = Image.FromStream(myPic);
                    }
                }//头像
                {
                    name.Direction = ParameterDirection.Output;
                    cmd1.Parameters.Add(e_address);
                    cmd1.Parameters.Add(name);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.ExecuteNonQuery();

                    label_name.Text = name.Value.ToString();
                    this.Text = $"{label_name.Text}的信息";
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

            #region ' Recent Games '

            using (MySqlConnection connection = new MySqlConnection(connection_string)) {
                DataTable data = new DataTable();               
                select_name = @"
                    SELECT
                        record.result AS red_result,
                        CASE
                            WHEN self.email_address = red_email_address THEN TRUE
                            ELSE FALSE
                        END AS is_red,
                        record.game_string,
                        opponent.avatar AS opponent_avatar,
                        opponent.username AS opponent_username,
                        opponent.email_address AS opponent_email_address
                    FROM platform_user AS opponent, platform_user AS self, game_record AS record
                    WHERE
                        (
                            (self.email_address = record.red_email_address AND opponent.email_address = record.black_email_address)
                            OR
                            (self.email_address = record.black_email_address AND opponent.email_address = record.red_email_address)
                        )
                        AND
                            self.email_address = @email_address
                    ORDER BY record.game_id DESC;";
                MySqlCommand cmd = new MySqlCommand(select_name, connection);
                cmd.Parameters.Add(new MySqlParameter("@email_address", MySqlDbType.VarString, 254)
                {
                    Value = user_email
                });
                MySqlDataAdapter adapter_name = new MySqlDataAdapter(cmd);
                adapter_name.Fill(data);
                foreach(DataRow dr in data.Rows)
                {
                    red_results.Add(Convert.ToInt32(dr["red_result"]));
                    is_reds.Add(Convert.ToBoolean(dr["is_red"]));
                    game_strings.Add(dr["game_string"].ToString());
                    if (Convert.IsDBNull(dr["opponent_avatar"]))
                    {
                        avatars.Add(Properties.Resources.default_avatar);
                    }
                    else
                    {
                        byte[] mydata = (byte[])dr["opponent_avatar"];
                        avatars.Add(Image.FromStream(new MemoryStream(mydata)));
                    }
                    usernames.Add(dr["opponent_username"].ToString());
                    email_addresses.Add(dr["opponent_email_address"].ToString());
                }
            }
            new RecentGames(red_results, is_reds, game_strings, avatars, usernames, email_addresses)
            {
                Parent = this,
                Location = new Point(
                    groupBox3.Location.X + 29, groupBox3.Location.Y + 22)
            }.Show();
            groupBox3.SendToBack();

            #endregion
        }
    }
}
