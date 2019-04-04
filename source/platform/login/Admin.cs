using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using platform.common;
using MySql.Data.MySqlClient;

namespace platform.login
{
    
    public partial class Admin :FormBase
    {
        String connection_string = "server = 45.32.82.133; user = ccol_user; database = chinese_chess_online; port = 3306; password = 123PengZiYu@";
        int state = 0;
        public Admin()
        {
            
            InitializeComponent();
        }
        
        private void Admin_Load(object sender, EventArgs e)
        {
            
            try
            {
                MySqlConnection conn = new MySqlConnection(connection_string);
                DataSet data = new DataSet();
                MySqlDataAdapter adapter = new MySqlDataAdapter("select email_address from platform_user", conn);
                adapter.Fill(data, "email");
                comboBox1.DataSource = data.Tables["email"];
                comboBox1.DisplayMember = "email_address";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            state = 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (state == 0)
                return;
            MySqlConnection conn = new MySqlConnection(connection_string);
            
                MySqlDataAdapter adapter1 = new MySqlDataAdapter("select * from user_login_record where email_address='" + comboBox1.Text + "'", conn);
                MySqlDataAdapter adapter2 = new MySqlDataAdapter("select email_address,user_logout_record.login_id,logout_time from user_logout_record,user_login_record " +
                    "where user_logout_record.login_id = user_login_record.login_id and user_login_record.email_address='" + comboBox1.Text + "'", conn);
                MySqlDataAdapter adapter3 = new MySqlDataAdapter("select * from game_record where red_email_address='" + comboBox1.Text +"' or black_email_address = '" + comboBox1.Text + "'", conn);

                DataSet data = new DataSet();
            try
            {
                adapter1.Fill(data, "login");
                
                dataGridView1.DataSource = data.Tables["login"];
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
                //DataSet data = new DataSet();
            try
            {
                adapter2.Fill(data, "logout");
                dataGridView2.DataSource = data.Tables["logout"];
            }//DataSet data = new DataSet();
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }    
            adapter3.Fill(data, "game");
                dataGridView3.DataSource = data.Tables["game"];
            
        }
    }
}
