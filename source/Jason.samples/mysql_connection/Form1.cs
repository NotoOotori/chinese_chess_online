using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
// 请先右键项目->Add->Reference->Browse添加MySql.Data.dll.

namespace mysql_connection
{
    public partial class Form1 : Form
    {
        String connection_string = "server = 45.32.82.133; user = ccol_user; database = chinese_chess_online; port = 3306; password = 123PengZiYu@";
        // 连接数据库时需要用到的字符串.

        public Form1()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        private void button_adapter_snum_Click(object sender, EventArgs e)
        {
            String snum = text_box_adapter_snum.Text;
            String select = "SELECT course.cname, sc.score FROM student, sc, sections, course\r\n" +
                $"WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum AND student.snum = '{snum}'\r\n";
            // SELECT AS中肯定不能有中文, 别的地方可能不能有中文
            using (MySqlConnection connection = new MySqlConnection(connection_string))
            // 这是文档中推荐的使用connection的方式, 在这段代码结束之后自动关闭connection, 无须程序猿来关闭.
            {
                connection.Open();
                try
                {
                    MySqlCommand command = new MySqlCommand(select, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    adapter.Fill(data_set, "snum");
                    data_grid_view_adapter.DataSource = data_set;
                    data_grid_view_adapter.DataMember = "snum";
                }
                catch (System.IndexOutOfRangeException ex)
                {
                    MessageBox.Show("您的学号可能有误.\r\n报错信息:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
