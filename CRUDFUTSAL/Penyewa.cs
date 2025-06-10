using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NPOI.HSSF.UserModel;

namespace CRUDFUTSAL
{
    public partial class Penyewa : Form
    {

        private string connectionString = "Data Source=DAEN\\PIYOPUYU;Initial Catalog=Booking_futsal;Integrated Security=True";
        public Penyewa()
        {
            InitializeComponent();
        }

        private void ClearFormPenyewa()
        {
            txtNama.Clear();
            txtAlamat.Clear();
            txtTelepon.Clear();
            txtEmail.Clear();
            txtNama.Focus();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id_penyewa, nama_penyewa, alamat, no_telepon, email, tanggal_terdaftar FROM Penyewa";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvPenyewa.DataSource = dt;
                    ClearFormPenyewa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void Penyewa_Load(object sender, EventArgs e)
        {

        }




        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void btnMenu_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();

            this.Hide();
        }

        private void dgvPenyewa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPenyewa.Rows[e.RowIndex];
                txtNama.Text = row.Cells["nama_penyewa"].Value.ToString();
                txtAlamat.Text = row.Cells["alamat"].Value.ToString();
                txtTelepon.Text = row.Cells["no_telepon"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                dtp1.Value = Convert.ToDateTime(row.Cells["tanggal_terdaftar"].Value);
            }
        }




        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Files|*.xlsx;*.xls" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    int successCount = 0;
                    int failedCount = 0;
                    StringBuilder failedRows = new StringBuilder();

