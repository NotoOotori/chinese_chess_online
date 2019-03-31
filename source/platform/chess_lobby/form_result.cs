using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platform.chess_lobby
{
    public partial class FormResult : Form
    {
        public FormResult(Int32 result, Int32 elo_change)
        {
            InitializeComponent();

            glossyButton_confirm.Font = new Font(
                "Comic Sans MS", Convert.ToSingle(22.75));
            glossyButton_confirm.Text = "OK";
            glossyButton_confirm.label1.Click += new EventHandler(
                glossyButton_confirm_Click);

            List<Color> colours = new List<Color>();
            switch (result)
            {
                default:
                    throw new ArgumentOutOfRangeException("Result out of range!");
                case 0:
                    colours.Add(Color.Lime);
                    colours.Add(Color.Red);
                    break;
                case 1:
                    colours.Add(Color.White);
                    colours.Add(Color.White);
                    break;
                case 2:
                    colours.Add(Color.Red);
                    colours.Add(Color.Lime);
                    break;
            }

            label_result_player.Text = result.to_chinese_result();
            label_result_opponent.Text = (2 - result).to_chinese_result();
            label_elo_change_player.Text = elo_change.to_elo_change_string();
            label_elo_change_opponent.Text = (-elo_change).to_elo_change_string();

            label_result_player.ForeColor = colours[0];
            label_result_opponent.ForeColor = colours[1];
            label_elo_change_player.ForeColor = colours[0];
            label_elo_change_opponent.ForeColor = colours[1];
        }

        private void glossyButton_confirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
