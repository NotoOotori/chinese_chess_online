namespace platform.chess_lobby
{
    partial class ChessLobby
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_ready = new System.Windows.Forms.Label();
            this.button_surrender = new platform.GlossyButton();
            this.button_draw = new platform.GlossyButton();
            this.button_ready = new platform.GlossyButton();
            this.chessboard_container = new platform.chess_lobby.ChessboardContainer();
            this.play_book = new platform.chess_lobby.PlayBook();
            this.SuspendLayout();
            // 
            // button_exit
            // 
            this.button_exit.FlatAppearance.BorderSize = 0;
            // 
            // button_min
            // 
            this.button_min.FlatAppearance.BorderSize = 0;
            // 
            // label_ready
            // 
            this.label_ready.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ready.AutoSize = true;
            this.label_ready.BackColor = System.Drawing.Color.Transparent;
            this.label_ready.Font = new System.Drawing.Font("FZShuTi", 50F);
            this.label_ready.ForeColor = System.Drawing.Color.Cyan;
            this.label_ready.Location = new System.Drawing.Point(80, 570);
            this.label_ready.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_ready.Name = "label_ready";
            this.label_ready.Size = new System.Drawing.Size(298, 70);
            this.label_ready.TabIndex = 5;
            this.label_ready.Text = "准备就绪";
            this.label_ready.Visible = false;
            // 
            // button_surrender
            // 
            this.button_surrender.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_surrender.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.button_surrender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_surrender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.button_surrender.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button_surrender.DownColor = System.Drawing.Color.DarkOliveGreen;
            this.button_surrender.EnterColor = System.Drawing.Color.Olive;
            this.button_surrender.Location = new System.Drawing.Point(1275, 720);
            this.button_surrender.Margin = new System.Windows.Forms.Padding(2);
            this.button_surrender.Name = "button_surrender";
            this.button_surrender.NormalColor = System.Drawing.Color.DarkGoldenrod;
            this.button_surrender.Size = new System.Drawing.Size(100, 50);
            this.button_surrender.TabIndex = 3;
            this.button_surrender.TextColor = System.Drawing.Color.White;
            this.button_surrender.Click += new System.EventHandler(this.button_surrender_Click);
            // 
            // button_draw
            // 
            this.button_draw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_draw.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.button_draw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_draw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.button_draw.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button_draw.DownColor = System.Drawing.Color.DarkOliveGreen;
            this.button_draw.EnterColor = System.Drawing.Color.Olive;
            this.button_draw.Location = new System.Drawing.Point(1275, 620);
            this.button_draw.Margin = new System.Windows.Forms.Padding(2);
            this.button_draw.Name = "button_draw";
            this.button_draw.NormalColor = System.Drawing.Color.DarkGoldenrod;
            this.button_draw.Size = new System.Drawing.Size(100, 50);
            this.button_draw.TabIndex = 2;
            this.button_draw.TextColor = System.Drawing.Color.White;
            this.button_draw.Click += new System.EventHandler(this.button_draw_Click);
            // 
            // button_ready
            // 
            this.button_ready.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ready.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.button_ready.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_ready.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.button_ready.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button_ready.DownColor = System.Drawing.Color.DarkOliveGreen;
            this.button_ready.EnterColor = System.Drawing.Color.Olive;
            this.button_ready.Location = new System.Drawing.Point(1275, 520);
            this.button_ready.Margin = new System.Windows.Forms.Padding(2);
            this.button_ready.Name = "button_ready";
            this.button_ready.NormalColor = System.Drawing.Color.DarkGoldenrod;
            this.button_ready.Size = new System.Drawing.Size(100, 50);
            this.button_ready.TabIndex = 1;
            this.button_ready.TextColor = System.Drawing.Color.White;
            this.button_ready.Click += new System.EventHandler(this.button_ready_Click);
            // 
            // chessboard_container
            // 
            this.chessboard_container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chessboard_container.Location = new System.Drawing.Point(418, 25);
            this.chessboard_container.Name = "chessboard_container";
            this.chessboard_container.Size = new System.Drawing.Size(720, 800);
            this.chessboard_container.TabIndex = 0;
            // 
            // play_book
            // 
            this.play_book.BackColor = System.Drawing.Color.Transparent;
            this.play_book.Location = new System.Drawing.Point(1225, 25);
            this.play_book.Name = "play_book";
            this.play_book.Size = new System.Drawing.Size(200, 400);
            this.play_book.TabIndex = 9;
            // 
            // ChessLobby
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.BackgroundImage = global::platform.Properties.Resources.bgpic2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1540, 845);
            this.Controls.Add(this.play_book);
            this.Controls.Add(this.label_ready);
            this.Controls.Add(this.button_surrender);
            this.Controls.Add(this.button_draw);
            this.Controls.Add(this.button_ready);
            this.Controls.Add(this.chessboard_container);
            this.Name = "ChessLobby";
            this.Load += new System.EventHandler(this.ChessLobby_Load);
            this.Controls.SetChildIndex(this.chessboard_container, 0);
            this.Controls.SetChildIndex(this.button_ready, 0);
            this.Controls.SetChildIndex(this.button_draw, 0);
            this.Controls.SetChildIndex(this.button_surrender, 0);
            this.Controls.SetChildIndex(this.label_ready, 0);
            this.Controls.SetChildIndex(this.button_min, 0);
            this.Controls.SetChildIndex(this.button_exit, 0);
            this.Controls.SetChildIndex(this.play_book, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ChessboardContainer chessboard_container;
        private GlossyButton button_ready;
        private GlossyButton button_draw;
        private GlossyButton button_surrender;
        private System.Windows.Forms.Label label_ready;
        private PlayBook play_book;
    }
}