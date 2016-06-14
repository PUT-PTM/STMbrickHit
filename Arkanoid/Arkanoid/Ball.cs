using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid
{
    public class Ball
    {
        private int x, y, width, height;
        public int xspeed, yspeed;
        private Random randSpeed;

        private Image ballImage;
        private Rectangle ballRec;

        public Rectangle BallRec
        {
            get { return ballRec; }
        }

        public int Xspeed
        {
            get { return xspeed; }
        }

        public int Yspeed
        {
            get { return yspeed; }
        }



        public Ball()
        {
            randSpeed = new Random();
            x = 400;
            y = 400;
            width = 20;
            height = 20;

            xspeed = randSpeed.Next(5, 7);
            yspeed = randSpeed.Next(5, 7);

            ballImage = Arkanoid.Properties.Resources.ball2;

            ballRec = new Rectangle(x, y, width, height);
        }

        public void drawBall(Graphics paper)
        {
            paper.DrawImage(ballImage, ballRec);
        }

        public void movePaddle(int mouseX)
        {
            ballRec.X = mouseX - (ballRec.Width);
        }

        public void moveBall()
        {
            ballRec.X += xspeed;
            ballRec.Y += yspeed;
        }

        public void collision()
        {
            if (ballRec.X < 0 || ballRec.X > 760)
            {
                xspeed *= -1;
            }
            if (ballRec.Y < 0)
            {
                yspeed *= -1;
            }
         
        }

        public void hitPaddle(Rectangle paddleRec)
        {
            if(paddleRec.IntersectsWith(ballRec))
            {
                yspeed *= -1;
            }
        }
    }
}
