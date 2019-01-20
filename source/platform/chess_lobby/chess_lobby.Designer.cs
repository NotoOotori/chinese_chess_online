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
            this.chessboard1 = new platform.chess_lobby.Chessboard();
            this.SuspendLayout();
            // 
            // chessboard1
            // 
            this.chessboard1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chessboard1.BackgroundImage")));
            this.chessboard1.Location = new System.Drawing.Point(0, 0);
            this.chessboard1.Name = "chessboard1";
            this.chessboard1.Size = new System.Drawing.Size(520, 576);
            this.chessboard1.TabIndex = 0;
            // 
            // ChessLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 845);
            this.Controls.Add(this.chessboard1);
            this.Name = "ChessLobby";
            this.Text = "chess_lobby";
            this.ResumeLayout(false);

        }

        #endregion

        private Chessboard chessboard1;
    }
}