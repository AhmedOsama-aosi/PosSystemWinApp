using PosWInApp.Screens.Custormers;
using PosWInApp.Screens.Products;
using PosWInApp.Screens.SalesBill;
using System;
using System.Windows.Forms;

namespace PosWInApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            lblUser.Text = Users.Name;
        }

        private void ادارةالمنتجاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductsForm products = new ProductsForm();
            products.ShowDialog();
        }

        private void أدارةالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomersForm customers = new CustomersForm();
            customers.ShowDialog();
        }

        private void فاتورةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesBillForm salesBillForm = new SalesBillForm();
            salesBillForm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SalesBillForm salesBillForm = new SalesBillForm();
            salesBillForm.ShowDialog();
        }

        private void الفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesBillListForm salesBillListForm = new SalesBillListForm();
            salesBillListForm.ShowDialog();
        }
    }
}
