namespace platform.dating
{
    partial class FormDating
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_renew = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // button_renew
            // 
            this.button_renew.BackColor = System.Drawing.Color.Transparent;
            this.button_renew.BackgroundImage = global::platform.Properties.Resources.refresh;
            this.button_renew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_renew.FlatAppearance.BorderSize = 0;
            this.button_renew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_renew.ForeColor = System.Drawing.Color.Transparent;
            this.button_renew.Location = new System.Drawing.Point(653, 0);
            this.button_renew.Margin = new System.Windows.Forms.Padding(2);
            this.button_renew.Name = "button_renew";
            this.button_renew.Size = new System.Drawing.Size(46, 41);
            this.button_renew.TabIndex = 9;
            this.button_renew.UseVisualStyleBackColor = false;
            this.button_renew.Click += new System.EventHandler(this.button_renew_Click);
            this.button_renew.MouseHover += new System.EventHandler(this.button_renew_MouseHover);
            // 
            // FormDating
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::platform.Properties.Resources.bgpic5;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_renew);
            this.Name = "FormDating";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.button_renew, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_renew;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

