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
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
        }

        private void Results_Load(object sender, EventArgs e)
        {
            var userList = FileProvider.GetData();
            for (int i = 0; i<userList.Count; i++)
            {
                resultsDataGridView.Rows.Add();
                resultsDataGridView.Rows[i].Cells[0].Value = userList[i].user;
                resultsDataGridView.Rows[i].Cells[1].Value = userList[i].score;
            }
        }
    }
}
