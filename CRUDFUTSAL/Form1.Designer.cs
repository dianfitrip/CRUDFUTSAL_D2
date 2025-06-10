namespace CRUDFUTSAL
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
            this.btnPenyewa = new System.Windows.Forms.Button();
            this.btnItemSewa = new System.Windows.Forms.Button();
            this.btnPeminjaman = new System.Windows.Forms.Button();
            this.btnPengembalian = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPenyewa
            // 
            this.btnPenyewa.Location = new System.Drawing.Point(36, 83);
            this.btnPenyewa.Name = "btnPenyewa";
            this.btnPenyewa.Size = new System.Drawing.Size(92, 53);
            this.btnPenyewa.TabIndex = 0;
            this.btnPenyewa.Text = "Penyewa";
            this.btnPenyewa.UseVisualStyleBackColor = true;
            this.btnPenyewa.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnItemSewa
            // 
            this.btnItemSewa.Location = new System.Drawing.Point(168, 83);
            this.btnItemSewa.Name = "btnItemSewa";
            this.btnItemSewa.Size = new System.Drawing.Size(84, 56);
            this.btnItemSewa.TabIndex = 1;
            this.btnItemSewa.Text = "Item Sewa";
            this.btnItemSewa.UseVisualStyleBackColor = true;
            this.btnItemSewa.Click += new System.EventHandler(this.btnItemSewa_Click);
            // 
            // btnPeminjaman
            // 
            this.btnPeminjaman.Location = new System.Drawing.Point(295, 82);
            this.btnPeminjaman.Name = "btnPeminjaman";
            this.btnPeminjaman.Size = new System.Drawing.Size(106, 54);
            this.btnPeminjaman.TabIndex = 2;
            this.btnPeminjaman.Text = "Peminjaman";
            this.btnPeminjaman.UseVisualStyleBackColor = true;
            this.btnPeminjaman.Click += new System.EventHandler(this.btnPeminjaman_Click);
            // 
            // btnPengembalian
            // 
            this.btnPengembalian.Location = new System.Drawing.Point(444, 83);
            this.btnPengembalian.Name = "btnPengembalian";
            this.btnPengembalian.Size = new System.Drawing.Size(105, 50);
            this.btnPengembalian.TabIndex = 3;
            this.btnPengembalian.Text = "Pengembalian";
            this.btnPengembalian.UseVisualStyleBackColor = true;
            this.btnPengembalian.Click += new System.EventHandler(this.btnPengembalian_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(326, 193);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 44);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(611, 83);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(87, 53);
            this.btnReport.TabIndex = 5;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPengembalian);
            this.Controls.Add(this.btnPeminjaman);
            this.Controls.Add(this.btnItemSewa);
            this.Controls.Add(this.btnPenyewa);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPenyewa;
        private System.Windows.Forms.Button btnItemSewa;
        private System.Windows.Forms.Button btnPeminjaman;
        private System.Windows.Forms.Button btnPengembalian;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReport;
    }
}

