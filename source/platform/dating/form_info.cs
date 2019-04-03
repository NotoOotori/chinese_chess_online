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

        }
    }
}
