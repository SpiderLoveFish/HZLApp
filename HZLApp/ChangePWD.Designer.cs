namespace HZLApp
{
    partial class ChangePWD
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
            this.txtoldpwd = new System.Windows.Forms.TextBox();
            this.txtpwd = new System.Windows.Forms.TextBox();
            this.txttpwd = new System.Windows.Forms.TextBox();
            this.btnqd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtoldpwd
            // 
            this.txtoldpwd.Location = new System.Drawing.Point(147, 36);
            this.txtoldpwd.Name = "txtoldpwd";
            this.txtoldpwd.Size = new System.Drawing.Size(100, 21);
            this.txtoldpwd.TabIndex = 0;
            this.txtoldpwd.UseSystemPasswordChar = true;
            // 
            // txtpwd
            // 
            this.txtpwd.Location = new System.Drawing.Point(147, 91);
            this.txtpwd.Name = "txtpwd";
            this.txtpwd.PasswordChar = '*';
            this.txtpwd.Size = new System.Drawing.Size(100, 21);
            this.txtpwd.TabIndex = 1;
            // 
            // txttpwd
            // 
            this.txttpwd.Location = new System.Drawing.Point(147, 147);
            this.txttpwd.Name = "txttpwd";
            this.txttpwd.PasswordChar = '*';
            this.txttpwd.Size = new System.Drawing.Size(100, 21);
            this.txttpwd.TabIndex = 2;
            // 
            // btnqd
            // 
            this.btnqd.Location = new System.Drawing.Point(75, 218);
            this.btnqd.Name = "btnqd";
            this.btnqd.Size = new System.Drawing.Size(75, 23);
            this.btnqd.TabIndex = 4;
            this.btnqd.Text = "确定";
            this.btnqd.UseVisualStyleBackColor = true;
            this.btnqd.Click += new System.EventHandler(this.btnqd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "原密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "新密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "新密码：";
            // 
            // ChangePWD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnqd);
            this.Controls.Add(this.txttpwd);
            this.Controls.Add(this.txtpwd);
            this.Controls.Add(this.txtoldpwd);
            this.Name = "ChangePWD";
            this.Text = "修改密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtoldpwd;
        private System.Windows.Forms.TextBox txtpwd;
        private System.Windows.Forms.TextBox txttpwd;
        private System.Windows.Forms.Button btnqd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}