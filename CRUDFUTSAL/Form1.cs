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
        private string connectionString = "Data Source=DAEN\\PIYOPUYU;Initial Catalog=Bfutsal;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }


      

        




        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Penyewa penyewaForm = new Penyewa();
            penyewaForm.Show();
        }

        private void btnItemSewa_Click(object sender, EventArgs e)
        {
            Item_Sewa itemSewaForm = new Item_Sewa();
            itemSewaForm.Show();
        }

        private void btnPeminjaman_Click(object sender, EventArgs e)
        {
            Peminjaman peminjamanForm = new Peminjaman();
            peminjamanForm.Show();
        }

        private void btnPengembalian_Click(object sender, EventArgs e)
        {
            Pengembalian pengembalianForm = new Pengembalian();
            pengembalianForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
