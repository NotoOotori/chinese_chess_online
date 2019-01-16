namespace mysql_connection
{
    partial class Form1
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
            this.label_sname = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.data_grid_view = new System.Windows.Forms.DataGridView();
            this.button_snum = new System.Windows.Forms.Button();
            this.text_box_snum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label_avg = new System.Windows.Forms.Label();
            this.label_ccnt = new System.Windows.Forms.Label();
            this.label_fcnt = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.data_grid_view)).BeginInit();
            this.SuspendLayout();
            // 
            // label_sname
            // 
            this.label_sname.AutoSize = true;
            this.label_sname.Font = new System.Drawing.Font("SimHei", 15F);
            this.label_sname.Location = new System.Drawing.Point(232, 9);
            this.label_sname.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_sname.MaximumSize = new System.Drawing.Size(90, 0);
            this.label_sname.MinimumSize = new System.Drawing.Size(90, 0);
            this.label_sname.Name = "label_sname";
            this.label_sname.Size = new System.Drawing.Size(90, 25);
            this.label_sname.TabIndex = 21;
            this.label_sname.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimHei", 15F);
            this.label3.Location = new System.Drawing.Point(317, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 25);
            this.label3.TabIndex = 20;
            this.label3.Text = "选修课程成绩";
            // 
            // data_grid_view
            // 
            this.data_grid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_grid_view.Location = new System.Drawing.Point(237, 37);
            this.data_grid_view.Name = "data_grid_view";
            this.data_grid_view.RowTemplate.Height = 27;
            this.data_grid_view.Size = new System.Drawing.Size(248, 191);
            this.data_grid_view.TabIndex = 19;
            // 
            // button_snum
            // 
            this.button_snum.Font = new System.Drawing.Font("SimHei", 15F);
            this.button_snum.Location = new System.Drawing.Point(19, 138);
            this.button_snum.Name = "button_snum";
            this.button_snum.Size = new System.Drawing.Size(150, 50);
            this.button_snum.TabIndex = 18;
            this.button_snum.Text = "检索";
            this.button_snum.UseVisualStyleBackColor = true;
            this.button_snum.Click += new System.EventHandler(this.button_snum_Click);
            // 
            // text_box_snum
            // 
            this.text_box_snum.Location = new System.Drawing.Point(19, 69);
            this.text_box_snum.Name = "text_box_snum";
            this.text_box_snum.Size = new System.Drawing.Size(150, 25);
            this.text_box_snum.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimHei", 15F);
            this.label2.Location = new System.Drawing.Point(14, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "请输入学号:";
            // 
            // label_avg
            // 
            this.label_avg.AutoSize = true;
            this.label_avg.Font = new System.Drawing.Font("SimHei", 12F);
            this.label_avg.Location = new System.Drawing.Point(642, 63);
            this.label_avg.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_avg.MaximumSize = new System.Drawing.Size(75, 0);
            this.label_avg.MinimumSize = new System.Drawing.Size(75, 0);
            this.label_avg.Name = "label_avg";
            this.label_avg.Size = new System.Drawing.Size(75, 20);
            this.label_avg.TabIndex = 22;
            // 
            // label_ccnt
            // 
            this.label_ccnt.AutoSize = true;
            this.label_ccnt.Font = new System.Drawing.Font("SimHei", 12F);
            this.label_ccnt.Location = new System.Drawing.Point(642, 94);
            this.label_ccnt.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_ccnt.MaximumSize = new System.Drawing.Size(75, 0);
            this.label_ccnt.MinimumSize = new System.Drawing.Size(75, 0);
            this.label_ccnt.Name = "label_ccnt";
            this.label_ccnt.Size = new System.Drawing.Size(75, 20);
            this.label_ccnt.TabIndex = 23;
            // 
            // label_fcnt
            // 
            this.label_fcnt.AutoSize = true;
            this.label_fcnt.Font = new System.Drawing.Font("SimHei", 12F);
            this.label_fcnt.Location = new System.Drawing.Point(642, 125);
            this.label_fcnt.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_fcnt.MaximumSize = new System.Drawing.Size(75, 0);
            this.label_fcnt.MinimumSize = new System.Drawing.Size(75, 0);
            this.label_fcnt.Name = "label_fcnt";
            this.label_fcnt.Size = new System.Drawing.Size(75, 20);
            this.label_fcnt.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimHei", 12F);
            this.label6.Location = new System.Drawing.Point(542, 63);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.MaximumSize = new System.Drawing.Size(90, 0);
            this.label6.MinimumSize = new System.Drawing.Size(90, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "平均分";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimHei", 12F);
            this.label7.Location = new System.Drawing.Point(542, 94);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.MaximumSize = new System.Drawing.Size(90, 0);
            this.label7.MinimumSize = new System.Drawing.Size(90, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 20);
            this.label7.TabIndex = 26;
            this.label7.Text = "总门数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimHei", 12F);
            this.label1.Location = new System.Drawing.Point(542, 125);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.MaximumSize = new System.Drawing.Size(90, 0);
            this.label1.MinimumSize = new System.Drawing.Size(90, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "挂科数";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 240);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label_fcnt);
            this.Controls.Add(this.label_ccnt);
            this.Controls.Add(this.label_avg);
            this.Controls.Add(this.label_sname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.data_grid_view);
            this.Controls.Add(this.button_snum);
            this.Controls.Add(this.text_box_snum);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.data_grid_view)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_sname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView data_grid_view;
        private System.Windows.Forms.Button button_snum;
        private System.Windows.Forms.TextBox text_box_snum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_avg;
        private System.Windows.Forms.Label label_ccnt;
        private System.Windows.Forms.Label label_fcnt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
    }
}

