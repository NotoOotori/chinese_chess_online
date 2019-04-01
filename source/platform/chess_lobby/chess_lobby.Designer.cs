﻿namespace platform.chess_lobby
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
            this.chessboard_container = new platform.chess_lobby.ChessboardContainer();
            this.button_ready = new platform.GlossyButton();
            this.button_draw = new platform.GlossyButton();
            this.button_surrender = new platform.GlossyButton();
            this.label_ready = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chessboard_container
            // 
            this.chessboard_container.Location = new System.Drawing.Point(555, 60);
            this.chessboard_container.Name = "chessboard_container";
            this.chessboard_container.Size = new System.Drawing.Size(810, 900);
            this.chessboard_container.TabIndex = 0;
            // 
            // button_ready
            // 
            this.button_ready.BackColor = System.Drawing.Color.Orange;
            this.button_ready.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_ready.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.button_ready.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button_ready.DownColor = System.Drawing.Color.Blue;
            this.button_ready.EnterColor = System.Drawing.Color.Pink;
            this.button_ready.Location = new System.Drawing.Point(1450, 580);
            this.button_ready.Margin = new System.Windows.Forms.Padding(2);
            this.button_ready.Name = "button_ready";
            this.button_ready.NormalColor = System.Drawing.Color.Orange;
            this.button_ready.Size = new System.Drawing.Size(100, 50);
            this.button_ready.TabIndex = 1;
            this.button_ready.Click += new System.EventHandler(this.button_ready_Click);
            // 
            // button_draw
            // 
            this.button_draw.BackColor = System.Drawing.Color.Orange;
            this.button_draw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_draw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.button_draw.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button_draw.DownColor = System.Drawing.Color.Blue;
            this.button_draw.EnterColor = System.Drawing.Color.Pink;
            this.button_draw.Location = new System.Drawing.Point(1450, 680);
            this.button_draw.Margin = new System.Windows.Forms.Padding(2);
            this.button_draw.Name = "button_draw";
            this.button_draw.NormalColor = System.Drawing.Color.Orange;
            this.button_draw.Size = new System.Drawing.Size(100, 50);
            this.button_draw.TabIndex = 2;
            this.button_draw.Click += new System.EventHandler(this.button_draw_Click);
            // 
            // button_surrender
            // 
            this.button_surrender.BackColor = System.Drawing.Color.Orange;
            this.button_surrender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_surrender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.button_surrender.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button_surrender.DownColor = System.Drawing.Color.Blue;
            this.button_surrender.EnterColor = System.Drawing.Color.Pink;
            this.button_surrender.Location = new System.Drawing.Point(1450, 780);
            this.button_surrender.Margin = new System.Windows.Forms.Padding(2);
            this.button_surrender.Name = "button_surrender";
            this.button_surrender.NormalColor = System.Drawing.Color.Orange;
            this.button_surrender.Size = new System.Drawing.Size(100, 50);
            this.button_surrender.TabIndex = 3;
            this.button_surrender.Click += new System.EventHandler(this.button_surrender_Click);
            // 
            // label_ready
            // 
            this.label_ready.AutoSize = true;
            this.label_ready.BackColor = System.Drawing.Color.Transparent;
            this.label_ready.Font = new System.Drawing.Font("FZShuTi", 75F);
            this.label_ready.ForeColor = System.Drawing.Color.Cyan;
            this.label_ready.Location = new System.Drawing.Point(80, 675);
            this.label_ready.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_ready.Name = "label_ready";
            this.label_ready.Size = new System.Drawing.Size(445, 105);
            this.label_ready.TabIndex = 5;
            this.label_ready.Text = "准备就绪";
            this.label_ready.Visible = false;
            // 
            // ChessLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.label_ready);
            this.Controls.Add(this.button_surrender);
            this.Controls.Add(this.button_draw);
            this.Controls.Add(this.button_ready);
            this.Controls.Add(this.chessboard_container);
            this.Name = "ChessLobby";
            this.Text = "chess_lobby";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ChessboardContainer chessboard_container;
        private GlossyButton button_ready;
        private GlossyButton button_draw;
        private GlossyButton button_surrender;
        private System.Windows.Forms.Label label_ready;
    }
}