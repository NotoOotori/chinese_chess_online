using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platform.chess_lobby
{
    public partial class PlayBook : UserControl
    {
        #region ' Constructors '

        public PlayBook()
        {
            InitializeComponent();

            panel_play_book.ControlAdded += panel_play_book_control_added;
        }

        #endregion

        #region ' Properties '

        private List<Label> label_ids { get; } = new List<Label>();
        private List<Label> label_red_moves { get; } = new List<Label>();
        private List<Label> label_black_moves { get; } = new List<Label>();

        private Int32 count_red { get { return label_red_moves.Count; } }
        private Int32 count_black { get { return label_black_moves.Count; } }

        private Font DEFAULT_FONT { get; } = new Font("YouYuan", 12);
        private const Int32 VERTICAL_INIT = 5;
        private const Int32 VERTICAL_GAP = 25;
        private static readonly Int32[] HORIZONAL_LOC = new Int32[]{ 0, 40, 120 };
        private static readonly Size LABEL_ID_SIZE = new Size(32, 16);

        #endregion

        #region ' Methods '

        public void add(String move)
        {
            if (count_red == count_black)
                add_red(move, count_red + 1);
            else if (count_red > count_black)
                add_black(move, count_black + 1);
            else
                throw new ApplicationException("内部错误!");
        }

        private void add_red(String move, Int32 turn)
        {
            Int32 vertical_pos = VERTICAL_INIT + (turn - 1) * VERTICAL_GAP;
            Label label_id = new Label()
            {
                Parent = panel_play_book,
                Text = turn.ToString(),
                AutoSize = false,
                Font = DEFAULT_FONT,
                Location = new Point(HORIZONAL_LOC[0], vertical_pos),
                Size = LABEL_ID_SIZE,
                TextAlign = ContentAlignment.TopRight
            };
            Label label_red_move = new Label()
            {
                Parent = panel_play_book,
                Text = move,
                AutoSize = true,
                Font = DEFAULT_FONT,
                Location = new Point(HORIZONAL_LOC[1], vertical_pos)
            };
            label_ids.Add(label_id);
            label_red_moves.Add(label_red_move);
        }

        private void add_black(String move, Int32 turn)
        {
            Int32 vertical_pos = VERTICAL_INIT + (turn - 1) * VERTICAL_GAP;
            Label label_black_move = new Label()
            {
                Parent = panel_play_book,
                Text = move,
                AutoSize = true,
                Font = DEFAULT_FONT,
                Location = new Point(HORIZONAL_LOC[2], vertical_pos)
            };
            label_black_moves.Add(label_black_move);
        }

        public void clear()
        {
            foreach (Label label in label_ids)
            {
                Controls.Remove(label);
                label.Dispose();
            }
            foreach (Label label in label_red_moves)
            {
                Controls.Remove(label);
                label.Dispose();
            }
            foreach (Label label in label_black_moves)
            {
                Controls.Remove(label);
                label.Dispose();
            }
            label_ids.Clear();
            label_red_moves.Clear();
            label_black_moves.Clear();
        }

        private void panel_play_book_control_added(object sender, ControlEventArgs e)
        {
            panel_play_book.ScrollControlIntoView(e.Control);
        }

        #endregion
    }
}
