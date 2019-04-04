namespace platform.chess_lobby
{
    partial class PlayBook
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_title = new System.Windows.Forms.Label();
            this.panel_play_book = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("FZShuTi", 24F);
            this.label_title.Location = new System.Drawing.Point(28, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(143, 34);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "棋局过程";
            // 
            // panel_play_book
            // 
            this.panel_play_book.AutoScroll = true;
            this.panel_play_book.BackColor = System.Drawing.Color.Transparent;
            this.panel_play_book.Location = new System.Drawing.Point(0, 49);
            this.panel_play_book.Name = "panel_play_book";
            this.panel_play_book.Size = new System.Drawing.Size(200, 351);
            this.panel_play_book.TabIndex = 1;
            // 
            // PlayBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel_play_book);
            this.Controls.Add(this.label_title);
            this.Name = "PlayBook";
            this.Size = new System.Drawing.Size(200, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Panel panel_play_book;
    }
}
