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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Pengembalian (IDPeminjaman, TanggalPengembalian, DendaKeterlambatan, KondisiPengembalian, JumlahDenda, AlasanDenda) " +
                               "VALUES (@IDPeminjaman, @TanggalPengembalian, @DendaKeterlambatan, @KondisiPengembalian, @JumlahDenda, @AlasanDenda)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDPeminjaman", cmbIDPeminjaman.Text);
                cmd.Parameters.AddWithValue("@TanggalPengembalian", TanggalPengembalian.Value);
                cmd.Parameters.AddWithValue("@DendaKeterlambatan", txtDendaKeterlambatan.Text);
                cmd.Parameters.AddWithValue("@KondisiPengembalian", txtKondisi.Text);
                cmd.Parameters.AddWithValue("@JumlahDenda", txtJumlahDenda.Text);
                cmd.Parameters.AddWithValue("@AlasanDenda", txtAlasan.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Data berhasil ditambahkan!");
            LoadData();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Pengembalian WHERE ID=@ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", id);

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
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Pengembalian SET IDPeminjaman=@IDPeminjaman, TanggalPengembalian=@TanggalPengembalian, DendaKeterlambatan=@DendaKeterlambatan, " +
                                   "KondisiPengembalian=@KondisiPengembalian, JumlahDenda=@JumlahDenda, AlasanDenda=@AlasanDenda WHERE ID=@ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IDPeminjaman", cmbIDPeminjaman.Text);
                    cmd.Parameters.AddWithValue("@TanggalPengembalian", TanggalPengembalian.Value);
                    cmd.Parameters.AddWithValue("@DendaKeterlambatan", txtDendaKeterlambatan.Text);
                    cmd.Parameters.AddWithValue("@KondisiPengembalian", txtKondisi.Text);
                    cmd.Parameters.AddWithValue("@JumlahDenda", txtJumlahDenda.Text);
                    cmd.Parameters.AddWithValue("@AlasanDenda", txtAlasan.Text);
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Data berhasil diupdate!");
                LoadData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
