namespace platform.dating
{
    partial class FormInfo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_email = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.user_avatar = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_ratio = new System.Windows.Forms.Label();
            this.label_elo = new System.Windows.Forms.Label();
            this.label_none = new System.Windows.Forms.Label();
            this.label_lose = new System.Windows.Forms.Label();
            this.label_win = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_level = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.user_avatar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_exit
            // 
            this.button_exit.FlatAppearance.BorderSize = 0;
            this.button_exit.Location = new System.Drawing.Point(703, 0);
            // 
            // button_min
            // 
            this.button_min.FlatAppearance.BorderSize = 0;
            this.button_min.Location = new System.Drawing.Point(652, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_email);
            this.groupBox1.Controls.Add(this.label_name);
            this.groupBox1.Controls.Add(this.user_avatar);
            this.groupBox1.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(24, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 310);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "个人信息";
            // 
            // label_email
            // 
            this.label_email.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_email.Location = new System.Drawing.Point(7, 258);
            this.label_email.Name = "label_email";
            this.label_email.Size = new System.Drawing.Size(180, 25);
            this.label_email.TabIndex = 2;
            this.label_email.Text = "label2";
            this.label_email.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_name
            // 
            this.label_name.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_name.Location = new System.Drawing.Point(7, 214);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(180, 25);
            this.label_name.TabIndex = 1;
            this.label_name.Text = "label1";
            this.label_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // user_avatar
            // 
            this.user_avatar.Location = new System.Drawing.Point(19, 35);
            this.user_avatar.Name = "user_avatar";
            this.user_avatar.Size = new System.Drawing.Size(160, 160);
            this.user_avatar.TabIndex = 0;
            this.user_avatar.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_level);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label_ratio);
            this.groupBox2.Controls.Add(this.label_elo);
            this.groupBox2.Controls.Add(this.label_none);
            this.groupBox2.Controls.Add(this.label_lose);
            this.groupBox2.Controls.Add(this.label_win);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(256, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(458, 135);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据统计";
            // 
            // label_ratio
            // 
            this.label_ratio.AutoSize = true;
            this.label_ratio.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ratio.ForeColor = System.Drawing.Color.Navy;
            this.label_ratio.Location = new System.Drawing.Point(384, 38);
            this.label_ratio.Name = "label_ratio";
            this.label_ratio.Size = new System.Drawing.Size(38, 28);
            this.label_ratio.TabIndex = 12;
            this.label_ratio.Text = "胜";
            this.label_ratio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_elo
            // 
            this.label_elo.AutoSize = true;
            this.label_elo.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_elo.ForeColor = System.Drawing.Color.Navy;
            this.label_elo.Location = new System.Drawing.Point(106, 38);
            this.label_elo.Name = "label_elo";
            this.label_elo.Size = new System.Drawing.Size(38, 28);
            this.label_elo.TabIndex = 11;
            this.label_elo.Text = "胜";
            this.label_elo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_none
            // 
            this.label_none.AutoSize = true;
            this.label_none.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_none.ForeColor = System.Drawing.Color.Navy;
            this.label_none.Location = new System.Drawing.Point(406, 78);
            this.label_none.Name = "label_none";
            this.label_none.Size = new System.Drawing.Size(38, 28);
            this.label_none.TabIndex = 10;
            this.label_none.Text = "胜";
            this.label_none.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_lose
            // 
            this.label_lose.AutoSize = true;
            this.label_lose.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_lose.ForeColor = System.Drawing.Color.Navy;
            this.label_lose.Location = new System.Drawing.Point(243, 78);
            this.label_lose.Name = "label_lose";
            this.label_lose.Size = new System.Drawing.Size(38, 28);
            this.label_lose.TabIndex = 9;
            this.label_lose.Text = "胜";
            this.label_lose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_win
            // 
            this.label_win.AutoSize = true;
            this.label_win.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_win.ForeColor = System.Drawing.Color.Navy;
            this.label_win.Location = new System.Drawing.Point(80, 78);
            this.label_win.Name = "label_win";
            this.label_win.Size = new System.Drawing.Size(38, 28);
            this.label_win.TabIndex = 8;
            this.label_win.Text = "胜";
            this.label_win.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Purple;
            this.label5.Location = new System.Drawing.Point(318, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 28);
            this.label5.TabIndex = 7;
            this.label5.Text = "胜率";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Purple;
            this.label4.Location = new System.Drawing.Point(22, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "等级分";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Purple;
            this.label3.Location = new System.Drawing.Point(356, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "平";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Purple;
            this.label2.Location = new System.Drawing.Point(193, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "负";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Purple;
            this.label1.Location = new System.Drawing.Point(30, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "胜";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(256, 204);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(458, 174);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "历史交手";
            // 
            // label_level
            // 
            this.label_level.AutoSize = true;
            this.label_level.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_level.ForeColor = System.Drawing.Color.Navy;
            this.label_level.Location = new System.Drawing.Point(245, 38);
            this.label_level.Name = "label_level";
            this.label_level.Size = new System.Drawing.Size(38, 28);
            this.label_level.TabIndex = 14;
            this.label_level.Text = "胜";
            this.label_level.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Purple;
            this.label7.Location = new System.Drawing.Point(178, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 28);
            this.label7.TabIndex = 13;
            this.label7.Text = "等级";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(748, 420);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormInfo";
            this.Load += new System.EventHandler(this.form_info_Load);
            this.Controls.SetChildIndex(this.button_min, 0);
            this.Controls.SetChildIndex(this.button_exit, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.user_avatar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_email;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.PictureBox user_avatar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_ratio;
        private System.Windows.Forms.Label label_elo;
        private System.Windows.Forms.Label label_none;
        private System.Windows.Forms.Label label_lose;
        private System.Windows.Forms.Label label_win;
        private System.Windows.Forms.Label label_level;
        private System.Windows.Forms.Label label7;
    }
}