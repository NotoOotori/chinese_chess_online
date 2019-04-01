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


namespace platform.login
{
    public partial class form_signup : Form
    {
        static string connection_string = "server = 45.32.82.133; user = ccol_user; database = chinese_chess_online; port = 3306; password = 123PengZiYu@";
        public form_signup()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }
        String HOST = "45.32.82.133"; // IP地址
        Int32 PORT = 21567; // 端口
        Int32 BUFSIZ = 1024; // 缓冲区大小
        
        private void form_signup_Load(object sender, EventArgs e)
        {
            label12.Text = "";
            label10.Text = "";
            textBox_password.PasswordChar = '*';
            textBox_confirm.PasswordChar = '*';
            label10.Text = "用户名不能包含<,>,/";
            label10.ForeColor = Color.Black;
            int i;
            for (i = 1950; i <= 2015; i++)
                comboBox_yy.Items.Add(i.ToString());
            for (i = 1; i < 13; i++)
                comboBox_mm.Items.Add(i.ToString());
            for (i = 1; i < 32; i++)
                comboBox_dd.Items.Add(i.ToString());
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
            for(int i=0;i<user_name.Length;i++)
            {
                if(user_name.Substring(i,1)=="<"|| user_name.Substring(i, 1) == ">"|| user_name.Substring(i, 1) == "/")
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

        private static MySqlConnection CreateConnection()
        {
            MySqlConnection Connection = new MySqlConnection(connection_string); //建立MySQL连接
            return Connection;
        }

        private static byte[] FileToBytes(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            byte[] buffer = new byte[fi.Length];
            FileStream fs = fi.OpenRead();
            fs.Read(buffer, 0, Convert.ToInt32(fi.Length));
            fs.Close();
            return buffer;

        }

        private void SendFileBytesToDatabase()
        {
            //procedure_sign_up
            ImageConverter imc = new ImageConverter();
            MySqlConnection sendDataConnection = CreateConnection();
            MySqlCommand cmd = new MySqlCommand("procedure_sign_up", sendDataConnection);
            string sendFileSql = "insert into platform_user(email_address,avatar) values(?email_address,?avatar);";
            //MySqlCommand sendCmd = new MySqlCommand(sendFileSql, sendDataConnection);
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
            avatar.Value = (byte[])imc.ConvertTo(pictureBox_avatar.Image, typeof(Byte[]));
            gender.Value = comboBox_gender.Text;
            String yyyy = comboBox_yy.Text;
            String mm = comboBox_mm.Text;
            String dd = comboBox_dd.Text;
            birthday.Value = $"{yyyy}/{dd}/{mm}";
            sendDataConnection.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("向数据库储存数据完成");
            }
            catch (Exception e)
            {
                Console.WriteLine("向数据库存储数据失败：" + e.Message);
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
                pictureBox_avatar.Image = Image.FromFile(opd.FileName);
            else
                return;
            
        }
    }
}
