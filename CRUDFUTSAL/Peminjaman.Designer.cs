namespace CRUDFUTSAL
{
    partial class Peminjaman
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvPeminjaman = new System.Windows.Forms.DataGridView();
            this.txtIdPenyewa = new System.Windows.Forms.TextBox();
            this.txtTanggalPinjam = new System.Windows.Forms.TextBox();
            this.txtTanggalJatuhTempo = new System.Windows.Forms.TextBox();
            this.txtTotalHarga = new System.Windows.Forms.TextBox();
            this.txtIdItem = new System.Windows.Forms.TextBox();
            this.combobox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeminjaman)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Penyewa";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID Item";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "tanggal pinjam";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tanggal jatuh Tempo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Total Harga";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Status";
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(554, 51);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 12;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(554, 80);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 23);
            this.btnHapus.TabIndex = 13;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(554, 109);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(554, 138);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvPeminjaman
            // 
            this.dgvPeminjaman.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeminjaman.Location = new System.Drawing.Point(39, 261);
            this.dgvPeminjaman.Name = "dgvPeminjaman";
            this.dgvPeminjaman.RowHeadersWidth = 51;
            this.dgvPeminjaman.RowTemplate.Height = 24;
            this.dgvPeminjaman.Size = new System.Drawing.Size(639, 150);
            this.dgvPeminjaman.TabIndex = 16;
            // 
            // txtIdPenyewa
            // 
            this.txtIdPenyewa.Location = new System.Drawing.Point(234, 44);
            this.txtIdPenyewa.Name = "txtIdPenyewa";
            this.txtIdPenyewa.Size = new System.Drawing.Size(100, 22);
            this.txtIdPenyewa.TabIndex = 17;
            // 
            // txtTanggalPinjam
            // 
            this.txtTanggalPinjam.Location = new System.Drawing.Point(234, 100);
            this.txtTanggalPinjam.Name = "txtTanggalPinjam";
            this.txtTanggalPinjam.Size = new System.Drawing.Size(100, 22);
            this.txtTanggalPinjam.TabIndex = 18;
            // 
            // txtTanggalJatuhTempo
            // 
            this.txtTanggalJatuhTempo.Location = new System.Drawing.Point(234, 128);
            this.txtTanggalJatuhTempo.Name = "txtTanggalJatuhTempo";
            this.txtTanggalJatuhTempo.Size = new System.Drawing.Size(100, 22);
            this.txtTanggalJatuhTempo.TabIndex = 19;
            // 
            // txtTotalHarga
            // 
            this.txtTotalHarga.Location = new System.Drawing.Point(234, 161);
            this.txtTotalHarga.Name = "txtTotalHarga";
            this.txtTotalHarga.Size = new System.Drawing.Size(100, 22);
            this.txtTotalHarga.TabIndex = 20;
            // 
            // txtIdItem
            // 
            this.txtIdItem.Location = new System.Drawing.Point(234, 72);
            this.txtIdItem.Name = "txtIdItem";
            this.txtIdItem.Size = new System.Drawing.Size(100, 22);
            this.txtIdItem.TabIndex = 22;
            // 
            // combobox
            // 
            this.combobox.FormattingEnabled = true;
            this.combobox.Location = new System.Drawing.Point(234, 211);
            this.combobox.Name = "combobox";
            this.combobox.Size = new System.Drawing.Size(121, 24);
            this.combobox.TabIndex = 23;
            this.combobox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // Peminjaman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.combobox);
            this.Controls.Add(this.txtIdItem);
            this.Controls.Add(this.txtTotalHarga);
            this.Controls.Add(this.txtTanggalJatuhTempo);
            this.Controls.Add(this.txtTanggalPinjam);
            this.Controls.Add(this.txtIdPenyewa);
            this.Controls.Add(this.dgvPeminjaman);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Peminjaman";
            this.Text = "Peminjaman";
            this.Load += new System.EventHandler(this.Peminjaman_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeminjaman)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvPeminjaman;
        private System.Windows.Forms.TextBox txtIdPenyewa;
        private System.Windows.Forms.TextBox txtTanggalPinjam;
        private System.Windows.Forms.TextBox txtTanggalJatuhTempo;
        private System.Windows.Forms.TextBox txtTotalHarga;
        private System.Windows.Forms.TextBox txtIdItem;
        private System.Windows.Forms.ComboBox combobox;
    }
}