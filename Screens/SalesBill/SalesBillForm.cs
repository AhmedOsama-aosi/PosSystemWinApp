using PosWInApp.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace PosWInApp.Screens.SalesBill
{
    public partial class SalesBillForm : Form
    {
        PosDatabaseEntities posDB = new PosDatabaseEntities();
        List<Product> products;
        public SalesBillForm()
        {
            InitializeComponent();
            comboBox1.DataSource = posDB.Customers.Where(x => x.IsActive == true).ToList();
            comboBox1.SelectedText = "Name";
            comboBox1.SelectedValue = "Id";
            lblUser.Text = PosWInApp.Users.Name;
            products = posDB.Products.ToList();

            listView1.View = View.LargeIcon;

            listView1.Columns.Add("image", 150);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            imageList1.ImageSize = new Size(70, 70);
            List<ListViewItem> li = new List<ListViewItem>();

            try
            {
                foreach (var item in products)
                {
                    imageList1.Images.Add(Image.FromFile(item.Image));
                    ListViewItem listViewItem = new ListViewItem(item.Name, imageList1.Images.Count - 1);
                    listViewItem.Tag = item;
                    li.Add(listViewItem);

                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

            listView1.LargeImageList = imageList1;

            listView1.Items.AddRange(li.ToArray());



        }

        private void listView1_Click(object sender, EventArgs e)
        {
           
            Product product = (Product)listView1.SelectedItems[0].Tag;
            int rowIndex = -1;
           

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString() == product.Id.ToString())
                {
                    rowIndex = row.Index;
                    break;
                }
            }



            if (rowIndex == -1)
            {
                dataGridView1.Rows.Add(product.Id, product.Name, product.Price, 1);
            }
            else
            {
                dataGridView1.Rows[rowIndex].Cells["quantity"].Value = int.Parse(dataGridView1.Rows[rowIndex].Cells["quantity"].Value.ToString()) + 1;

            }



            lblTotalValue.Text = GetTotal().ToString();



        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            double discount;
            double.TryParse(txtDiscount.Text, out discount);
            lblAfterDiscount.Text = (double.Parse(lblTotalValue.Text) - discount).ToString();
        }

        private void txtPaid_TextChanged(object sender, EventArgs e)
        {
            double paid;
            double.TryParse(txtPaid.Text, out paid);
            txtRest.Text = (paid - double.Parse(lblAfterDiscount.Text)).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DB.SalesBill salesBill = new DB.SalesBill()
            {
                total = decimal.Parse(lblAfterDiscount.Text),
                date = dateTimePicker1.Value,
                CustomerID = (int)comboBox1.SelectedValue,
                discount = decimal.Parse(txtDiscount.Text),

                notes = txtNotes.Text,
                userId = PosWInApp.Users.Id,


            };

            List<SalesBillProduct> products = new List<SalesBillProduct>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                products.Add(new SalesBillProduct()
                {
                    productId = int.Parse(row.Cells["Id"].Value.ToString()),
                    productPrice = decimal.Parse(row.Cells["price"].Value.ToString()),
                    quantity = int.Parse(row.Cells["quantity"].Value.ToString())

                });

            }
            salesBill.SalesBillProducts = products;
            posDB.SalesBills.Add(salesBill);
            posDB.SaveChanges();



            MessageBox.Show("تم الحفظ");

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            lblTotalValue.Text = GetTotal().ToString();
        }
        

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblTotalValue.Text = GetTotal().ToString();
        }

        public double GetTotal()
        {
            double sum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                sum += double.Parse(row.Cells["quantity"].Value.ToString()) * double.Parse(row.Cells["price"].Value.ToString());
            }
            return sum;
        }
    }
}
