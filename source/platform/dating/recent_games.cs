using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using platform.common;

namespace platform.dating
{
    public partial class RecentGames : UserControl
    {
        public RecentGames(
            List<Int32> red_results,
            List<Boolean> is_reds,
            List<String> game_strings,
            List<Image> avatars,
            List<String> usernames,
            List<String> email_addresses)
        {
            Int32 length;
            try
            {
                length = Math.Min(new[] {
                    red_results.Count,
                    is_reds.Count,
                    game_strings.Count,
                    avatars.Count,
                    usernames.Count,
                    email_addresses.Count,
                }.Distinct().Single(), TOTAL_NUM);
            }
            catch (InvalidOperationException)
            {
                throw new RecentGameException("Length of the lists do not match.");
            }

            InitializeComponent();
            this.Size = CONTROL_SIZE;
            this.BackColor = Color.Transparent;
            tool_tip = new ToolTip()
            {
                AutoPopDelay = 2500,
                InitialDelay = 500,
                ReshowDelay = 250,
                ShowAlways = true
            };

            for (Int32 count = 0; count < length; count++)
                add_recent_game(
                    count,
                    red_results[count],
                    is_reds[count],
                    game_strings[count],
                    avatars[count],
                    usernames[count],
                    email_addresses[count]);
        }

        private const Int32 TOTAL_NUM = 10;
        private static readonly Size CONTROL_SIZE = new Size(400, 150);
        private static readonly Size BOX_SIZE = new Size(30, 30);
        private const Int32 HORIZONAL_INIT = 5;
        private static readonly Int32 HORIZONAL_GAP = 10 + BOX_SIZE.Width;
        private static readonly Int32[] VERTICAL_LOC = new Int32[] { 40, 80 };
        private static readonly Color RED_COLOUR = Color.Red;
        private static readonly Color BLACK_COLOUR = Color.DarkSeaGreen;
        private static readonly Font DEFAULT_FONT = new Font("STHupo", 18);

        private ToolTip tool_tip { get; }

        private void add_recent_game(
            Int32 count,
            Int32 red_result,
            Boolean is_red,
            String game_string,
            Image avatar,
            String username,
            String email_address)
        {
            Point label_location = new Point(
                HORIZONAL_INIT + count * HORIZONAL_GAP,
                VERTICAL_LOC[0]);
            Point picture_box_location = new Point(
                HORIZONAL_INIT + count * HORIZONAL_GAP,
                VERTICAL_LOC[1]);
            add_label(label_location, red_result, is_red, game_string, username);
            add_picture_box(picture_box_location, avatar, username, email_address);
        }

        #region ' Label '

        private void add_label(
            Point location,
            Int32 red_result,
            Boolean is_red,
            String game_string,
            String username)
        {
            Label label = new Label()
            {
                Parent = this,
                Text = get_text(red_result, is_red),
                ForeColor = get_colour(is_red),
                BackColor = Color.Transparent,
                Location = location,
                AutoSize = false,
                Size = BOX_SIZE,
                Font = DEFAULT_FONT,
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                Tag = game_string
            };
            label.Click += label_on_click;
            tool_tip.SetToolTip(label, $"复盘您与{username}的对局");
        }

        private String get_text(Int32 red_result, Boolean is_red)
        {
            Int32 result = is_red ? red_result : 2 - red_result;
            switch (result)
            {
                default:
                    throw new RecentGameException("Result out of range!");
                case 0:
                    return "负";
                case 1:
                    return "和";
                case 2:
                    return "胜";
            }
        }

        private Color get_colour(Boolean is_red)
        {
            return is_red ? RED_COLOUR : BLACK_COLOUR;
        }

        private void label_on_click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            String game_string = String.Format(
                "{0};{1}", chess_lobby.FEN.init_w, (sender as Label).Tag as String);
            String arguments = $"-g \"{game_string}\" -s readonly";
            string path = Path.Combine(Path.GetTempPath(), "temp_replay.exe");
            File.WriteAllBytes(path, Properties.Resources.replay);
            Process.Start(path, arguments);

            Cursor = Cursors.Arrow;
        }

        #endregion

        #region ' PictureBox '

        private void add_picture_box(
            Point location, Image avatar, String username, String email_address)
        {
            PictureBox picture_box = new PictureBox()
            {
                Parent = this,
                Image = avatar,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Location = location,
                Size = BOX_SIZE,
                Cursor = Cursors.Hand,
                Tag = email_address
            };
            picture_box.Click += picture_box_on_click;
            tool_tip.SetToolTip(picture_box, $"查看{username}的信息");
        }

        private void picture_box_on_click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            String email_address = (sender as PictureBox).Tag as String;
            new FormInfo(email_address).ShowDialog();

            Cursor = Cursors.Arrow;
        }

        #endregion
    }

    public class RecentGameException : Exception
    {
        public RecentGameException()
        {; }

        public RecentGameException(String message):
            base(message)
        {; }
    }
}
