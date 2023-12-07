using PosWInApp.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PosWInApp.Screens.Custormers
{
    public partial class CustomersForm : Form
    {
        PosDatabaseEntities posDB = new PosDatabaseEntities();
        string imagePath = "";
        Customer c;
        int pageNumber = 0;
        bool orderingtype = true;
        public CustomersForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = posDB.Customers.ToList();
           
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'posDatabaseDataSet1.Customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.Fill(this.posDatabaseDataSet1.Customer);

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                imagePath = "";
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                c = posDB.Customers.FirstOrDefault(x => x.Id == id);

                txtCustomerName.Text = c.Name;
                txtCustomerPhone.Text = c.Phone;
                txtCustomerAddress.Text = c.address;
                txtCustomerEmail.Text = c.email;
                txtCustomerCompany.Text = c.Company;
                txtCustomerNotes.Text = c.Notes;
                if (c.IsActive == null)
                {
                    c.IsActive = false;
                }
                checkBox1.Checked = (bool)c.IsActive;
                pictureBox1.ImageLocation = c.Image;
            }
            catch (Exception)
            {


            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "" || txtCustomerPhone.Text == "")
            {
                MessageBox.Show("برجاء ادخال كود و اسم و سعر النتج");
            }
            else
            {
               

                c.Name = txtCustomerName.Text;
                c.Phone= txtCustomerPhone.Text ;
                c.address= txtCustomerAddress.Text ;
                c.email= txtCustomerEmail.Text ;
                c.Company = txtCustomerCompany.Text;
                c.Notes =   txtCustomerNotes.Text;
              

                if (imagePath != "")
                {

                    string newImagePath = Environment.CurrentDirectory + "\\images\\customers\\" + c.Id + ".png";

                    File.Copy(imagePath, newImagePath, true);
                    c.Image = newImagePath;
                }

                posDB.SaveChanges();

                MessageBox.Show("تم الحفظ");
                dataGridView1.DataSource = posDB.Customers.ToList();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = posDB.Customers.Where(x => x.Name == txtSearch.Text).ToList();

        }

        private void btnRefrash_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = posDB.Customers.ToList();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(" هل ترغب بحذف  " + c.Name, "تأكيد عملية الحذق", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string removedItemName = c.Name;
                posDB.Customers.Remove(c);

                posDB.SaveChanges();
                dataGridView1.DataSource = posDB.Customers.ToList();
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
            if (orderingtype)
            {
                dataGridView1.DataSource = posDB.Customers.OrderBy(x => x.Name).Skip(pageNumber * 10).Take(10).ToList();

            }
            else
            {
                dataGridView1.DataSource = posDB.Customers.OrderByDescending(x => x.Name).Skip(pageNumber * 10).Take(10).ToList();

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            pageNumber++;
            lblPageNumber.Text = (pageNumber + 1).ToString();
            if (orderingtype)
            {
                dataGridView1.DataSource = posDB.Customers.OrderBy(x => x.Name).Skip(pageNumber * 10).Take(10).ToList();

            }
            else
            {
                dataGridView1.DataSource = posDB.Customers.OrderByDescending(x => x.Name).Skip(pageNumber * 10).Take(10).ToList();

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
