using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HangMan
{
    class DrawPicture
    {

        Pen p = new Pen(Color.Black, 2);

        public void GenerateGallows(Graphics g, int canvasHeight) // Gallows: sibenice/gia treo co
        {
            Pen p = new Pen(Color.Black, 2);
            // Phan duoi
            g.DrawLine(p, new Point(30, canvasHeight - 30), new Point(200, canvasHeight - 30));
            // Coc
            g.DrawLine(p, new Point(80, 30), new Point(80, canvasHeight - 30));
            // Coc ngang
            g.DrawLine(p, new Point(80, 30), new Point(250, 30));
            // Coc cheo
            g.DrawLine(p, new Point(80, 80), new Point(130, 30));
            // Coc treo
            g.DrawLine(p, new Point(250, 30), new Point(250, 70));
        }

        public void GenerateBodyPart(Graphics g, int numberOfBodyPart)
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
    }
}
