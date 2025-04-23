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
            LoadComboBox();
            LoadData();
        }

        private void LoadComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmdPenyewa = new SqlCommand("SELECT id_penyewa, nama_penyewa FROM Penyewa", conn);
                    SqlDataAdapter daPenyewa = new SqlDataAdapter(cmdPenyewa);
                    DataTable dtPenyewa = new DataTable();
                    daPenyewa.Fill(dtPenyewa);
                    cmbNamaPenyewa.DataSource = dtPenyewa;
                    cmbNamaPenyewa.DisplayMember = "nama_penyewa";
                    cmbNamaPenyewa.ValueMember = "id_penyewa";

                    SqlCommand cmdItem = new SqlCommand("SELECT id_item, nama_item FROM Item_Sewa", conn);
                    SqlDataAdapter daItem = new SqlDataAdapter(cmdItem);
                    DataTable dtItem = new DataTable();
                    daItem.Fill(dtItem);
                    cmbNamaItem.DataSource = dtItem;
                    cmbNamaItem.DisplayMember = "nama_item";
                    cmbNamaItem.ValueMember = "id_item";

                    cmbStatus.Items.Clear();
                    cmbStatus.Items.Add("Dipinjam");
                    cmbStatus.Items.Add("Dikembalikan");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id_peminjaman, id_penyewa, id_item, tanggal_peminjaman, tanggal_jatuh_tempo, total_harga, status FROM Peminjaman";
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
                    string query = "INSERT INTO Peminjaman (id_peminjaman, id_penyewa, id_item, tanggal_peminjaman, tanggal_jatuh_tempo, total_harga, status) " +
                                   "VALUES (@id_peminjaman, @id_penyewa, @id_item, @tanggal_peminjaman, @tanggal_jatuh_tempo, @total_harga, @status)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    string idPeminjaman = "PM" + DateTime.Now.ToString("ddMMyyyyHHmmss");

                    cmd.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);
                    cmd.Parameters.AddWithValue("@id_penyewa", cmbNamaPenyewa.SelectedValue);
                    cmd.Parameters.AddWithValue("@id_item", cmbNamaItem.SelectedValue);
                    cmd.Parameters.AddWithValue("@tanggal_peminjaman", TanggalPinjam.Value);
                    cmd.Parameters.AddWithValue("@tanggal_jatuh_tempo", JatuhTempo.Value);
                    cmd.Parameters.AddWithValue("@total_harga", decimal.Parse(txtTotalHarga.Text));
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);

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
                        string query = "UPDATE Peminjaman SET id_penyewa = @id_penyewa, id_item = @id_item, tanggal_peminjaman = @tanggal_peminjaman, tanggal_jatuh_tempo = @tanggal_jatuh_tempo, total_harga = @total_harga, status = @status WHERE id_peminjaman = @id_peminjaman";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);
                        cmd.Parameters.AddWithValue("@id_penyewa", cmbNamaPenyewa.SelectedValue);
                        cmd.Parameters.AddWithValue("@id_item", cmbNamaItem.SelectedValue);
                        cmd.Parameters.AddWithValue("@tanggal_peminjaman", TanggalPinjam.Value);
                        cmd.Parameters.AddWithValue("@tanggal_jatuh_tempo", JatuhTempo.Value);
                        cmd.Parameters.AddWithValue("@total_harga", decimal.Parse(txtTotalHarga.Text));
                        cmd.Parameters.AddWithValue("@status", cmbStatus.Text);

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
    }
}
