﻿namespace CRUDFUTSAL
{
    partial class Pengembalian
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
            this.txtKondisiPengembalian = new System.Windows.Forms.TextBox();
            this.txtJumlahDenda = new System.Windows.Forms.TextBox();
            this.txtAlasanDenda = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtDendaKeterlambatan = new System.Windows.Forms.TextBox();
            this.txtIdPeminjaman = new System.Windows.Forms.TextBox();
            this.txtTanggalPengembalian = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Peminjaman";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tanggal Pengembalian";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Denda Keterlambatan";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Kondisi Pengembalian";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Jumlah Denda";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Alasan Denda";
            // 
            // txtKondisiPengembalian
            // 
            this.txtKondisiPengembalian.Location = new System.Drawing.Point(244, 134);
            this.txtKondisiPengembalian.Name = "txtKondisiPengembalian";
            this.txtKondisiPengembalian.Size = new System.Drawing.Size(205, 22);
            this.txtKondisiPengembalian.TabIndex = 9;
            // 
            // txtJumlahDenda
            // 
            this.txtJumlahDenda.Location = new System.Drawing.Point(244, 173);
            this.txtJumlahDenda.Name = "txtJumlahDenda";
            this.txtJumlahDenda.Size = new System.Drawing.Size(205, 22);
            this.txtJumlahDenda.TabIndex = 10;
            // 
            // txtAlasanDenda
            // 
            this.txtAlasanDenda.Location = new System.Drawing.Point(244, 211);
            this.txtAlasanDenda.Name = "txtAlasanDenda";
            this.txtAlasanDenda.Size = new System.Drawing.Size(205, 22);
            this.txtAlasanDenda.TabIndex = 11;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(582, 50);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 12;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(582, 79);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 23);
            this.btnHapus.TabIndex = 13;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(582, 108);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(582, 140);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(46, 265);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(611, 150);
            this.dataGridView1.TabIndex = 16;
            // 
            // txtDendaKeterlambatan
            // 
            this.txtDendaKeterlambatan.Location = new System.Drawing.Point(244, 100);
            this.txtDendaKeterlambatan.Name = "txtDendaKeterlambatan";
            this.txtDendaKeterlambatan.Size = new System.Drawing.Size(205, 22);
            this.txtDendaKeterlambatan.TabIndex = 17;
            // 
            // txtIdPeminjaman
            // 
            this.txtIdPeminjaman.Location = new System.Drawing.Point(244, 38);
            this.txtIdPeminjaman.Name = "txtIdPeminjaman";
            this.txtIdPeminjaman.Size = new System.Drawing.Size(205, 22);
            this.txtIdPeminjaman.TabIndex = 18;
            // 
            // txtTanggalPengembalian
            // 
            this.txtTanggalPengembalian.Location = new System.Drawing.Point(244, 69);
            this.txtTanggalPengembalian.Name = "txtTanggalPengembalian";
            this.txtTanggalPengembalian.Size = new System.Drawing.Size(205, 22);
            this.txtTanggalPengembalian.TabIndex = 19;
            // 
            // Pengembalian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtTanggalPengembalian);
            this.Controls.Add(this.txtIdPeminjaman);
            this.Controls.Add(this.txtDendaKeterlambatan);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtAlasanDenda);
            this.Controls.Add(this.txtJumlahDenda);
            this.Controls.Add(this.txtKondisiPengembalian);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Pengembalian";
            this.Text = "Pengembalian";
            this.Load += new System.EventHandler(this.Pengembalian_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.TextBox txtKondisiPengembalian;
        private System.Windows.Forms.TextBox txtJumlahDenda;
        private System.Windows.Forms.TextBox txtAlasanDenda;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtDendaKeterlambatan;
        private System.Windows.Forms.TextBox txtIdPeminjaman;
        private System.Windows.Forms.TextBox txtTanggalPengembalian;
    }
}