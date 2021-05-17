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
        
        public HangMan()
        {
            InitializeComponent();
            generateAlphabet();

            ReadWordFromFile();

            generateWord();

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.Black, 2);
            GenerateGallows(g);

            // Head
            g.DrawEllipse(p, 250 - 65/2, 70, 65, 65);
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
            
        }

        void generateWord()
        {
            Label ourWord = new Label();
            ourWord.Name = "ourWord";
            ourWord.Text = "_ U O _   _ _ _   _ _ _";
            ourWord.Font = new Font("Arial", 20);
            ourWord.Size = new Size(panel1.Width, panel1.Height);
            ourWord.TextAlign = ContentAlignment.MiddleCenter;
            ourWord.Location = new Point((panel1.Width - ourWord.Width) / 2, (panel1.Height - ourWord.Height) / 2);
            ourWord.Anchor = AnchorStyles.None;
            panel1.Controls.Add(ourWord);
            this.Controls.Add(panel1);
        }

        void ReadWordFromFile()
        {
            List<string> ourWord = new List<string>();
            using (StreamWriter sw = File.CreateText(@"ourWords.txt"))
            {
                sw.WriteLine("POČÍTAČ");
                sw.WriteLine("MATFYZ");
                sw.WriteLine("PROGRAMOVÁNÍ");
                sw.WriteLine("ČESKÁ REPUBLIKA");
                sw.WriteLine("PRAHA");
            }

            using (var sr = File.OpenText(@"ourWords.txt"))
            {
                string word = sr.ReadLine();
                while (word != null)
                {
                    ourWord.Add(word);
                    word = sr.ReadLine();
                }
            }
        }
    }
}
