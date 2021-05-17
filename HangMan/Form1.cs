using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.Black, 2);
          
            // Phan duoi
            g.DrawLine(p, new Point(30,canvas.Height - 30), new Point(200, canvas.Height - 30));
            // Coc
            g.DrawLine(p, new Point(80, 30), new Point(80, canvas.Height - 30));
            // Coc ngang
            g.DrawLine(p, new Point(80, 30), new Point(250, 30));
            // Coc cheo
            g.DrawLine(p, new Point(80, 80), new Point(130, 30));
            // Coc treo
            g.DrawLine(p, new Point(250, 30), new Point(250, 70));
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
    }
}
