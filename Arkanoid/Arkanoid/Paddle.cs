using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid
{
    public class Paddle
    {
        private int x, y, width, height;

        private Image paddleImage;
        private Rectangle paddleRec;

        public Rectangle PaddleRec
        {
            get { return paddleRec; }
        }

        public Paddle()
        {
            x = 350;
            y = 530;
            width = 100;
            height = 15;

            paddleImage = Arkanoid.Properties.Resources.paddle2;

            paddleRec = new Rectangle(x, y, width, height);
        }

        public void drawPaddle(Graphics paper)
        {
            paper.DrawImage(paddleImage, paddleRec);
        }

        public void movePaddle(int mouseX)
        {
            paddleRec.X = mouseX - (paddleRec.Width / 2);
        }

        public void movePaddlebyKeyboard(char Key, ArkanoidForm AF)
        {

            if ((Key == 'a') || (Key == 'A'))
            {
                paddleRec.X -= (paddleRec.Width / 2);
                if (paddleRec.X < 0)
                {
                    paddleRec.X = 0;
                }
            }

            if ((Key == 'd') || (Key == 'D'))
            {
                paddleRec.X += (paddleRec.Width / 2);
                if (paddleRec.X > AF.Width)
                {
                    paddleRec.X = AF.Width;
                }
            }

        }
    }
}
