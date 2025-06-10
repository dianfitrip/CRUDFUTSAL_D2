using Microsoft.Reporting.WinForms;
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
    public partial class FormReport: Form
    {
        private string connectionString = "Data Source=DAEN\\PIYOPUYU;Initial Catalog=Booking_futsal;Integrated Security=True";


        public FormReport()
        {
            InitializeComponent();
        }

        private void FormReport_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            string query = @"
                SELECT 
                    py.nama_penyewa,
                    pm.id_peminjaman,
                    it.nama_item,
                    pm.tanggal_pinjam,
                    pg.tanggal_pengembalian,
                    pg.tanggal_jatuh_tempo,
                    pm.jumlah,
                    pg.jumlah_total,
                    pg.jumlah_denda
                FROM 
                    Pengembalian pg
                INNER JOIN 
                    Peminjaman pm ON pg.id_peminjaman = pm.id_peminjaman
                INNER JOIN 
                    Penyewa py ON pm.id_penyewa = py.id_penyewa
                INNER JOIN 
                    Item_Sewa it ON pm.id_item = it.id_item";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ReportDataSource rds = new ReportDataSource("DataSet1", dt); // Nama DataSet harus sama dengan yang di RDLC

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Ganti path ini ke lokasi file ReportFutsal.rdlc di PC kamu jika perlu
            reportViewer1.LocalReport.ReportPath = @"D:\SEMESTER 4\CRUDFUTSALp\CRUDFUTSAL\CRUDFUTSAL\CRUDFUTSAL\ReportFutsal.rdlc";

            reportViewer1.RefreshReport();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
    
}
