using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CRUDFUTSAL
{
    public partial class Peminjaman : Form
    {
        private string connectionString = "Data Source=DAEN\\PIYOPUYU;Initial Catalog=Bfutsal;Integrated Security=True";
        public Peminjaman()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Peminjaman_Load(object sender, EventArgs e)
        {
         
            combobox.Items.AddRange(new string[] { "Dipinjam", "Dikembalikan", "Terlambat" });

            LoadData();
        }

       

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Peminjaman";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPeminjaman.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Peminjaman (id_penyewa, id_item, tanggal_pinjam, tanggal_jatuh_tempo, total_harga, status_peminjaman) " +
                                   "VALUES (@id_penyewa, @id_item, @tanggal_pinjam, @tanggal_jatuh_tempo, @total_harga, @status_peminjaman)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    

                    cmd.Parameters.AddWithValue("@id_penyewa", txtIdPenyewa.Text);
                    cmd.Parameters.AddWithValue("@id_item", txtIdItem.Text);
                    cmd.Parameters.AddWithValue("@tanggal_pinjam", DateTime.Parse(txtTanggalPinjam.Text));
                    cmd.Parameters.AddWithValue("@tanggal_jatuh_tempo", DateTime.Parse(txtTanggalJatuhTempo.Text));
                    cmd.Parameters.AddWithValue("@total_harga", decimal.Parse(txtTotalHarga.Text));
                    cmd.Parameters.AddWithValue("@status_peminjaman", combobox.Text);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Data peminjaman berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Data gagal disimpan!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvPeminjaman.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string idPeminjaman = dgvPeminjaman.SelectedRows[0].Cells["id_peminjaman"].Value.ToString();
                            string query = "DELETE FROM Peminjaman WHERE id_peminjaman = @id_peminjaman";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);

                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Data gagal dihapus!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPeminjaman.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string idPeminjaman = dgvPeminjaman.SelectedRows[0].Cells["id_peminjaman"].Value.ToString();
                        string query = "UPDATE Peminjaman SET id_penyewa = @id_penyewa, id_item = @id_item, tanggal_pinjam = @tanggal_pinjam, tanggal_jatuh_tempo = @tanggal_jatuh_tempo, total_harga = @total_harga, status_peminjaman = @status_peminjaman WHERE id_peminjaman = @id_peminjaman";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);
                        cmd.Parameters.AddWithValue("@id_penyewa", txtIdPenyewa.Text);
                        cmd.Parameters.AddWithValue("@id_item", txtIdItem.Text);
                        cmd.Parameters.AddWithValue("@tanggal_pinjam", DateTime.Parse(txtTanggalPinjam.Text));
                        cmd.Parameters.AddWithValue("@tanggal_jatuh_tempo", DateTime.Parse(txtTanggalJatuhTempo.Text));
                        cmd.Parameters.AddWithValue("@total_harga", decimal.Parse(txtTotalHarga.Text));
                        cmd.Parameters.AddWithValue("@status_peminjaman", combobox.Text);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Data berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Data gagal diupdate!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang akan diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
