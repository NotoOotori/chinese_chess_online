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
            this.SuspendLayout();
            // 
            // chessboard_container
            // 
            this.chessboard_container.Location = new System.Drawing.Point(0, 0);
            this.chessboard_container.Name = "chessboard_container";
            this.chessboard_container.Size = new System.Drawing.Size(810, 900);
            this.chessboard_container.TabIndex = 0;
            // 
            // ChessLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 845);
            this.Controls.Add(this.chessboard_container);
            this.Name = "ChessLobby";
            this.Text = "chess_lobby";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private ChessboardContainer chessboard_container;
    }
}