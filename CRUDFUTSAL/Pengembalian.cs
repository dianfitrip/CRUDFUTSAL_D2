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

namespace CRUDFUTSAL
{
    public partial class Pengembalian: Form
    {
        private string connectionString = "Data Source=DAEN\\PIYOPUYU;Initial Catalog=Booking_futsal;Integrated Security=True";
        public Pengembalian()
        {
            InitializeComponent();
            LoadPeminjamanCombo();
            cmbStatus.Items.AddRange(new string[] { "Tepat Waktu", "Terlambat" });
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Pengembalian", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            lblMessage.Text = "Data berhasil dimuat";
            lblMessage.ForeColor = Color.Green;
        }


        private void Pengembalian_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }
        private void LoadPeminjamanCombo()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id_peminjaman FROM Peminjaman";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<int> idList = new List<int>();
                    while (reader.Read())
                    {
                        idList.Add(reader.GetInt32(0));
                    }

                    cmb1.DataSource = idList;
                    lblMessage.Text = "Data peminjaman berhasil dimuat";
                    lblMessage.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Gagal memuat data peminjaman: " + ex.Message;
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin menambahkan data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertPengembalian", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_peminjaman", cmb1.SelectedValue);
                    cmd.Parameters.AddWithValue("@tanggal_pengembalian", dtp1.Value);
                    cmd.Parameters.AddWithValue("@tanggal_jatuh_tempo", dtp2.Value);
                    cmd.Parameters.AddWithValue("@status_pengembalian", cmbStatus.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@jumlah_total", decimal.Parse(txtJumlahTotal.Text));
                    cmd.Parameters.AddWithValue("@jumlah_denda", decimal.Parse(txtJumlahDenda.Text));
                    cmd.Parameters.AddWithValue("@alasan_denda", txtAlasanDenda.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                lblMessage.Text = "Data berhasil ditambahkan!";
                lblMessage.ForeColor = Color.Green;
                LoadData();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.ForeColor = Color.Red;
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            if (dataGridView1.CurrentRow == null)
            {
                lblMessage.Text = "Tidak ada data yang dipilih";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id_pengembalian"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeletePengembalian", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_pengembalian", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            lblMessage.Text = "Data berhasil dihapus!";
            lblMessage.ForeColor = Color.Green;
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            if (dataGridView1.CurrentRow == null)
            {
                lblMessage.Text = "Tidak ada data yang dipilih";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin mengedit data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id_pengembalian"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdatePengembalian", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_pengembalian", id);
                    cmd.Parameters.AddWithValue("@id_peminjaman", cmb1.SelectedValue);
                    cmd.Parameters.AddWithValue("@tanggal_pengembalian", dtp1.Value);
                    cmd.Parameters.AddWithValue("@tanggal_jatuh_tempo", dtp2.Value);
                    cmd.Parameters.AddWithValue("@status_pengembalian", cmbStatus.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@jumlah_total", decimal.Parse(txtJumlahTotal.Text));
                    cmd.Parameters.AddWithValue("@jumlah_denda", decimal.Parse(txtJumlahDenda.Text));
                    cmd.Parameters.AddWithValue("@alasan_denda", txtAlasanDenda.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                lblMessage.Text = "Data berhasil diupdate!";
                lblMessage.ForeColor = Color.Green;
                LoadData();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.ForeColor = Color.Red;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Memuat ulang data...";
            lblMessage.ForeColor = Color.Blue;
            LoadData();
            lblMessage.Text = "Data berhasil dimuat ulang";
            lblMessage.ForeColor = Color.Green;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            cmb1.SelectedValue = Convert.ToInt32(row.Cells["id_peminjaman"].Value);
            dtp1.Value = Convert.ToDateTime(row.Cells["tanggal_pengembalian"].Value);
            dtp2.Value = Convert.ToDateTime(row.Cells["tanggal_jatuh_tempo"].Value);
            cmbStatus.SelectedItem = row.Cells["status_pengembalian"].Value?.ToString();
            txtJumlahTotal.Text = row.Cells["jumlah_total"].Value?.ToString();
            txtJumlahDenda.Text = row.Cells["jumlah_denda"].Value?.ToString();
            txtAlasanDenda.Text = row.Cells["alasan_denda"].Value?.ToString();
        }

        private void dtp2_ValueChanged(object sender, EventArgs e)
        {
            HitungDendaDanTotal();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            HitungDendaDanTotal();
        }

        private void HitungDendaDanTotal()
        {
            if (cmb1.SelectedValue == null) return;

            DateTime jatuhTempo = dtp2.Value;
            DateTime tglKembali = dtp1.Value;

            decimal denda = 0;

            // Only calculate fine if status is "Terlambat"
            if (cmbStatus.SelectedItem?.ToString() == "Terlambat")
            {
                // Calculate hours late (rounded up)
                double keterlambatanJam = (tglKembali - jatuhTempo).TotalHours;
                keterlambatanJam = Math.Max(0, keterlambatanJam);

                // Round up to nearest hour
                int jamDenda = (int)Math.Ceiling(keterlambatanJam);

                // Calculate fine (Rp5000 per hour)
                denda = jamDenda * 5000;
            }

            decimal totalHargaPeminjaman = AmbilTotalHargaPeminjaman(Convert.ToInt32(cmb1.SelectedValue));
            decimal jumlahTotal = totalHargaPeminjaman + denda;

            txtJumlahDenda.Text = denda.ToString("F0");
            txtJumlahTotal.Text = jumlahTotal.ToString("F0");
        }

        private decimal AmbilTotalHargaPeminjaman(int idPeminjaman)
        {
            decimal totalHarga = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT total_harga FROM Peminjaman WHERE id_peminjaman = @id", conn);
                cmd.Parameters.AddWithValue("@id", idPeminjaman);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    totalHarga = Convert.ToDecimal(result);
                }
            }
            return totalHarga;
        }

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {
            HitungDendaDanTotal();
        }

        private void cmb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb1.SelectedItem == null) return;

            string selectedId = cmb1.Text.Split('-')[0].Trim(); // Misal: "PM001 - Futsal 1"

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                SELECT tanggal_jatuh_tempo, total_harga 
                FROM Peminjaman 
                WHERE id_peminjaman = @id", conn);
                cmd.Parameters.AddWithValue("@id", selectedId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dtp2.Value = Convert.ToDateTime(reader["tanggal_jatuh_tempo"]);
                    txtJumlahTotal.Text = Convert.ToDecimal(reader["total_harga"]).ToString("F0");
                }
                reader.Close();
            }

        }
    }
}
