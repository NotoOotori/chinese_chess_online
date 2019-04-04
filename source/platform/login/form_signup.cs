using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using platform.common;


namespace platform.login
{
    public partial class FormSignup : FormBase
    {
        static string connection_string = "server = 45.32.82.133; user = ccol_user; database = chinese_chess_online; port = 3306; password = 123PengZiYu@";
        public FormSignup(FormLogin form_login)
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
            this.form_login = form_login;
        }

        FormLogin form_login;
        
        private void form_signup_Load(object sender, EventArgs e)
        {
            label12.Text = "";
            label10.Text = "";
            textBox_password.PasswordChar = '*';
            textBox_confirm.PasswordChar = '*';
            label10.ForeColor = Color.White;
            label10.Text = "用户名不能包含<,>,/";            
            int i;
            for (i = 1950; i <= 2015; i++)
                comboBox_yy.Items.Add(i.ToString());
            for (i = 1; i < 13; i++)
                comboBox_mm.Items.Add(i.ToString());
            for (i = 1; i < 32; i++)
                comboBox_dd.Items.Add(i.ToString());
            Text = "";
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
            string user_name=textBox_user.Text;
            for (int i = 0; i < user_name.Length; i++)
            {
                if (user_name.Substring(i, 1) == "<" || user_name.Substring(i, 1) == ">" || user_name.Substring(i, 1) == "/")
                {
                    label10.ForeColor = Color.Red;
                    label10.Text = "用户名包含<>/,请重新输入";
                    return;
                }
            }
            if (textBox_password.Text != textBox_confirm.Text)
            {
                label12.Text = "密码输入错误，请重新输入";
                return;
            }
            
            SendFileBytesToDatabase();
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_user, "请勿输入<,>,/");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            form_login.Show();
            base.OnFormClosing(e);
        }

        private static MySqlConnection CreateConnection()
        {
            MySqlConnection Connection = new MySqlConnection(connection_string); //建立MySQL连接
            return Connection;
        }

        private Byte[] compress_image(
            Byte[] image, Int32 width = 160, Int32 height = 160)
        {
            using (MemoryStream ms = new MemoryStream(image, 0, image.Length))
            {
                using (Image tmp_img = Image.FromStream(ms))
                {
                    using (Bitmap b = new Bitmap(tmp_img, new Size(width, height)))
                    {
                        using (MemoryStream ms2 = new MemoryStream())
                        {
                            b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                            image = ms2.ToArray();
                        }
                    }
                }
            }
            return image;
        }

        private void SendFileBytesToDatabase()
        {
            //procedure_sign_up
            ImageConverter imc = new ImageConverter();
            MySqlConnection sendDataConnection = CreateConnection();
            MySqlCommand cmd = new MySqlCommand("procedure_sign_up", sendDataConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlParameter email_address = new MySqlParameter("_email_address",MySqlDbType.VarChar,254);
            MySqlParameter username = new MySqlParameter("_username", MySqlDbType.VarChar, 16);
            MySqlParameter password = new MySqlParameter("_unencrypted_password", MySqlDbType.VarChar, 256);
            MySqlParameter avatar = new MySqlParameter("_avatar", MySqlDbType.MediumBlob);
            MySqlParameter gender = new MySqlParameter("_gender", MySqlDbType.String, 1);
            MySqlParameter birthday = new MySqlParameter("_birthday", MySqlDbType.Date);
            email_address.Direction = ParameterDirection.Input;
            username.Direction = ParameterDirection.Input;
            password.Direction = ParameterDirection.Input;
            avatar.Direction = ParameterDirection.Input;
            gender.Direction = ParameterDirection.Input;
            birthday.Direction = ParameterDirection.Input;
            email_address.Value = this.textBox_email.Text;
            username.Value = this.textBox_user.Text;
            password.Value = this.textBox_password.Text;
            avatar.Value = compress_image(
                (byte[])imc.ConvertTo(pictureBox_avatar.Image, typeof(Byte[])));
            gender.Value = ((comboBox_gender.Text=="男")?"m":"f");
            String yyyy = comboBox_yy.Text;
            String mm = comboBox_mm.Text;
            String dd = comboBox_dd.Text;
            birthday.Value = $"{yyyy}/{mm}/{dd}";
            cmd.Parameters.Add(email_address);
            cmd.Parameters.Add(username);
            cmd.Parameters.Add(password);
            cmd.Parameters.Add(avatar);
            cmd.Parameters.Add(gender);
            cmd.Parameters.Add(birthday);
            sendDataConnection.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBoxBase.Show("注册成功！");
                this.Close();
            }
            catch (Exception e)
            {
                MessageBoxBase.Show("注册失败：" + e.Message);
            }
            finally
            {
                sendDataConnection.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.Title = "Select your avatar";
            if (opd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox_avatar.Image = Image.FromFile(opd.FileName);
                label13.Visible = false;
            }
            else
                return;
        }
    }
}
