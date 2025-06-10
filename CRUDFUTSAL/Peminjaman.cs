using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace CRUDFUTSAL
{
    public partial class Peminjaman : Form
    {
        private string connectionString = "Data Source=DAEN\\PIYOPUYU;Initial Catalog=Booking_futsal;Integrated Security=True";

        public Peminjaman()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Peminjaman_Load(object sender, EventArgs e)
        {
            LoadPenyewa();
            LoadItem();
            LoadData();
            cmbStatus.Items.AddRange(new string[] { "Dipinjam", "Dibatalkan" });

            // Add the CellClick event handler
            dgvPeminjaman.CellClick += dgvPeminjaman_CellClick;
        }


        private void LoadPenyewa()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_penyewa, nama_penyewa FROM Penyewa", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbNamaPenyewa.Items.Add($"{reader["id_penyewa"]} - {reader["nama_penyewa"]}");
                }
                reader.Close();
            }
        }

        private void LoadItem()
        {
            Dictionary<string, decimal> hargaMap = new Dictionary<string, decimal>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_item, nama_item, harga_sewa_per_jam FROM Item_Sewa", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader["id_item"].ToString();
                    string nama = reader["nama_item"].ToString();
                    decimal harga = Convert.ToDecimal(reader["harga_sewa_per_jam"]);
                    hargaMap[id] = harga;
                    cmbItem.Items.Add($"{id} - {nama}");
                }
                reader.Close();
            }
            cmbItem.Tag = hargaMap;
        }



        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT p.id_peminjaman, p.id_penyewa, py.nama_penyewa, p.id_item, i.nama_item,
                           p.tanggal_pinjam, p.tanggal_jatuh_tempo, p.total_harga, p.status_peminjaman
                    FROM Peminjaman p
                    JOIN Penyewa py ON p.id_penyewa = py.id_penyewa
                    JOIN Item_Sewa i ON p.id_item = i.id_item";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPeminjaman.DataSource = dt;
            }
        }


        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Clear previous messages
            lblMessage.Text = "";
            lblMessage.ForeColor = SystemColors.ControlText;

            // Validate date - cannot be in the past
            if (dtpTanggalPinjam.Value.Date < DateTime.Now.Date)
            {
                lblMessage.Text = "Tanggal peminjaman tidak boleh dari masa lalu! Hanya boleh hari ini atau masa depan.";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            // Validate inputs
            if (cmbNamaPenyewa.SelectedIndex == -1 || cmbItem.SelectedIndex == -1 || string.IsNullOrEmpty(txtJumlah.Text))
            {
                lblMessage.Text = "Harap lengkapi semua field!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                lblMessage.Text = "Jumlah harus berupa angka positif lebih dari 0!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            // Calculate duration
            TimeSpan duration = dtpTanggalTempo.Value - dtpTanggalPinjam.Value;
            int hours = (int)Math.Ceiling(duration.TotalHours);
            if (hours < 1) hours = 1;

            // Get price from dictionary
            var hargaMap = cmbItem.Tag as Dictionary<string, decimal>;
            if (hargaMap == null)
            {
                lblMessage.Text = "Data harga tidak valid!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            string idItem = cmbItem.Text.Split('-')[0].Trim();
            if (!hargaMap.TryGetValue(idItem, out decimal harga))
            {
                lblMessage.Text = "Harga item tidak ditemukan!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            // Calculate total price
            decimal totalHarga = harga * jumlah * hours;

            // Confirm before saving
            DialogResult result = MessageBox.Show(
                $"Anda akan meminjam {jumlah} item {idItem} selama {hours} jam.\nTotal harga: Rp {totalHarga:N0}\n\nLanjutkan?",
                "Konfirmasi Peminjaman",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            // Get penyewa ID
            string idPenyewa = cmbNamaPenyewa.Text.Split('-')[0].Trim();

            // Execute stored procedure
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertPeminjaman", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    cmd.Parameters.AddWithValue("@id_penyewa", int.Parse(idPenyewa));
                    cmd.Parameters.AddWithValue("@id_item", idItem);
                    cmd.Parameters.AddWithValue("@tanggal_pinjam", dtpTanggalPinjam.Value);
                    cmd.Parameters.AddWithValue("@tanggal_jatuh_tempo", dtpTanggalTempo.Value);
                    cmd.Parameters.AddWithValue("@jumlah", jumlah);

                    // Add output parameters
                    SqlParameter returnMessage = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 255);
                    returnMessage.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(returnMessage);

                    SqlParameter isSuccess = new SqlParameter("@IsSuccess", SqlDbType.Bit);
                    isSuccess.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(isSuccess);

                    // Execute the command
                    cmd.ExecuteNonQuery();

                    // Get the results
                    bool success = (bool)isSuccess.Value;
                    string message = returnMessage.Value.ToString();

                    if (success)
                    {
                        lblMessage.Text = message;
                        lblMessage.ForeColor = Color.Green;
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        lblMessage.Text = message;
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Terjadi kesalahan: {ex.Message}";
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }

        // Optional helper method to clear form
        private void ClearForm()
        {
            cmbNamaPenyewa.SelectedIndex = -1;  // Fixed potential typo
            cmbItem.SelectedIndex = -1;
            txtJumlah.Text = "";                // Fixed typo
            dtpTanggalPinjam.Value = DateTime.Now;
            dtpTanggalTempo.Value = DateTime.Now.AddHours(1);

            // Ganti bagian pencarian lblTotalHarga dengan ini:
            var totalHargaLabel = this.Controls.Find("lblTotalHarga", true).FirstOrDefault() as Label;
            if (totalHargaLabel != null)
            {
                totalHargaLabel.Text = "Total Harga: Rp 0";
            }
        }




        private void btnHapus_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = SystemColors.ControlText;

            if (dgvPeminjaman.SelectedRows.Count == 0)
            {
                lblMessage.Text = "Silakan pilih data yang akan dihapus!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            string idPeminjaman = dgvPeminjaman.SelectedRows[0].Cells["id_peminjaman"].Value.ToString();

            DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_DeletePeminjaman", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_peminjaman", int.Parse(idPeminjaman));

                try
                {
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Data berhasil dihapus.";
                    lblMessage.ForeColor = Color.Green;
                    LoadData();
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = $"Gagal menghapus data: {ex.Message}";
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Clear previous messages
            lblMessage.Text = "";
            lblMessage.ForeColor = SystemColors.ControlText;

            if (dgvPeminjaman.SelectedRows.Count == 0)
            {
                lblMessage.Text = "Pilih data yang akan diubah terlebih dahulu!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (cmbNamaPenyewa.SelectedIndex == -1 || cmbItem.SelectedIndex == -1 ||
                string.IsNullOrEmpty(txtJumlah.Text) || cmbStatus.SelectedIndex == -1)
            {
                lblMessage.Text = "Harap lengkapi semua field termasuk status!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                lblMessage.Text = "Jumlah harus berupa angka positif lebih dari 0!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (dtpTanggalTempo.Value <= dtpTanggalPinjam.Value)
            {
                lblMessage.Text = "Tanggal jatuh tempo harus setelah tanggal pinjam!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            // Get selected row data
            DataGridViewRow selectedRow = dgvPeminjaman.SelectedRows[0];
            string oldStatus = selectedRow.Cells["status_peminjaman"].Value.ToString();
            string newStatus = cmbStatus.SelectedItem.ToString();

            // Show confirmation message with status change info if relevant
            string confirmMessage = "Apakah Anda yakin ingin mengubah data ini?";
            if (oldStatus != newStatus)
            {
                confirmMessage += $"\n\nPerubahan status: {oldStatus} → {newStatus}";
                if (newStatus == "Dibatalkan")
                {
                    confirmMessage += "\n\nStok item akan dikembalikan ke inventory.";
                }
                else if (oldStatus == "Dibatalkan" && newStatus != "Dibatalkan")
                {
                    confirmMessage += "\n\nStok item akan dikurangi dari inventory.";
                }
            }

            DialogResult result = MessageBox.Show(confirmMessage, "Konfirmasi",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            string idPeminjaman = selectedRow.Cells["id_peminjaman"].Value.ToString();
            string idPenyewa = cmbNamaPenyewa.Text.Split('-')[0].Trim();
            string idItem = cmbItem.Text.Split('-')[0].Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdatePeminjaman", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_peminjaman", int.Parse(idPeminjaman));
                        cmd.Parameters.AddWithValue("@id_penyewa", int.Parse(idPenyewa));
                        cmd.Parameters.AddWithValue("@id_item", idItem);
                        cmd.Parameters.AddWithValue("@tanggal_pinjam", dtpTanggalPinjam.Value);
                        cmd.Parameters.AddWithValue("@tanggal_jatuh_tempo", dtpTanggalTempo.Value);
                        cmd.Parameters.AddWithValue("@jumlah", jumlah);
                        cmd.Parameters.AddWithValue("@status_peminjaman", newStatus);

                        SqlParameter returnParam = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                        returnParam.Direction = ParameterDirection.ReturnValue;

                        cmd.ExecuteNonQuery();

                        int returnValue = (int)returnParam.Value;

                        // Handle return values with detailed messages
                        switch (returnValue)
                        {
                            case 1:
                                lblMessage.Text = "Data berhasil diubah.";
                                if (newStatus == "Dibatalkan")
                                {
                                    lblMessage.Text += "\nStok item telah dikembalikan ke inventory.";
                                }
                                lblMessage.ForeColor = Color.Green;
                                break;
                            case -1:
                                lblMessage.Text = "Data peminjaman tidak ditemukan.";
                                lblMessage.ForeColor = Color.Red;
                                break;
                            case -2:
                                lblMessage.Text = "Item tidak ditemukan.";
                                lblMessage.ForeColor = Color.Red;
                                break;
                            case -3:
                                lblMessage.Text = "Stok tidak mencukupi.";
                                lblMessage.ForeColor = Color.Red;
                                break;
                            case -4:
                                lblMessage.Text = "Status peminjaman tidak valid. Gunakan: Dipinjam, Dibatalkan, atau Dikembalikan.";
                                lblMessage.ForeColor = Color.Red;
                                break;
                            case -5:
                                lblMessage.Text = "Tanggal jatuh tempo harus setelah tanggal pinjam.";
                                lblMessage.ForeColor = Color.Red;
                                break;
                            case -99:
                                lblMessage.Text = "Terjadi kesalahan saat mengupdate data.";
                                lblMessage.ForeColor = Color.Red;
                                break;
                            default:
                                lblMessage.Text = "Tidak ada perubahan data atau terjadi kesalahan tidak diketahui.";
                                lblMessage.ForeColor = Color.Blue;
                                break;
                        }

                        // Refresh data setelah edit
                        LoadData();

                        // Also refresh item stock display if needed
                        if (returnValue == 1 && (newStatus == "Dibatalkan" || oldStatus == "Dibatalkan"))
                        {
                            LoadItem(); // Reload item data to reflect stock changes
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                lblMessage.Text = $"Database Error: {ex.Message}";
                lblMessage.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
                lblMessage.ForeColor = Color.Red;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

     
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void HitungTotalHarga()
        {
            if (cmbItem.SelectedItem == null)
                return;

            var hargaMap = cmbItem.Tag as Dictionary<string, decimal>;
            if (hargaMap == null)
                return;

            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah harus berupa angka dan lebih dari 0.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idItem = cmbItem.Text.Split('-')[0].Trim();

            if (hargaMap.TryGetValue(idItem, out decimal harga))
            {
                decimal total = harga * jumlah;

                if (total < 2000)
                {
                    MessageBox.Show("Total harga minimal Rp 2000. Peminjaman ditolak.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Total harga: Rp {total:F0}", "Total Harga", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Item tidak ditemukan dalam daftar harga.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnMenu_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();

            this.Hide();
        }

        private void txtJumlah_TextChanged(object sender, EventArgs e)
        {
            HitungTotalHarga();

        }

        private void lblJumlah_Click(object sender, EventArgs e)
        {

        }



        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            HitungTotalHarga();
        }

       

        private void cmbNamaPenyewa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamaPenyewa.SelectedItem != null)
            {
                string selected = cmbNamaPenyewa.SelectedItem.ToString();
                string idPenyewa = selected.Split('-')[0].Trim();
                cmbNamaPenyewa.Text = idPenyewa;
            }
        }


        private bool ValidasiHarga(out decimal totalHarga)
        {
            totalHarga = 0;

            // Pastikan item dipilih
            if (cmbItem.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih item terlebih dahulu.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Pastikan Tag berisi Dictionary harga
            var hargaMap = cmbItem.Tag as Dictionary<string, decimal>;
            if (hargaMap == null)
            {
                MessageBox.Show("Data harga tidak tersedia.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validasi jumlah harus angka dan lebih dari 0
            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah harus berupa angka dan lebih dari 0.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Ambil ID item dari combobox (format: IT001 - Nama Item)
            string idItem = cmbItem.Text.Split('-')[0].Trim();

            // Ambil harga dari dictionary
            if (!hargaMap.TryGetValue(idItem, out decimal harga))
            {
                MessageBox.Show("Harga untuk item yang dipilih tidak ditemukan.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Hitung total harga
            totalHarga = harga * jumlah;

            // Validasi minimum harga
            if (totalHarga < 2000)
            {
                MessageBox.Show("Total harga minimal adalah Rp 2000.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void dtpTanggalPinjam_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvPeminjaman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Skip if header row is clicked or no row is selected
            if (e.RowIndex < 0 || dgvPeminjaman.Rows.Count == 0) return;

            // Get the selected row
            DataGridViewRow row = dgvPeminjaman.Rows[e.RowIndex];

            try
            {
                // Populate penyewa combobox
                string penyewaId = row.Cells["id_penyewa"].Value.ToString();
                string penyewaName = row.Cells["nama_penyewa"].Value.ToString();
                cmbNamaPenyewa.Text = $"{penyewaId} - {penyewaName}";

                // Populate item combobox
                string itemId = row.Cells["id_item"].Value.ToString();
                string itemName = row.Cells["nama_item"].Value.ToString();
                cmbItem.Text = $"{itemId} - {itemName}";

                // Populate dates
                dtpTanggalPinjam.Value = Convert.ToDateTime(row.Cells["tanggal_pinjam"].Value);
                dtpTanggalTempo.Value = Convert.ToDateTime(row.Cells["tanggal_jatuh_tempo"].Value);

                // Calculate and populate jumlah (quantity)
                decimal totalHarga = Convert.ToDecimal(row.Cells["total_harga"].Value);
                var hargaMap = cmbItem.Tag as Dictionary<string, decimal>;
                if (hargaMap != null && hargaMap.TryGetValue(itemId, out decimal hargaPerItem))
                {
                    TimeSpan duration = dtpTanggalTempo.Value - dtpTanggalPinjam.Value;
                    int hours = (int)Math.Ceiling(duration.TotalHours);
                    if (hours < 1) hours = 1;

                    int jumlah = (int)(totalHarga / (hargaPerItem * hours));
                    txtJumlah.Text = jumlah.ToString();
                }

                // Populate status if available
                if (row.Cells["status_peminjaman"].Value != null)
                {
                    cmbStatus.Text = row.Cells["status_peminjaman"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
