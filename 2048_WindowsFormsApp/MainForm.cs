using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _2048_WindowsFormsApp
{
    public partial class MainForm : Form
    {
        private int size = 4;
        private Label[,] labelMap;
        private Random random = new Random();
        private int score = 0;
        public static int maxScore = FileProvider.GetMaxScore();
        public static string userName;

        public Dictionary<int, string> Colors = new Dictionary<int, string>()
        {
            { 2, "#E6E6FA" }, { 4, "#D8BFD8" },{ 8, "#DDA0DD" },{ 16, "#EE82EE" },{ 32, "#DA70D6" },{ 64, "#FF00FF" },{ 128, "#FF00FF" },{ 256, "#BA55D3" },{ 512, "#9370DB" },{ 1024, "#8A2BE2" },{ 2048, "#9400D3" },{ 4096, "#9932CC" },{ 8192, "#8B008B" },{ 16384, "#800080" }
        };
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Auth auth = new Auth();
            auth.ShowDialog();
            InitMap();
            maxScoreLabel2.Text = FileProvider.GetMaxScore().ToString();
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
                List<string> LabelsText = CheckAvailableCells();
                if (LabelsText.Count == size * size)
                {
                    MessageBox.Show("Нет свободных ячеек, начните заново!");
                    break;
                }
                if (labelMap[randCol, randRaw].Text == string.Empty)
                {
                    if (randNumber < size * size * 0.75)
                    {
                        labelMap[randCol, randRaw].Text = "2";
                    }
                    else
                    {
                        labelMap[randCol, randRaw].Text = "4";
                        labelMap[randCol, randRaw].BackColor = System.Drawing.ColorTranslator.FromHtml("#D8BFD8");
                    }
                    break;
                }
            }
        }

        private List<string> CheckAvailableCells()
        {
            List<string> LabelsText = new List<string>();
            foreach (var i in labelMap)
            {
                if (i.Text != string.Empty)
                {
                    LabelsText.Add(i.Text);
                }
            }

            return LabelsText;
        }

        private Label CreateLabel(int x, int y)
        {
            var label = new Label();
            var colPos = 10 + x * 76;
            var rawPos = 70 + y * 76;
            label.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            label.Location = new Point(colPos, rawPos);
            label.BackColor = System.Drawing.ColorTranslator.FromHtml("#E6E6FA");
            label.Size = new Size(70, 70);
            label.TabIndex = 1;
            label.TextAlign = ContentAlignment.MiddleCenter;
            return label;
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteNewUser();
            Application.Exit();
        }

        private void ПравилаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rules rules = new Rules();
            rules.ShowDialog();
        }

        private void РестартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteNewUser();
            foreach (var i in labelMap)
            {
                i.Text = string.Empty;
                i.BackColor = System.Drawing.ColorTranslator.FromHtml("#E6E6FA");
            }
            GenerateCellNumber();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                MoveLeft();
                GenerateCellNumber();
            }


            if (e.KeyCode == Keys.Right)
            {
                MoveRight();
                GenerateCellNumber();
            }

            if (e.KeyCode == Keys.Up)
            {
                MoveUp();
                GenerateCellNumber();
            }

            if (e.KeyCode == Keys.Down)
            {
                MoveDown();
                GenerateCellNumber();
            }
        }

        private void MoveLeft()
        {
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (labelMap[i, j].Text != string.Empty)
                    {
                        for (int k = i + 1; k < size; k++)
                        {
                            if (labelMap[i, j].Text == labelMap[k, j].Text)
                            {
                                var newNumber = int.Parse(labelMap[i, j].Text) * 2;
                                SetScore(newNumber);
                                labelMap[i, j].Text = newNumber.ToString();
                                labelMap[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml(Colors[int.Parse(labelMap[i, j].Text)]);
                                labelMap[k, j].Text = string.Empty;
                                labelMap[k, j].BackColor = System.Drawing.ColorTranslator.FromHtml("#E6E6FA");
                                break;
                            }
                            if (labelMap[k, j].Text != string.Empty)
                            {
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
                                labelMap[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml(Colors[int.Parse(labelMap[i, j].Text)]);
                                labelMap[k, j].Text = string.Empty;
                                labelMap[k, j].BackColor = System.Drawing.ColorTranslator.FromHtml("#E6E6FA");
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveRight()
        {
            for (int i = size - 1; i > 0; i--)
            {
                for (int j = 0; j < size; j++)
                {
                    if (labelMap[i, j].Text != string.Empty)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (labelMap[i, j].Text == labelMap[k, j].Text)
                            {
                                var newNumber = int.Parse(labelMap[i, j].Text) * 2;
                                SetScore(newNumber);
                                labelMap[i, j].Text = newNumber.ToString();
                                labelMap[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml(Colors[int.Parse(labelMap[i, j].Text)]);
                                labelMap[k, j].Text = string.Empty;
                                labelMap[k, j].BackColor = System.Drawing.ColorTranslator.FromHtml("#E6E6FA");
                                break;
                            }
                            if (labelMap[k, j].Text != string.Empty)
                            {
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
                                labelMap[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml(Colors[int.Parse(labelMap[i, j].Text)]);
                                labelMap[k, j].Text = string.Empty;
                                labelMap[k, j].BackColor = System.Drawing.ColorTranslator.FromHtml("#E6E6FA");
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveDown()
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
                                SetScore(newNumber);
                                labelMap[i, j].Text = newNumber.ToString();
                                labelMap[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml(Colors[int.Parse(labelMap[i, j].Text)]);
                                labelMap[i, k].Text = string.Empty;
                                labelMap[i, k].BackColor = System.Drawing.ColorTranslator.FromHtml("#E6E6FA");
                                break;
                            }
                            if (labelMap[k, j].Text != string.Empty)
                            {
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
                                labelMap[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml(Colors[int.Parse(labelMap[i, j].Text)]);
                                labelMap[i, k].Text = string.Empty;
                                labelMap[i, k].BackColor = System.Drawing.ColorTranslator.FromHtml("#E6E6FA");
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveUp()
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
                                SetScore(newNumber);
                                labelMap[i, j].Text = newNumber.ToString();
                                labelMap[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml(Colors[int.Parse(labelMap[i, j].Text)]);
                                labelMap[i, k].Text = string.Empty;
                                labelMap[i, k].BackColor = System.Drawing.ColorTranslator.FromHtml("#E6E6FA");
                                break;
                            }
                            if (labelMap[k, j].Text != string.Empty)
                            {
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
                                labelMap[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml(Colors[int.Parse(labelMap[i, j].Text)]);
                                labelMap[i, k].Text = string.Empty;
                                labelMap[i, k].BackColor = System.Drawing.ColorTranslator.FromHtml("#E6E6FA");
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void SetScore(int newNumber)
        {
            score += newNumber;
            scoreLabel2.Text = score.ToString();
            if (int.Parse(maxScoreLabel2.Text) < int.Parse(scoreLabel2.Text))
            {
                maxScoreLabel2.Text = score.ToString();
                maxScore = score;
            }
        }

        private void СтатистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Results results = new Results();
            results.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteNewUser();
        }

        private void WriteNewUser()
        {
            if (score != 0)
            {
                User user = new User(userName, score);
                FileProvider.WriteData(user);
                FileProvider.WriteMaxScore(maxScore);
            }
        }

        private void Х7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 5;
            ClearLabels();
        }

        private void ClearLabels()
        {
            ClearField();
            InitMap();
            this.CenterToScreen();
        }

        private void Х9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 7;
            ClearLabels();
        }

        private void х4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 4;
            ClearLabels();
        }

        private void ClearField()
        {
            foreach (var i in labelMap)
            {
                Controls.Remove(i);
            }
        }
    }
}
