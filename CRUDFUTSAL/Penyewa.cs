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
    public partial class Penyewa: Form
    {

        private string connectionString = "Data Source=DAEN\\PIYOPUYU;Initial Catalog=Bfutsal;Integrated Security=True";
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
            txtTanggalTerdaftar.Clear();
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

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (txtNama.Text == "" || txtAlamat.Text == "" || txtTelepon.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Harap lengkapi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Penyewa (nama_penyewa, alamat, no_telepon, email, tanggal_terdaftar) " +
                                   "VALUES (@nama, @alamat, @telepon, @email, @tanggal)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text.Trim());
                        cmd.Parameters.AddWithValue("@telepon", txtTelepon.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@tanggal", txtTanggalTerdaftar.Text.Trim());

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvPenyewa.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvPenyewa.SelectedRows[0].Cells["id_penyewa"].Value);
                DialogResult result = MessageBox.Show("Apakah yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM Penyewa WHERE id_penyewa = @id";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                int rows = cmd.ExecuteNonQuery();
                                if (rows > 0)
                                {
                                    MessageBox.Show("Data berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPenyewa.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvPenyewa.SelectedRows[0].Cells["id_penyewa"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE Penyewa SET nama_penyewa=@nama, alamat=@alamat, no_telepon=@telepon, email=@email, tanggal_terdaftar=@tanggal WHERE id_penyewa=@id";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@nama", txtNama.Text.Trim());
                            cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text.Trim());
                            cmd.Parameters.AddWithValue("@telepon", txtTelepon.Text.Trim());
                            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@tanggal", txtTanggalTerdaftar.Text.Trim());
                            cmd.Parameters.AddWithValue("@id", id);

                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Data berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang akan diubah!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvPenyewa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPenyewa.Rows[e.RowIndex];
                txtNama.Text = row.Cells["nama_penyewa"].Value.ToString();
                txtAlamat.Text = row.Cells["alamat"].Value.ToString();
                txtTelepon.Text = row.Cells["no_telepon"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtTanggalTerdaftar.Text = row.Cells["tanggal_terdaftar"].Value.ToString();
            }

        }
    }
}

