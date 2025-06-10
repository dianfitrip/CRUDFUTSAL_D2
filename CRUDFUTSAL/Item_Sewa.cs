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
        private string connectionString = "Data Source=DAEN\\PIYOPUYU;Initial Catalog=Booking_futsal;Integrated Security=True";
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
            // Validasi input
            if (!decimal.TryParse(txtHargaSewa.Text, out decimal harga) || harga <= 0)
            {
                MessageBox.Show("Harga sewa harus lebih besar dari 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtStok.Text, out int stok) || stok < 0)
            {
                MessageBox.Show("Stok tidak boleh negatif", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Apakah Anda yakin ingin menambahkan data ini?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertItemSewa", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdItem", txtIdItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@NamaItem", txtNamaItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@Deskripsi", txtDeskripsi.Text.Trim());
                        cmd.Parameters.AddWithValue("@HargaSewaPerJam", harga);
                        cmd.Parameters.AddWithValue("@Stok", stok);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    lblMessage.Text = "Data berhasil ditambahkan!";
                    LoadData();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    lblMessage.Text = "Gagal menambahkan data: " + ex.Message;
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Apakah Anda yakin ingin menghapus data ini?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteItemSewa", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IdItem", txtIdItem.Text.Trim());

                            // Execute and handle response
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    if (reader.FieldCount == 1) // Success message
                                    {
                                        lblMessage.Text = reader.GetString(0);
                                    }
                                    else // Error occurred
                                    {
                                        lblMessage.Text = reader.GetString(0);
                                        // You could also use ErrorSeverity and ErrorState if needed
                                    }
                                }
                            }
                        }
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Gagal menghapus data: " + ex.Message;
                    }
                }
            }
            else
            {
                lblMessage.Text = "Pilih data terlebih dahulu!";
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count == 0)
            {
                lblMessage.Text = "Pilih data terlebih dahulu!";
                return;
            }

            // Validasi input
            if (!decimal.TryParse(txtHargaSewa.Text, out decimal harga) || harga <= 0)
            {
                MessageBox.Show("Harga sewa harus lebih besar dari 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtStok.Text, out int stok) || stok < 0)
            {
                MessageBox.Show("Stok tidak boleh negatif", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Apakah Anda yakin ingin mengedit data ini?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateItemSewa", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_item", txtIdItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@nama_item", txtNamaItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text.Trim());
                        cmd.Parameters.AddWithValue("@harga_sewa_per_jam", harga);
                        cmd.Parameters.AddWithValue("@stok", stok);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    lblMessage.Text = "Data berhasil diupdate!";
                    LoadData();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }  


        private void txtIdItem_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();

            this.Hide();
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
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
    }
}