                    try
                    {
                        using (FileStream stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            IWorkbook workbook;
                            if (ofd.FileName.EndsWith(".xlsx"))
                            {
                                workbook = new XSSFWorkbook(stream);
                            }
                            else
                            {
                                workbook = new HSSFWorkbook(stream);
                            }

                            ISheet sheet = workbook.GetSheetAt(0);

                            for (int i = 1; i <= sheet.LastRowNum; i++)
                            {
                                IRow row = sheet.GetRow(i);
                                if (row == null) continue;

                                try
                                {
                                    string nama = row.GetCell(0)?.ToString()?.Trim();
                                    string alamat = row.GetCell(1)?.ToString()?.Trim();
                                    string telepon = row.GetCell(2)?.ToString()?.Trim();
                                    string email = row.GetCell(3)?.ToString()?.Trim();
                                    DateTime tanggal;

                                    // Validasi data wajib
                                    if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(alamat) ||
                                        string.IsNullOrWhiteSpace(telepon) || string.IsNullOrWhiteSpace(email))
                                    {
                                        throw new Exception("Data tidak lengkap");
                                    }

                                    // Validasi format telepon
                                    if (!telepon.StartsWith("08") || telepon.Length < 10 || telepon.Length > 13)
                                    {
                                        throw new Exception("Nomor telepon harus dimulai dengan '08' dan panjang 10-13 digit");
                                    }

                                    // Validasi email
                                    if (!email.Contains("@") || !email.Contains("."))
                                    {
                                        throw new Exception("Format email tidak valid");
                                    }

                                    // Tanggal terdaftar
                                    ICell dateCell = row.GetCell(4);
                                    if (dateCell != null)
                                    {
                                        if (dateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(dateCell))
                                        {
                                            tanggal = dateCell.DateCellValue ?? DateTime.Now;
                                        }
                                        else if (DateTime.TryParse(dateCell.ToString(), out DateTime parsedDate))
                                        {
                                            tanggal = parsedDate;
                                        }
                                        else
                                        {
                                            tanggal = DateTime.Now;
                                        }
                                    }
                                    else
                                    {
                                        tanggal = DateTime.Now;
                                    }

                                    // Simpan ke database
                                    using (SqlConnection conn = new SqlConnection(connectionString))
                                    {
                                        conn.Open();
                                        SqlCommand cmd = new SqlCommand("sp_InsertPenyewa", conn);
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@nama_penyewa", nama);
                                        cmd.Parameters.AddWithValue("@alamat", alamat);
                                        cmd.Parameters.AddWithValue("@no_telepon", telepon);
                                        cmd.Parameters.AddWithValue("@email", email);
                                        cmd.Parameters.AddWithValue("@tanggal_terdaftar", tanggal);

                                        cmd.ExecuteNonQuery();
                                        successCount++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    failedCount++;
                                    failedRows.AppendLine($"Baris {i + 1}: {ex.Message}");
                                }
                            }
                        }

                        string message = $"Import selesai.\nBerhasil: {successCount}\nGagal: {failedCount}";
                        if (failedCount > 0)
                        {
                            message += $"\n\nDetail gagal:\n{failedRows.ToString()}";
                        }

                        MessageBox.Show(message, "Hasil Import", MessageBoxButtons.OK,
                            failedCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                        // Refresh data setelah import
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal memproses file Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_AnalisisPenyewa", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Tampilkan di DataGridView atau MessageBox
                    string hasil = "Analisis Jumlah Penyewa per Bulan:\n\n";
                    foreach (DataRow row in dt.Rows)
                    {
                        hasil += $"{row["Bulan"]}: {row["JumlahPenyewa"]} penyewa\n";
                    }

                    MessageBox.Show(hasil, "Hasil Analisis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal melakukan analisis: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtpTanggalTerdaftar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtAlamat.Text) ||
                string.IsNullOrWhiteSpace(txtTelepon.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblMessage.Text = "Harap lengkapi semua data!";
                return;
            }

            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin menambahkan data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertPenyewa", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nama_penyewa", txtNama.Text);
                        cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                        cmd.Parameters.AddWithValue("@no_telepon", txtTelepon.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@tanggal_terdaftar", dtp1.Value);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();


                    lblMessage.Text = "Data berhasil ditambahkan.";
                    LoadData();

                }
                catch (Exception ex)
                {
                    if (transaction != null)
                        transaction.Rollback();

                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvPenyewa.SelectedRows.Count == 0)
            {
                lblMessage.Text = "Pilih data yang akan dihapus!";
                return;
            }

            int id = Convert.ToInt32(dgvPenyewa.SelectedRows[0].Cells["id_penyewa"].Value);
            DialogResult result = MessageBox.Show("Apakah yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlTransaction transaction = null;

                    try
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction();

                        using (SqlCommand cmd = new SqlCommand("DeletePenyewa", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id_penyewa", id);

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                transaction.Commit();
                                lblMessage.Text = "Data berhasil dihapus.";
                                LoadData();

                            }
                            else
                            {
                                transaction.Rollback();
                                lblMessage.Text = "Data tidak ditemukan atau gagal dihapus.";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction?.Rollback();
                        lblMessage.Text = "Error: " + ex.Message;
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPenyewa.SelectedRows.Count == 0)
            {
                lblMessage.Text = "Pilih data penyewa yang akan diubah!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            // Validasi data wajib
            if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtAlamat.Text) ||
                string.IsNullOrWhiteSpace(txtTelepon.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblMessage.Text = "Harap lengkapi semua data!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            // Validasi format telepon
            if (!txtTelepon.Text.StartsWith("08") || txtTelepon.Text.Length < 10 || txtTelepon.Text.Length > 13)
            {
                lblMessage.Text = "Nomor telepon harus dimulai dengan '08' dan panjang 10-13 digit";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            // Validasi email
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                lblMessage.Text = "Format email tidak valid (harus mengandung @ dan .)";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            // Konfirmasi perubahan
            DialogResult result = MessageBox.Show(
                "Apakah Anda yakin ingin mengubah data penyewa ini?",
                "Konfirmasi Perubahan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
                return;

            try
            {
                int idPenyewa = Convert.ToInt32(dgvPenyewa.SelectedRows[0].Cells["id_penyewa"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdatePenyewa", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@id_penyewa", idPenyewa);
                        cmd.Parameters.AddWithValue("@nama_penyewa", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text.Trim());
                        cmd.Parameters.AddWithValue("@no_telepon", txtTelepon.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@tanggal_terdaftar", dtp1.Value);

                        // Add return parameter
                        SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(returnValue);

                        cmd.ExecuteNonQuery();

                        int resultCode = (int)cmd.Parameters["@ReturnValue"].Value;

                        // Set warna default hijau untuk pesan sukses
                        lblMessage.ForeColor = Color.Green;

                        switch (resultCode)
                        {
                            case 1:
                                lblMessage.Text = "Data penyewa berhasil diperbarui!";
                                LoadData();
                                break;
                            case 0:
                                // Jika data sama, tampilkan pesan info (bukan error)
                                lblMessage.Text = "Tidak ada perubahan data karena nilai yang dimasukkan sama dengan data yang sudah ada.";
                                lblMessage.ForeColor = Color.Blue;
                                break;
                            case -1:
                                lblMessage.Text = "Data penyewa tidak ditemukan.";
                                lblMessage.ForeColor = Color.Red;
                                break;
                            case -2:
                                lblMessage.Text = "Format nomor telepon tidak valid (harus dimulai dengan 08, 10-13 digit).";
                                lblMessage.ForeColor = Color.Red;
                                break;
                            case -3:
                                lblMessage.Text = "Format email tidak valid (harus mengandung @ dan .).";
                                lblMessage.ForeColor = Color.Red;
                                break;
                            case -99:
                                lblMessage.Text = "Terjadi kesalahan saat memperbarui data.";
                                lblMessage.ForeColor = Color.Red;
                                break;
                            default:
                                lblMessage.Text = "Hasil tidak dikenali.";
                                lblMessage.ForeColor = Color.Red;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Kesalahan koneksi atau proses: " + ex.Message;
                lblMessage.ForeColor = Color.Red;
            }

        }
    }
}

