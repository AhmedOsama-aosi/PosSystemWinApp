using PosWInApp.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PosWInApp
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PosDatabaseEntities posDB = new PosDatabaseEntities();
             var result = posDB.Users.FirstOrDefault(x => x.UesrName == txtUserName.Text && x.Password == txtUserPassword.Text);


            if(result != null)
            {
                Users.Name = result.UesrName;
                Users.Id = result.Id;
                Thread th = new Thread(openForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                Close();
            }
            else
            {
                MessageBox.Show("يوجد خطأ في اسم المستخدم او كلمة المرور");
            }

           
        }
        void openForm()
        {
            Application.Run(new MainForm());
        }
    }
    static class Users
    {
        static public string Name { get; set; }
        static public int Id { get; set; }
    }
}
