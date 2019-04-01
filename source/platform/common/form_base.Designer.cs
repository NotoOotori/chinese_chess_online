namespace platform.common
{
    partial class FormBase
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
            this.picture_box_icon = new System.Windows.Forms.PictureBox();
            this.button_exit = new System.Windows.Forms.Button();
            this.button_min = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picture_box_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("SimSun", 12F);
            this.label_title.ForeColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(50, 7);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(80, 16);
            this.label_title.TabIndex = 8;
            this.label_title.Text = "form_base";
            // 
            // picture_box_icon
            // 
            this.picture_box_icon.BackColor = System.Drawing.Color.Transparent;
            this.picture_box_icon.Image = global::platform.Properties.Resources.chess_icon1;
            this.picture_box_icon.InitialImage = null;
            this.picture_box_icon.Location = new System.Drawing.Point(0, 0);
            this.picture_box_icon.Name = "picture_box_icon";
            this.picture_box_icon.Size = new System.Drawing.Size(32, 32);
            this.picture_box_icon.TabIndex = 7;
            this.picture_box_icon.TabStop = false;
            // 
            // button_exit
            // 
            this.button_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_exit.BackColor = System.Drawing.Color.Transparent;
            this.button_exit.FlatAppearance.BorderSize = 0;
            this.button_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_exit.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_exit.ForeColor = System.Drawing.Color.White;
            this.button_exit.Location = new System.Drawing.Point(755, 0);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(46, 41);
            this.button_exit.TabIndex = 6;
            this.button_exit.Text = "X";
            this.button_exit.UseVisualStyleBackColor = false;
            // 
            // button_min
            // 
            this.button_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_min.BackColor = System.Drawing.Color.Transparent;
            this.button_min.FlatAppearance.BorderSize = 0;
            this.button_min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_min.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_min.ForeColor = System.Drawing.Color.White;
            this.button_min.Location = new System.Drawing.Point(704, 0);
            this.button_min.Name = "button_min";
            this.button_min.Size = new System.Drawing.Size(46, 41);
            this.button_min.TabIndex = 5;
            this.button_min.Text = "---";
            this.button_min.UseVisualStyleBackColor = false;
            // 
            // FormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.picture_box_icon);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_min);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBase";
            this.Text = "form_base";
            this.Load += new System.EventHandler(this.FormBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picture_box_icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.PictureBox picture_box_icon;
        protected System.Windows.Forms.Button button_exit;
        protected System.Windows.Forms.Button button_min;
    }
}