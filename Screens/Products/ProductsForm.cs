using PosWInApp.DB;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PosWInApp.Screens.Products
{
    public partial class ProductsForm : Form
    {
        PosDatabaseEntities posDB = new PosDatabaseEntities();
        string imagePath = "";
        Product p;
        int pageNumber = 0;
        bool orderingtype = true;
        public ProductsForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = posDB.Products.ToList();
            lblPageNumber.Text = (pageNumber+1).ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm();
            addProductForm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtProductCode.Text == "" || txtProductName.Text == "" || txtProductPrice.Text == "")
            {
                MessageBox.Show("برجاء ادخال كود و اسم و سعر النتج");
            }
            else
            {
                decimal price;
                int quantity;
                decimal.TryParse(txtProductPrice.Text, out price);
                int.TryParse(txtProductQuantity.Text, out quantity);

                p.Code = txtProductCode.Text;
                p.Name = txtProductName.Text;
                p.Price = price;
                p.Notes = txtProductNotes.Text;
                p.Quantity = quantity;
                p.CategoryId = int.Parse(comboBox1.SelectedValue.ToString());


                if (imagePath != "")
                {

                    string newImagePath = Environment.CurrentDirectory + "\\images\\products\\" + p.Id + ".png";

                    File.Copy(imagePath, newImagePath, true);
                    p.Image = newImagePath;
                }

                posDB.SaveChanges();

                MessageBox.Show("تم الحفظ");
                dataGridView1.DataSource = posDB.Products.ToList();
            }
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'posDatabaseDataSet3.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.posDatabaseDataSet3.Category);
            // TODO: This line of code loads data into the 'posDatabaseDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.posDatabaseDataSet.Product);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = posDB.Products.Where(x => x.Code == txtSearch.Text).ToList();
        }

        private void btnRefrash_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = posDB.Products.ToList();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                imagePath = "";
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                p = posDB.Products.FirstOrDefault(x => x.Id == id);

                txtProductCode.Text = p.Code;
                txtProductName.Text = p.Name;
                txtProductPrice.Text = p.Price.ToString();
                txtProductNotes.Text = p.Notes;
                txtProductQuantity.Text = p.Quantity.ToString();
                pictureBox1.ImageLocation = p.Image;
                comboBox1.SelectedValue = p.CategoryId;
            }
            catch (Exception)
            {

               
            }
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = dialog.FileName;
                pictureBox1.ImageLocation = imagePath;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
          var result =  MessageBox.Show(" هل ترغب بحذف  "+ p.Name,"تأكيد عملية الحذق", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string removedItemName = p.Name;
                posDB.Products.Remove(p);

                posDB.SaveChanges();
                dataGridView1.DataSource = posDB.Products.ToList();
                MessageBox.Show(" تم حذف " + removedItemName);
            }
          


        }

        private void btnPervious_Click(object sender, EventArgs e)
        {
            pageNumber--;

            if (pageNumber < 0)
            {
                pageNumber = 0;
            }
           
            lblPageNumber.Text = (pageNumber + 1).ToString();
            if(orderingtype)
            {
                dataGridView1.DataSource = posDB.Products.OrderBy(x => x.Name).Skip(pageNumber * 10).Take(10).ToList();

            }
            else
            {
                dataGridView1.DataSource = posDB.Products.OrderByDescending(x => x.Name).Skip(pageNumber * 10).Take(10).ToList();

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            pageNumber++;
            lblPageNumber.Text = (pageNumber + 1).ToString();
            if (orderingtype)
            {
                dataGridView1.DataSource = posDB.Products.OrderBy(x => x.Name).Skip(pageNumber * 10).Take(10).ToList();

            }
            else
            {
                dataGridView1.DataSource = posDB.Products.OrderByDescending(x => x.Name).Skip(pageNumber * 10).Take(10).ToList();

            }

        }

        private void cbAsc_Click(object sender, EventArgs e)
        {
            if (orderingtype)
            {
                cbDesc.Checked = false;
                orderingtype = !orderingtype;
            }
          

        }

        private void cbDesc_Click(object sender, EventArgs e)
        {
            if (!orderingtype)
            {
                cbAsc.Checked = false;
                orderingtype = !orderingtype;
            }
        }
    }
}
