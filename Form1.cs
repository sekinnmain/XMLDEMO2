using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace XMLDEMO
{

    public partial class Form1 : Form
    {
        static AppData db;
        protected static AppData App
        {
            get
            {
                if (db == null)
                    db = new AppData();
                return db;
            }
        }
        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string fileName = string.Format("{0}//Data.dat", Application.StartupPath);
            if (File.Exists(fileName))
                App.Employee.ReadXml(fileName);
            employeeBindingSource.DataSource = App.Employee;
        }

        private void Button1Save_Click(object sender, EventArgs e)
        {
            try {
                employeeBindingSource.EndEdit();
                App.Employee.AcceptChanges();
                App.Employee.WriteXml(string.Format("{0}//Data.dat", Application.StartupPath));
            }
            

             catch (Exception message)
            {
                MessageBox.Show(message.Message);
                App.Employee.RejectChanges();
            }
        }

        private void Button1New_Click(object sender, EventArgs e)
        {
            try
            {
                App.Employee.AddEmployeeRow(App.Employee.NewEmployeeRow());
                employeeBindingSource.MoveLast();
                textBox1Name.Focus();
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
                App.Employee.RejectChanges();
            }
        }

        private void Button2Edit_Click(object sender, EventArgs e)
        {
            textBox1Name.Focus();
        }

        private void Button3Cancel_Click(object sender, EventArgs e)
        {
            employeeBindingSource.ResumeBinding();
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(MessageBox.Show("Do Delete?","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    employeeBindingSource.RemoveCurrent();
                }
            }
        }
    }
}
