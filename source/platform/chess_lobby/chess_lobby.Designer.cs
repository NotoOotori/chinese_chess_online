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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessLobby));
            this.chessboard = new platform.chess_lobby.ChessboardContainer();
            this.SuspendLayout();
            // 
            // chessboard
            // 
            this.chessboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chessboard.BackgroundImage")));
            this.chessboard.Location = new System.Drawing.Point(0, 0);
            this.chessboard.Name = "chessboard";
            this.chessboard.Size = new System.Drawing.Size(520, 576);
            this.chessboard.TabIndex = 0;
            // 
            // ChessLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 845);
            this.Controls.Add(this.chessboard);
            this.Name = "ChessLobby";
            this.Text = "chess_lobby";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private ChessboardContainer chessboard;
    }
}