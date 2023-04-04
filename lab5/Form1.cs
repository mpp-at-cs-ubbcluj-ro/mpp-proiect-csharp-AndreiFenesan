using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab5.servicies;

namespace lab5
{
    public partial class Form1 : Form
    {
        private IService Service { get; set; }

        public Form1(IService service)
        {
            this.Service = service;
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            String username = this.usernameText.Text;
            String password = this.passwordText.Text;
            if (this.Service.AuthenticateVolunteer(username, password))
            {
                MainForm mainForm = new MainForm(this.Service);
                this.Close();
                mainForm.Show();

            }
            else
            {
                this.incorectLabel.Visible = true;
            }
        }
    }
}