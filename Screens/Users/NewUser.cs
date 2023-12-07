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

namespace PosWInApp.Screens.Users
{
    public partial class NewUser : Form
    {
        PosDatabaseEntities posDB = new PosDatabaseEntities();
        string imagePath = "";
        public NewUser()
        {
            InitializeComponent();

          
        }

        private void btnAdduser_Click(object sender, EventArgs e)
        {
             
            var u = posDB.Users.Add(new User
            {
                UesrName = txtName.Text,
                Password = txtPassword.Text,
               
            });
            if (imagePath != "")
            {
                string newImagePath = Environment.CurrentDirectory + "\\images\\users\\" + u.Id + ".png";
                u.Image = newImagePath;
                File.Copy(imagePath, newImagePath);
            }

           

            posDB.SaveChanges();

            MessageBox.Show("تم الحفظ");
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
    }
}
