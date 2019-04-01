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
            this.chessboard_container = new platform.chess_lobby.ChessboardContainer();
            this.button_ready = new System.Windows.Forms.Button();
            this.button_draw = new System.Windows.Forms.Button();
            this.button_surrender = new System.Windows.Forms.Button();
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
            this.button_ready.Location = new System.Drawing.Point(600, 980);
            this.button_ready.Name = "button_ready";
            this.button_ready.Size = new System.Drawing.Size(100, 50);
            this.button_ready.TabIndex = 1;
            this.button_ready.Text = "Ready";
            this.button_ready.UseVisualStyleBackColor = true;
            this.button_ready.Click += new System.EventHandler(this.button_ready_Click);
            // 
            // button_draw
            // 
            this.button_draw.Location = new System.Drawing.Point(750, 980);
            this.button_draw.Name = "button_draw";
            this.button_draw.Size = new System.Drawing.Size(100, 50);
            this.button_draw.TabIndex = 2;
            this.button_draw.Text = "Draw";
            this.button_draw.UseVisualStyleBackColor = true;
            this.button_draw.Click += new System.EventHandler(this.button_draw_Click);
            // 
            // button_surrender
            // 
            this.button_surrender.Location = new System.Drawing.Point(900, 980);
            this.button_surrender.Name = "button_surrender";
            this.button_surrender.Size = new System.Drawing.Size(100, 50);
            this.button_surrender.TabIndex = 3;
            this.button_surrender.Text = "Surrender";
            this.button_surrender.UseVisualStyleBackColor = true;
            this.button_surrender.Click += new System.EventHandler(this.button_surrender_Click);
            // 
            // ChessLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.button_surrender);
            this.Controls.Add(this.button_draw);
            this.Controls.Add(this.button_ready);
            this.Controls.Add(this.chessboard_container);
            this.Name = "ChessLobby";
            this.Text = "chess_lobby";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private ChessboardContainer chessboard_container;
        private System.Windows.Forms.Button button_ready;
        private System.Windows.Forms.Button button_draw;
        private System.Windows.Forms.Button button_surrender;
    }
}