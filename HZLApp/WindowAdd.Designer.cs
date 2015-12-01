namespace HZLApp
{
    partial class WindowAdd
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btngdc = new System.Windows.Forms.Button();
            this.btnpkm = new System.Windows.Forms.Button();
            this.btntlm = new System.Windows.Forms.Button();
            this.btnpkc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btntlc = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btngdc, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnpkm, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btntlm, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnpkc, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btntlc, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(778, 357);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btngdc
            // 
            this.btngdc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngdc.Location = new System.Drawing.Point(3, 252);
            this.btngdc.Name = "btngdc";
            this.btngdc.Size = new System.Drawing.Size(383, 102);
            this.btngdc.TabIndex = 6;
            this.btngdc.Text = "固定窗";
            this.btngdc.UseVisualStyleBackColor = true;
            this.btngdc.Click += new System.EventHandler(this.btngdc_Click);
            // 
            // btnpkm
            // 
            this.btnpkm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnpkm.Location = new System.Drawing.Point(392, 145);
            this.btnpkm.Name = "btnpkm";
            this.btnpkm.Size = new System.Drawing.Size(383, 101);
            this.btnpkm.TabIndex = 5;
            this.btnpkm.Text = "平开门";
            this.btnpkm.UseVisualStyleBackColor = true;
            this.btnpkm.Click += new System.EventHandler(this.btnpkm_Click);
            // 
            // btntlm
            // 
            this.btntlm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btntlm.Location = new System.Drawing.Point(3, 145);
            this.btntlm.Name = "btntlm";
            this.btntlm.Size = new System.Drawing.Size(383, 101);
            this.btntlm.TabIndex = 4;
            this.btntlm.Text = "推拉门";
            this.btntlm.UseVisualStyleBackColor = true;
            this.btntlm.Click += new System.EventHandler(this.btntlm_Click);
            // 
            // btnpkc
            // 
            this.btnpkc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnpkc.Location = new System.Drawing.Point(392, 38);
            this.btnpkc.Name = "btnpkc";
            this.btnpkc.Size = new System.Drawing.Size(383, 101);
            this.btnpkc.TabIndex = 3;
            this.btnpkc.Text = "平开窗";
            this.btnpkc.UseVisualStyleBackColor = true;
            this.btnpkc.Click += new System.EventHandler(this.btnpkc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("SimSun", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(772, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择窗型";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btntlc
            // 
            this.btntlc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btntlc.Location = new System.Drawing.Point(3, 38);
            this.btntlc.Name = "btntlc";
            this.btntlc.Size = new System.Drawing.Size(383, 101);
            this.btntlc.TabIndex = 2;
            this.btntlc.Text = "推拉窗";
            this.btntlc.UseVisualStyleBackColor = true;
            this.btntlc.Click += new System.EventHandler(this.btntlc_Click);
            // 
            // WindowAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 357);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "WindowAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加窗体";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btntlc;
        private System.Windows.Forms.Button btngdc;
        private System.Windows.Forms.Button btnpkm;
        private System.Windows.Forms.Button btntlm;
        private System.Windows.Forms.Button btnpkc;
    }
}