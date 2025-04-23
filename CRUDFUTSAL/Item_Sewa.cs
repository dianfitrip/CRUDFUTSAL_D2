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
    public partial class Item_Sewa : Form
    {
        private string connectionString = "Data Source=DAEN\\PIYOPUYU;Initial Catalog=Bfutsal;Integrated Security=True";
        public Item_Sewa()
        {
            InitializeComponent();
        }

        private void Item_Sewa_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            txtIdItem.Clear();
            txtNamaItem.Clear();
            txtDeskripsi.Clear();
            txtHargaSewa.Clear();
            txtStok.Clear();
            txtIdItem.Focus();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Item_Sewa", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvItem.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengambil data: " + ex.Message);
                }
            }
            ClearForm();

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string idItem = txtIdItem.Text.Trim();
                    string namaItem = txtNamaItem.Text.Trim();
                    string deskripsi = txtDeskripsi.Text.Trim();
                    decimal harga = decimal.Parse(txtHargaSewa.Text.Trim());
                    int stok = int.Parse(txtStok.Text.Trim());

                    SqlCommand cmd = new SqlCommand("INSERT INTO Item_Sewa (id_item, nama_item, deskripsi, harga_sewa_per_jam, stok) VALUES (@id, @nama, @desc, @harga, @stok)", conn);
                    cmd.Parameters.AddWithValue("@id", idItem);
                    cmd.Parameters.AddWithValue("@nama", namaItem);
                    cmd.Parameters.AddWithValue("@desc", deskripsi);
                    cmd.Parameters.AddWithValue("@harga", harga);
                    cmd.Parameters.AddWithValue("@stok", stok);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data berhasil ditambahkan!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menambahkan data: " + ex.Message);
                }
            
               
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string idItem = txtIdItem.Text.Trim();
                            SqlCommand cmd = new SqlCommand("DELETE FROM Item_Sewa WHERE id_item = @id", conn);
                            cmd.Parameters.AddWithValue("@id", idItem);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Data berhasil dihapus!");
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Gagal menghapus data: " + ex.Message);
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
            if (dgvItem.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string idItem = txtIdItem.Text.Trim();
                        string namaItem = txtNamaItem.Text.Trim();
                        string deskripsi = txtDeskripsi.Text.Trim();
                        decimal harga = decimal.Parse(txtHargaSewa.Text.Trim());
                        int stok = int.Parse(txtStok.Text.Trim());

                        SqlCommand cmd = new SqlCommand("UPDATE Item_Sewa SET nama_item = @nama, deskripsi = @desc, harga_sewa_per_jam = @harga, stok = @stok WHERE id_item = @id", conn);
                        cmd.Parameters.AddWithValue("@id", idItem);
                        cmd.Parameters.AddWithValue("@nama", namaItem);
                        cmd.Parameters.AddWithValue("@desc", deskripsi);
                        cmd.Parameters.AddWithValue("@harga", harga);
                        cmd.Parameters.AddWithValue("@stok", stok);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data berhasil diubah!");
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengubah data: " + ex.Message);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvItem.Rows[e.RowIndex];
                txtIdItem.Text = row.Cells["id_item"].Value.ToString();
                txtNamaItem.Text = row.Cells["nama_item"].Value.ToString();
                txtDeskripsi.Text = row.Cells["deskripsi"].Value.ToString();
                txtHargaSewa.Text = row.Cells["harga_sewa_per_jam"].Value.ToString();
                txtStok.Text = row.Cells["stok"].Value.ToString();
            }
        }

        private void txtIdItem_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
