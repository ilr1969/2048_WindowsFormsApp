using System;
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

        private void loginTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MainForm.userName = loginTextBox.Text;
                this.Close();
            }
        }
    }
}
