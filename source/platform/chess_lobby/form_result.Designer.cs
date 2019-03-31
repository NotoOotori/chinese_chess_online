namespace platform.chess_lobby
{
    partial class FormResult
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
            this.label_title = new System.Windows.Forms.Label();
            this.label_player = new System.Windows.Forms.Label();
            this.label_opponent = new System.Windows.Forms.Label();
            this.label_result_player = new System.Windows.Forms.Label();
            this.label_result_opponent = new System.Windows.Forms.Label();
            this.label_elo_change_player = new System.Windows.Forms.Label();
            this.label_elo_change_opponent = new System.Windows.Forms.Label();
            this.glossyButton_confirm = new platform.GlossyButton();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("FZShuTi", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_title.ForeColor = System.Drawing.Color.Cyan;
            this.label_title.Location = new System.Drawing.Point(72, 24);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(129, 30);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "比赛结果";
            // 
            // label_player
            // 
            this.label_player.AutoSize = true;
            this.label_player.Font = new System.Drawing.Font("FZShuTi", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_player.ForeColor = System.Drawing.Color.Cyan;
            this.label_player.Location = new System.Drawing.Point(12, 82);
            this.label_player.Name = "label_player";
            this.label_player.Size = new System.Drawing.Size(71, 30);
            this.label_player.TabIndex = 1;
            this.label_player.Text = "玩家";
            // 
            // label_opponent
            // 
            this.label_opponent.AutoSize = true;
            this.label_opponent.Font = new System.Drawing.Font("FZShuTi", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_opponent.ForeColor = System.Drawing.Color.Cyan;
            this.label_opponent.Location = new System.Drawing.Point(12, 145);
            this.label_opponent.Name = "label_opponent";
            this.label_opponent.Size = new System.Drawing.Size(71, 30);
            this.label_opponent.TabIndex = 2;
            this.label_opponent.Text = "对手";
            // 
            // label_result_player
            // 
            this.label_result_player.AutoSize = true;
            this.label_result_player.Font = new System.Drawing.Font("FZShuTi", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_result_player.ForeColor = System.Drawing.Color.Red;
            this.label_result_player.Location = new System.Drawing.Point(114, 82);
            this.label_result_player.Name = "label_result_player";
            this.label_result_player.Size = new System.Drawing.Size(42, 30);
            this.label_result_player.TabIndex = 3;
            this.label_result_player.Text = "胜";
            // 
            // label_result_opponent
            // 
            this.label_result_opponent.AutoSize = true;
            this.label_result_opponent.Font = new System.Drawing.Font("FZShuTi", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_result_opponent.ForeColor = System.Drawing.Color.Lime;
            this.label_result_opponent.Location = new System.Drawing.Point(114, 145);
            this.label_result_opponent.Name = "label_result_opponent";
            this.label_result_opponent.Size = new System.Drawing.Size(42, 30);
            this.label_result_opponent.TabIndex = 4;
            this.label_result_opponent.Text = "负";
            // 
            // label_elo_change_player
            // 
            this.label_elo_change_player.AutoSize = true;
            this.label_elo_change_player.Font = new System.Drawing.Font("FZShuTi", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_elo_change_player.ForeColor = System.Drawing.Color.Red;
            this.label_elo_change_player.Location = new System.Drawing.Point(199, 82);
            this.label_elo_change_player.Name = "label_elo_change_player";
            this.label_elo_change_player.Size = new System.Drawing.Size(56, 30);
            this.label_elo_change_player.TabIndex = 5;
            this.label_elo_change_player.Text = "+10";
            // 
            // label_elo_change_opponent
            // 
            this.label_elo_change_opponent.AutoSize = true;
            this.label_elo_change_opponent.Font = new System.Drawing.Font("FZShuTi", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_elo_change_opponent.ForeColor = System.Drawing.Color.Lime;
            this.label_elo_change_opponent.Location = new System.Drawing.Point(200, 145);
            this.label_elo_change_opponent.Name = "label_elo_change_opponent";
            this.label_elo_change_opponent.Size = new System.Drawing.Size(55, 30);
            this.label_elo_change_opponent.TabIndex = 6;
            this.label_elo_change_opponent.Text = "-10";
            // 
            // glossyButton_confirm
            // 
            this.glossyButton_confirm.BackColor = System.Drawing.Color.Orange;
            this.glossyButton_confirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.glossyButton_confirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.glossyButton_confirm.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.glossyButton_confirm.DownColor = System.Drawing.Color.Blue;
            this.glossyButton_confirm.EnterColor = System.Drawing.Color.Pink;
            this.glossyButton_confirm.Location = new System.Drawing.Point(77, 196);
            this.glossyButton_confirm.Margin = new System.Windows.Forms.Padding(2);
            this.glossyButton_confirm.Name = "glossyButton_confirm";
            this.glossyButton_confirm.NormalColor = System.Drawing.Color.Orange;
            this.glossyButton_confirm.Size = new System.Drawing.Size(124, 44);
            this.glossyButton_confirm.TabIndex = 7;
            // 
            // FormResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(267, 261);
            this.Controls.Add(this.glossyButton_confirm);
            this.Controls.Add(this.label_elo_change_opponent);
            this.Controls.Add(this.label_elo_change_player);
            this.Controls.Add(this.label_result_opponent);
            this.Controls.Add(this.label_result_player);
            this.Controls.Add(this.label_opponent);
            this.Controls.Add(this.label_player);
            this.Controls.Add(this.label_title);
            this.Name = "FormResult";
            this.Text = "Result";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_player;
        private System.Windows.Forms.Label label_opponent;
        private System.Windows.Forms.Label label_result_player;
        private System.Windows.Forms.Label label_result_opponent;
        private System.Windows.Forms.Label label_elo_change_player;
        private System.Windows.Forms.Label label_elo_change_opponent;
        private GlossyButton glossyButton_confirm;
    }
}