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
    
    public partial class Form1: Form
    {
        private string connectionString = "Data Source=LAPTOP-I4P54CK5\\TYAS;Initial Catalog=Booking_futsal;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }


      

        




        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Penyewa formpenyewa = new Penyewa();
            formpenyewa.Show();

            this.Hide();
        }

        private void btnItemSewa_Click(object sender, EventArgs e)
        {

            Item_Sewa formitemsewa = new Item_Sewa();
            formitemsewa.Show();

            this.Hide();
        }

        private void btnPeminjaman_Click(object sender, EventArgs e)
        {
            Peminjaman formpeminjaman = new Peminjaman();
            formpeminjaman.Show();

            this.Hide();
        }

        private void btnPengembalian_Click(object sender, EventArgs e)
        {
            Pengembalian formpengembalian = new Pengembalian();
            formpengembalian.Show();

            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormReport formReport = new FormReport();
            formReport.Show();

            this.Hide();
        }
    }
}
