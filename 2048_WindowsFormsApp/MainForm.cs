using System;
using System.Drawing;
using System.Windows.Forms;

namespace _2048_WindowsFormsApp
{
    public partial class MainForm : Form
    {
        private int size = 4;
        private Label[,] labelMap;
        Random random = new Random();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitMap();
        }

        private void InitMap()
        {
            labelMap = new Label[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var newLabel = CreateLabel(i, j);
                    Controls.Add(newLabel);
                    labelMap[i, j] = newLabel;
                }
            }
            GenerateCellNumber();
        }

        private void GenerateCellNumber()
        {
            while (true)
            {
                var randNumber = random.Next(size * size);
                var randCol = randNumber / size;
                var randRaw = randNumber % size;
                if (labelMap[randCol, randRaw].Text == string.Empty)
                {
                    if (randNumber < size * size * 0.75)
                    {
                        labelMap[randCol, randRaw].Text = "2";
                    }
                    else
                    {
                        labelMap[randCol, randRaw].Text = "4";
                    }
                    break;
                }
            }
        }

        private Label CreateLabel(int x, int y)
        {
            var label = new Label();
            var colPos = 10 + x * 76;
            var rawPos = 70 + y * 76;
            label.Font = new Font("Microsoft Sans Serif", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label.Location = new Point(colPos, rawPos);
            label.BackColor = System.Drawing.SystemColors.Info;
            label.Size = new Size(70, 70);
            label.TabIndex = 1;
            label.TextAlign = ContentAlignment.MiddleCenter;
            return label;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void правилаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rules rules = new Rules();
            rules.ShowDialog();
        }

        private void рестартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var i in labelMap)
            {
                i.Text = "";
            }
            GenerateCellNumber();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (labelMap[i, j].Text != string.Empty)
                        {
                            for (int k = i + 1; k < size; k++)
                            {
                                if (labelMap[k, j].Text == labelMap[i, j].Text)
                                {
                                    var newNumber = int.Parse(labelMap[i, j].Text) * 2;
                                    labelMap[i, j].Text = newNumber.ToString();
                                    labelMap[k, j].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (labelMap[i, j].Text == string.Empty)
                        {
                            for (int k = i + 1; k < size; k++)
                            {
                                if (labelMap[k, j].Text != string.Empty)
                                {
                                    labelMap[i, j].Text = labelMap[k, j].Text;
                                    labelMap[k, j].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
                GenerateCellNumber();
            }

            if (e.KeyCode == Keys.Right)
            {
                for (int i = size - 1; i >= 0; i--)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (labelMap[i, j].Text != string.Empty)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (labelMap[k, j].Text == labelMap[i, j].Text)
                                {
                                    var newNumber = int.Parse(labelMap[i, j].Text) * 2;
                                    labelMap[i, j].Text = newNumber.ToString();
                                    labelMap[k, j].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int i = size - 1; i > 0; i--)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (labelMap[i, j].Text == string.Empty)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (labelMap[k, j].Text != string.Empty)
                                {
                                    labelMap[i, j].Text = labelMap[k, j].Text;
                                    labelMap[k, j].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }

                }
                GenerateCellNumber();
            }

            if (e.KeyCode == Keys.Up)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (labelMap[i, j].Text != string.Empty)
                        {
                            for (int k = j + 1; k < size; k++)
                            {
                                if (labelMap[i, j].Text == labelMap[i, k].Text)
                                {
                                    var newNumber = int.Parse(labelMap[i, j].Text) * 2;
                                    labelMap[i, j].Text = newNumber.ToString();
                                    labelMap[i, k].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (labelMap[i, j].Text == string.Empty)
                        {
                            for (int k = j + 1; k < size; k++)
                            {
                                if (labelMap[i, k].Text != string.Empty)
                                {
                                    labelMap[i, j].Text = labelMap[i, k].Text;
                                    labelMap[i, k].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
                GenerateCellNumber();
            }

            if (e.KeyCode == Keys.Down)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = size - 1; j >= 0; j--)
                    {
                        if (labelMap[i, j].Text != string.Empty)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (labelMap[i, j].Text == labelMap[i, k].Text)
                                {
                                    var newNumber = int.Parse(labelMap[i, j].Text) * 2;
                                    labelMap[i, j].Text = newNumber.ToString();
                                    labelMap[i, k].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < size; i++)
                {
                    for (int j = size - 1; j >= 0; j--)
                    {
                        if (labelMap[i, j].Text == string.Empty)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (labelMap[i, k].Text != string.Empty)
                                {
                                    labelMap[i, j].Text = labelMap[i, k].Text;
                                    labelMap[i, k].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
                GenerateCellNumber();
            }
        }
    }
}
