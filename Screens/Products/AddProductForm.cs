using PosWInApp.DB;
using System;
using System.IO;
using System.Windows.Forms;

namespace PosWInApp.Screens.Products
{
    public partial class AddProductForm : Form
    {
        PosDatabaseEntities posDB = new PosDatabaseEntities();
        string imagePath ="";
        public AddProductForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductCode.Text == "" || txtProductName.Text == "" || txtProductPrice.Text == "" || comboBox1.SelectedValue == null)
            {
                MessageBox.Show("برجاء ادخال كود و اسم و سعر النتج");
            }
            else
            {
                decimal price;
                int quantity;
                decimal.TryParse(txtProductPrice.Text, out price);
                int.TryParse(txtProductQuantity.Text, out quantity);
                Product p = posDB.Products.Add(new Product
                {
                    Code = txtProductCode.Text,
                    Name = txtProductName.Text,
                    Price = price,
                    Notes = txtProductNotes.Text,
                    Quantity = quantity,
                    Image = imagePath,
                    CategoryId = int.Parse(comboBox1.SelectedValue.ToString())
                }) ;
                posDB.SaveChanges();
                if (imagePath != "")
                {
                    string newImagePath = Environment.CurrentDirectory + "\\images\\products\\" + p.Id + ".png";
                    File.Copy(imagePath, newImagePath,true);
                    p.Image = newImagePath;
                    posDB.SaveChanges();
                }

               

                MessageBox.Show("تم الحفظ");

                txtProductCode.Text = "";
                txtProductName.Text = "";
                txtProductPrice.Text = "";
                txtProductNotes.Text = "";
                txtProductQuantity.Text = "";
                pictureBox1.ImageLocation = "";
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

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'posDatabaseDataSet2.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.posDatabaseDataSet2.Category);

        }
    }
}
