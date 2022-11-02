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
        private int score = 0;
        public static int maxScore = FileProvider.GetMaxScore();
        public static string userName;
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
            label.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            label.Location = new Point(colPos, rawPos);
            label.BackColor = SystemColors.Info;
            label.Size = new Size(70, 70);
            label.TabIndex = 1;
            label.TextAlign = ContentAlignment.MiddleCenter;
            return label;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User user = new User(userName, score);
            FileProvider.WriteData(user);
            FileProvider.WriteMaxScore(maxScore);
            this.Close();
        }

        private void правилаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rules rules = new Rules();
            rules.ShowDialog();
        }

        private void рестартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User user = new User(userName, score);
            FileProvider.WriteData(user);
            FileProvider.WriteMaxScore(maxScore);
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
                for (int i = 0; i < size - 1; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (labelMap[i, j].Text != string.Empty && labelMap[i, j].Text == labelMap[i + 1, j].Text)
                        {
                            var newNumber = int.Parse(labelMap[i, j].Text) * 2;
                            SetScore(newNumber);
                            labelMap[i, j].Text = newNumber.ToString();
                            labelMap[i + 1, j].Text = string.Empty;
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
                for (int i = size - 1; i > 0; i--)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (labelMap[i, j].Text != string.Empty && labelMap[i, j].Text == labelMap[i - 1, j].Text)
                        {
                            var newNumber = int.Parse(labelMap[i, j].Text) * 2;
                            SetScore(newNumber);
                            labelMap[i, j].Text = newNumber.ToString();
                            labelMap[i - 1, j].Text = string.Empty;
                            break;
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
                    for (int j = 0; j < size - 1; j++)
                    {
                        if (labelMap[i, j].Text != string.Empty && labelMap[i, j].Text == labelMap[i, j + 1].Text)
                        {
                            var newNumber = int.Parse(labelMap[i, j].Text) * 2;
                            SetScore(newNumber);
                            labelMap[i, j].Text = newNumber.ToString();
                            labelMap[i, j + 1].Text = string.Empty;
                            break;
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
                    for (int j = size - 1; j > 0; j--)
                    {
                        if (labelMap[i, j].Text != string.Empty && labelMap[i, j].Text == labelMap[i, j - 1].Text)
                        {
                            var newNumber = int.Parse(labelMap[i, j].Text) * 2;
                            SetScore(newNumber);
                            labelMap[i, j].Text = newNumber.ToString();
                            labelMap[i, j - 1].Text = string.Empty;
                            break;
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

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Results results = new Results();
            results.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            User user = new User(userName, score);
            FileProvider.WriteData(user);
            FileProvider.WriteMaxScore(maxScore);
        }

        private void х7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 5;
            ClearField();
            InitMap();
        }

        private void х9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 7;
            ClearField();
            InitMap();
        }

        private void х4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 4;
            ClearField();
            InitMap();
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
