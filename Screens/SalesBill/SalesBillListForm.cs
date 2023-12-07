using PosWInApp.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PosWInApp.Screens.SalesBill
{
    public partial class SalesBillListForm : Form
    {
        PosDatabaseEntities posDB = new PosDatabaseEntities();

        public SalesBillListForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = posDB.SalesBills.Select(x=> new {x.Id,x.total,x.discount, after_discount  =x.total-x.discount,x.User.UesrName,x.Customer.Name}).ToList();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
            var bill = posDB.SalesBills.FirstOrDefault(x => x.Id == id);
            txtBillId.Text = bill.Id.ToString();
            comboBox1.Text = bill.Customer.Name;
            txtNotes.Text = bill.notes;
            dateTimePicker1.Value = bill.date.Value;
            dataGridView2.Rows.Clear();
            foreach (var item in bill.SalesBillProducts)
            {
                dataGridView2.Rows.Add(item.productId,item.Product.Name,item.productPrice,item.quantity);
            }
           
            //dataGridView2.DataSource = posDB.SalesBillProducts.Where(x => x.salesBillId == id ).Select(x=> new { x.productId ,x.Product.Name,x.productPrice,x.quantity}).ToList();
        }
    }
}
