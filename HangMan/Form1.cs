using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        int numberOfBodyPart = 0;

        public HangMan()
        {
            InitializeComponent();
            generateAlphabet();
            answerWord = wordNeedToFind().ToUpper();
            generateWordWithHiddenChars();
            
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.Black, 2);
            GenerateGallows(g);
            GenerateBodyPart(g, p, numberOfBodyPart);
            
        }

        void Check_Win_Lose()
        {
            if(numberOfBodyPart == 7) 
            {
                LoseGame();
            }
            if(!displayWord.Contains("_ "))
            {
                WinGame();
            }
        }

        void GenerateBodyPart(Graphics g, Pen p, int numberOfBodyPart)
        {
            // Head
            if (numberOfBodyPart > 0)              
                g.DrawEllipse(p, 250 - 65 / 2, 70, 65, 65);
            // Body
            if (numberOfBodyPart > 1)
                g.DrawLine(p, new Point(250, 135), new Point(250, 250));
            // Right leg
            if (numberOfBodyPart > 2)
                g.DrawLine(p, new Point(250, 250), new Point(300, 370));
            // Left leg
            if (numberOfBodyPart > 3)
                g.DrawLine(p, new Point(250, 250), new Point(200, 370));
            // Right arm
            if (numberOfBodyPart > 4)
                g.DrawLine(p, new Point(250, 150), new Point(300, 250));
            // Left arm
            if (numberOfBodyPart > 5)
                g.DrawLine(p, new Point(250, 150), new Point(200, 250));
            // Dead
            if (numberOfBodyPart > 6)
            {
                g.DrawLine(p, new Point(210, 145), new Point(290, 145));
            }
        }

        void GenerateGallows(Graphics g) // Gallows: sibenice/gia treo co
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
            btn.Enabled = false;

            if (!openHidenChar(btn.Text))
            {
                numberOfBodyPart++;
                canvas.Refresh();
            }
            Check_Win_Lose();
        }

        bool openHidenChar(string choosenChar)
        {
            bool charFound = false;

            for (int i = 0; i < answerWord.Length; i++)
            {
                if (choosenChar == RemoveDiacritics(answerWord[i].ToString()))
                {
                    displayWord[i] = answerWord[i] + " ";
                    charFound = true;
                }
            }
            ourWord.Text = String.Join("", displayWord.ToArray());

            return charFound;
        }
       

        void generateWordWithHiddenChars()
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
            ourWord.Text = ourWord.Text;
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
            int difficulty;
            try
            {
                using (var sr = File.OpenText(@"dificulty.txt"))
                {
                    difficulty = int.Parse(sr.ReadLine());
                }
            }
            catch
            {
                difficulty = 0;
            }
                     
            readWordFromFile(difficulty);
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
        
        void readWordFromFile(int difficulty)
        {
            switch (difficulty)
            {
                case 0: // easy
                    using (StreamWriter sw = File.CreateText(@"ourWords.txt"))
                    {
                        sw.WriteLine("MATFYZ");
                        sw.WriteLine("PRAHA");
                    }
                    break;
                case 1: // medium
                    using (StreamWriter sw = File.CreateText(@"ourWords.txt"))
                    {
                        sw.WriteLine("POČÍTAČ");
                        sw.WriteLine("INTERNET");
                    }
                    break;
                case 2: // hard
                    using (StreamWriter sw = File.CreateText(@"ourWords.txt"))
                    {
                        sw.WriteLine("PROGRAMOVÁNÍ");
                        sw.WriteLine("ČESKÁ REPUBLIKA");
                    }
                    break;

            }
        }

        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        void LoseGame()
        {
            DialogResult result = MessageBox.Show("Prohráli jste! Slovíčko bylo:\n" + answerWord + "\nChcete hrát znovu?", "Konec Hry", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                showMenu();
            }
            if (result == DialogResult.Yes)
            {
                HangMan hangMan = new HangMan();
                this.Hide();
                hangMan.Show();
            }
        }

        void WinGame()
        {
            DialogResult result = MessageBox.Show("Gratuluji, Vyhrali jste! Slovíčko bylo:\n" + answerWord + "\nChcete hrát znovu?", "Konec Hry", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                showMenu();
            }
            if (result == DialogResult.Yes)
            {
                HangMan hangMan = new HangMan();
                this.Hide();
                hangMan.Show();
            }
        }

        private void HangMan_FormClosing(object sender, FormClosingEventArgs e)
        {
            showMenu();
        }


        void showMenu()
        {
            this.Hide();
            Menu m = new Menu();
            m.Show();
        }
    }
}