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

            DrawPicture dp = new DrawPicture();
            dp.GenerateGallows(g, canvas.Height);
            dp.GenerateBodyPart(g, numberOfBodyPart);
            
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
                     
            createWordFile(difficulty);
            return randomWordFromFile();
        }

        string randomWordFromFile()
        {
            var words = File.ReadAllLines(@"ourWords.txt");
            var r = new Random();
            var randomLineNumber = r.Next(0, words.Length);
            string word = words[randomLineNumber];

            return word;
        }
        
        void createWordFile(int difficulty)
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