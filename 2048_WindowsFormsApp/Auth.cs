using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_WindowsFormsApp
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void Auth_Load(object sender, EventArgs e)
        {
            this.ActiveControl = loginTextBox;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            MainForm.userName = loginTextBox.Text;
            this.Close();
        }
    }
}
