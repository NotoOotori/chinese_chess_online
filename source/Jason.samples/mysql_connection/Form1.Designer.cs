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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.data_grid_view_adapter = new System.Windows.Forms.DataGridView();
            this.button_adapter_snum = new System.Windows.Forms.Button();
            this.text_box_adapter_snum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_grid_view_adapter)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.data_grid_view_adapter);
            this.groupBox1.Controls.Add(this.button_adapter_snum);
            this.groupBox1.Controls.Add(this.text_box_adapter_snum);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 253);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "adapter";
            // 
            // data_grid_view_adapter
            // 
            this.data_grid_view_adapter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_grid_view_adapter.Location = new System.Drawing.Point(257, 21);
            this.data_grid_view_adapter.Name = "data_grid_view_adapter";
            this.data_grid_view_adapter.RowTemplate.Height = 27;
            this.data_grid_view_adapter.Size = new System.Drawing.Size(248, 205);
            this.data_grid_view_adapter.TabIndex = 13;
            // 
            // button_adapter_snum
            // 
            this.button_adapter_snum.Font = new System.Drawing.Font("SimHei", 15F);
            this.button_adapter_snum.Location = new System.Drawing.Point(35, 154);
            this.button_adapter_snum.Name = "button_adapter_snum";
            this.button_adapter_snum.Size = new System.Drawing.Size(150, 50);
            this.button_adapter_snum.TabIndex = 12;
            this.button_adapter_snum.Text = "检索";
            this.button_adapter_snum.UseVisualStyleBackColor = true;
            this.button_adapter_snum.Click += new System.EventHandler(this.button_adapter_snum_Click);
            // 
            // text_box_adapter_snum
            // 
            this.text_box_adapter_snum.Location = new System.Drawing.Point(35, 85);
            this.text_box_adapter_snum.Name = "text_box_adapter_snum";
            this.text_box_adapter_snum.Size = new System.Drawing.Size(150, 25);
            this.text_box_adapter_snum.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimHei", 15F);
            this.label2.Location = new System.Drawing.Point(30, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "请输入学号:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_grid_view_adapter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView data_grid_view_adapter;
        private System.Windows.Forms.Button button_adapter_snum;
        private System.Windows.Forms.TextBox text_box_adapter_snum;
        private System.Windows.Forms.Label label2;
    }
}

