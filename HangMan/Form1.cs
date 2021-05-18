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

namespace HangMan
{
    public partial class HangMan : Form
    {
        Label ourWord = new Label();
        List<string> displayWord = new List<string>();
        string answerWord;
        

        public HangMan()
        {
            InitializeComponent();
            generateAlphabet();
            answerWord = wordNeedToFind();
            generateWord();
            
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.Black, 2);
            GenerateGallows(g);

            // Head
            g.DrawEllipse(p, 250 - 65 / 2, 70, 65, 65);
            // Body
            g.DrawLine(p, new Point(250, 135), new Point(250, 250));
            // Right leg
            g.DrawLine(p, new Point(250, 250), new Point(300, 370));
            // Left leg
            g.DrawLine(p, new Point(250, 250), new Point(200, 370));
            // Right arm
            g.DrawLine(p, new Point(250, 150), new Point(300, 250));
            // Left arm
            g.DrawLine(p, new Point(250, 150), new Point(200, 250));
            // Dead
            g.DrawLine(p, new Point(210, 145), new Point(290, 145));

        }

        void GenerateGallows(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            // Phan duoi
            g.DrawLine(p, new Point(30, canvas.Height - 30), new Point(200, canvas.Height - 30));
            // Coc
            g.DrawLine(p, new Point(80, 30), new Point(80, canvas.Height - 30));
            // Coc ngang
            g.DrawLine(p, new Point(80, 30), new Point(250, 30));
            // Coc cheo
            g.DrawLine(p, new Point(80, 80), new Point(130, 30));
            // Coc treo
            g.DrawLine(p, new Point(250, 30), new Point(250, 70));
        }

        void generateAlphabet()
        {
            List<string> alphabetCharacter = new List<string>()
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V","W", "X", "Y", "Z"
        };
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Button btn = new Button()
                    {
                        Width = 40,
                        Height = 40,
                        Location = new Point(10 + j * 40, 10 + i * 40),
                        Text = alphabetCharacter[0]
                    };
                    btn.Click += alphabetCharacter_Click;
                    pnl_alphabet.Controls.Add(btn);
                    alphabetCharacter.RemoveAt(0);
                }
            }
        }

        void alphabetCharacter_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            for (int i = 0; i < answerWord.Length; i++)
            {
                if(btn.Text == answerWord[i].ToString())
                {
                    displayWord[i] = btn.Text + " ";
                }
            }

            ourWord.Text = String.Join("", displayWord.ToArray());
        }

        void generateWord()
        {
            for (int i = 0; i < answerWord.Length; i++)
            {
                if (answerWord[i].ToString() == " ")
                {
                    ourWord.Text += "   ";
                    displayWord.Add("   ");
                }
                else
                {
                    ourWord.Text += "_ ";
                    displayWord.Add("_ ");
                }

            }
            ourWord.Text = ourWord.Text.Remove(ourWord.Text.Length - 1);
            ourWord.Font = new Font("Arial", 20);
            ourWord.Size = new Size(panel1.Width, panel1.Height);
            ourWord.TextAlign = ContentAlignment.MiddleCenter;
            ourWord.Location = new Point((panel1.Width - ourWord.Width) / 2, (panel1.Height - ourWord.Height) / 2);
            ourWord.Anchor = AnchorStyles.None;
            panel1.Controls.Add(ourWord);
            this.Controls.Add(panel1);
        }

        string wordNeedToFind()
        {
            int level = 2;
            readWordFromFile(level);
            return randomWordFromFile();
        }

        string randomWordFromFile()
        {
            List<string> ourWord = new List<string>();
            using (var sr = File.OpenText(@"ourWords.txt"))
            {
                string word = sr.ReadLine();
                while (word != null)
                {
                    ourWord.Add(word);
                    word = sr.ReadLine();
                }
            }

            Random rd = new Random();
            return ourWord[rd.Next(ourWord.Count)];
        }
        
        void readWordFromFile(int level)
        {
            switch (level)
            {
                case 0:
                    using (StreamWriter sw = File.CreateText(@"ourWords.txt"))
                    {
                        sw.WriteLine("MATFYZ");
                        sw.WriteLine("PRAHA");
                    }
                    break;
                case 1:
                    using (StreamWriter sw = File.CreateText(@"ourWords.txt"))
                    {
                        sw.WriteLine("POČÍTAČ");
                        sw.WriteLine("INTERNET");
                    }
                    break;
                case 2:
                    using (StreamWriter sw = File.CreateText(@"ourWords.txt"))
                    {
                        sw.WriteLine("PROGRAMOVÁNÍ");
                        sw.WriteLine("ČESKÁ REPUBLIKA");
                    }
                    break;

            }
        }
    }
}