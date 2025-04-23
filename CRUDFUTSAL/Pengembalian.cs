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
        private string connectionString = "Data Source=DAEN\\PIYOPUYU;Initial Catalog=Bfutsal;Integrated Security=True";
        public Pengembalian()
        {
            InitializeComponent();
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
        }


        private void Pengembalian_Load(object sender, EventArgs e)
        {

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Pengembalian (id_peminjaman, tanggal_pengembalian, denda_keterlambatan, kondisi_pengembalian, jumlah_denda, alasan_denda) " +
                                   "VALUES (@id_peminjaman, @tanggal_pengembalian, @denda_keterlambatan, @kondisi_pengembalian, @jumlah_denda, @alasan_denda)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_peminjaman", txtIdPeminjaman.Text);
                    cmd.Parameters.AddWithValue("@tanggal_pengembalian", DateTime.Parse(txtTanggalPengembalian.Text));
                    cmd.Parameters.AddWithValue("@denda_keterlambatan", decimal.Parse(txtDendaKeterlambatan.Text));
                    cmd.Parameters.AddWithValue("@kondisi_pengembalian", txtKondisiPengembalian.Text);
                    cmd.Parameters.AddWithValue("@jumlah_denda", decimal.Parse(txtJumlahDenda.Text));
                    cmd.Parameters.AddWithValue("@alasan_denda", txtAlasanDenda.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Data berhasil ditambahkan!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id_pengembalian"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Pengembalian WHERE id_pengembalian=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Data berhasil dihapus!");
                LoadData();
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow != null)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id_pengembalian"].Value);

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Pengembalian SET id_peminjaman=@id_peminjaman, tanggal_pengembalian=@tanggal_pengembalian, denda_keterlambatan=@denda_keterlambatan, " +
                                       "kondisi_pengembalian=@kondisi_pengembalian, jumlah_denda=@jumlah_denda, alasan_denda=@alasan_denda WHERE id_pengembalian=@id_pengembalian";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id_pengembalian", id);
                        cmd.Parameters.AddWithValue("@id_peminjaman", txtIdPeminjaman.Text);
                        cmd.Parameters.AddWithValue("@tanggal_pengembalian", DateTime.Parse(txtTanggalPengembalian.Text));
                        cmd.Parameters.AddWithValue("@denda_keterlambatan", decimal.Parse(txtDendaKeterlambatan.Text));
                        cmd.Parameters.AddWithValue("@kondisi_pengembalian", txtKondisiPengembalian.Text);
                        cmd.Parameters.AddWithValue("@jumlah_denda", decimal.Parse(txtJumlahDenda.Text));
                        cmd.Parameters.AddWithValue("@alasan_denda", txtAlasanDenda.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    MessageBox.Show("Data berhasil diupdate!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
