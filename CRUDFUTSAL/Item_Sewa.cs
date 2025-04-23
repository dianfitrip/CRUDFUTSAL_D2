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
            txtNamaItem.Clear();
            txtDeskripsi.Clear();
            txtHargaSewa.Clear();
            txtStok.Clear();
            txtNamaItem.Focus();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id_item, nama_item, deskripsi, harga_sewa_per_jam, stok FROM Item_Sewa";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvItem.DataSource = dt;
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    if (txtNamaItem.Text == "" || txtHargaSewa.Text == "" || txtStok.Text == "")
                    {
                        MessageBox.Show("Harap lengkapi data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    conn.Open();
                    string query = "INSERT INTO Item_Sewa (id_item, nama_item, deskripsi, harga_sewa_per_jam, stok) VALUES (@id_item, @nama_item, @deskripsi, @harga, @stok)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        string idItem = "IT" + DateTime.Now.ToString("ddMMyyyyHHmmss");

                        cmd.Parameters.AddWithValue("@id_item", idItem);
                        cmd.Parameters.AddWithValue("@nama_item", txtNamaItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text.Trim());
                        cmd.Parameters.AddWithValue("@harga", decimal.Parse(txtHargaSewa.Text));
                        cmd.Parameters.AddWithValue("@stok", int.Parse(txtStok.Text));

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Data gagal ditambahkan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
            if (dgvItem.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Yakin ingin menghapus item ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            string idItem = dgvItem.SelectedRows[0].Cells["id_item"].Value.ToString();
                            conn.Open();
                            string query = "DELETE FROM Item_Sewa WHERE id_item = @id_item";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@id_item", idItem);
                                if (cmd.ExecuteNonQuery() > 0)
                                {
                                    MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                }
                                else
                                {
                                    MessageBox.Show("Data gagal dihapus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
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
            if (dgvItem.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        string idItem = dgvItem.SelectedRows[0].Cells["id_item"].Value.ToString();

                        conn.Open();
                        string query = "UPDATE Item_Sewa SET nama_item = @nama_item, deskripsi = @deskripsi, harga_sewa_per_jam = @harga, stok = @stok WHERE id_item = @id_item";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id_item", idItem);
                            cmd.Parameters.AddWithValue("@nama_item", txtNamaItem.Text.Trim());
                            cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text.Trim());
                            cmd.Parameters.AddWithValue("@harga", decimal.Parse(txtHargaSewa.Text));
                            cmd.Parameters.AddWithValue("@stok", int.Parse(txtStok.Text));

                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Data berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Data gagal diubah!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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

        private void dgvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtNamaItem.Text = dgvItem.Rows[e.RowIndex].Cells["nama_item"].Value.ToString();
                txtDeskripsi.Text = dgvItem.Rows[e.RowIndex].Cells["deskripsi"].Value.ToString();
                txtHargaSewa.Text = dgvItem.Rows[e.RowIndex].Cells["harga_sewa_per_jam"].Value.ToString();
                txtStok.Text = dgvItem.Rows[e.RowIndex].Cells["stok"].Value.ToString();
            }
        }
    }
}
