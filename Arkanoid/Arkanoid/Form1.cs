using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkanoid
{
    public partial class Form1 : Form
    {
        Graphics paper;
        Paddle paddle = new Paddle();
        Ball ball = new Ball();
        int toWin = 30;
        
        private Rectangle recbrick01;
        private Rectangle recbrick02;
        private Rectangle recbrick03;
        private Rectangle recbrick04;
        private Rectangle recbrick05;
        private Rectangle recbrick06;
        private Rectangle recbrick07;
        private Rectangle recbrick08;
        private Rectangle recbrick09;
        private Rectangle recbrick10;
        private Rectangle recbrick11;
        private Rectangle recbrick12;
        private Rectangle recbrick13;
        private Rectangle recbrick14;
        private Rectangle recbrick15;
        private Rectangle recbrick16;
        private Rectangle recbrick17;
        private Rectangle recbrick18;
        private Rectangle recbrick19;
        private Rectangle recbrick20;
        private Rectangle recbrick21;
        private Rectangle recbrick22;
        private Rectangle recbrick23;
        private Rectangle recbrick24;
        private Rectangle recbrick25;
        private Rectangle recbrick26;
        private Rectangle recbrick27;
        private Rectangle recbrick28;
        private Rectangle recbrick29;
        private Rectangle recbrick30;

        private Rectangle[] recArray;
        private PictureBox[] brickArray;
        private PictureBox[] winn;
        private PictureBox[] losee;

        private Rectangle win;
        private Rectangle lose;
        
        public Form1()
        {
            InitializeComponent();

            recArray = new Rectangle[30];
            brickArray = new PictureBox[30];
            winn = new PictureBox[1];
            losee = new PictureBox[1];

            win = new Rectangle(youWon.Location.X, youWon.Location.Y, youWon.Width, youWon.Height);
            lose = new Rectangle(youLose.Location.X, youLose.Location.Y, youLose.Width, youLose.Height);
          
            recbrick01 = new Rectangle(brick01.Location.X, brick01.Location.Y, brick01.Width, brick01.Height);
            recbrick02 = new Rectangle(brick02.Location.X, brick02.Location.Y, brick02.Width, brick02.Height);
            recbrick03 = new Rectangle(brick03.Location.X, brick03.Location.Y, brick03.Width, brick03.Height);
            recbrick04 = new Rectangle(brick04.Location.X, brick04.Location.Y, brick04.Width, brick04.Height); 
            recbrick05 = new Rectangle(brick05.Location.X, brick05.Location.Y, brick05.Width, brick05.Height);
            recbrick06 = new Rectangle(brick06.Location.X, brick06.Location.Y, brick06.Width, brick06.Height);
            recbrick07 = new Rectangle(brick07.Location.X, brick07.Location.Y, brick07.Width, brick07.Height);
            recbrick08 = new Rectangle(brick08.Location.X, brick08.Location.Y, brick08.Width, brick08.Height); 
            recbrick09 = new Rectangle(brick09.Location.X, brick09.Location.Y, brick09.Width, brick09.Height);
            recbrick10 = new Rectangle(brick10.Location.X, brick10.Location.Y, brick10.Width, brick10.Height);
            recbrick11 = new Rectangle(brick11.Location.X, brick11.Location.Y, brick11.Width, brick11.Height);
            recbrick12 = new Rectangle(brick12.Location.X, brick12.Location.Y, brick12.Width, brick12.Height);
            recbrick13 = new Rectangle(brick13.Location.X, brick13.Location.Y, brick13.Width, brick13.Height); 
            recbrick14 = new Rectangle(brick14.Location.X, brick14.Location.Y, brick14.Width, brick14.Height);
            recbrick15 = new Rectangle(brick15.Location.X, brick15.Location.Y, brick15.Width, brick15.Height);
            recbrick16 = new Rectangle(brick16.Location.X, brick16.Location.Y, brick16.Width, brick16.Height);
            recbrick17 = new Rectangle(brick17.Location.X, brick17.Location.Y, brick17.Width, brick17.Height);
            recbrick18 = new Rectangle(brick18.Location.X, brick18.Location.Y, brick18.Width, brick18.Height); 
            recbrick19 = new Rectangle(brick19.Location.X, brick19.Location.Y, brick19.Width, brick19.Height);
            recbrick20 = new Rectangle(brick20.Location.X, brick20.Location.Y, brick20.Width, brick20.Height);
            recbrick21 = new Rectangle(brick21.Location.X, brick21.Location.Y, brick21.Width, brick21.Height);
            recbrick22 = new Rectangle(brick22.Location.X, brick22.Location.Y, brick22.Width, brick22.Height);
            recbrick23 = new Rectangle(brick23.Location.X, brick23.Location.Y, brick23.Width, brick23.Height); 
            recbrick24 = new Rectangle(brick24.Location.X, brick24.Location.Y, brick24.Width, brick24.Height);
            recbrick25 = new Rectangle(brick25.Location.X, brick25.Location.Y, brick25.Width, brick25.Height);
            recbrick26 = new Rectangle(brick26.Location.X, brick26.Location.Y, brick26.Width, brick26.Height);
            recbrick27 = new Rectangle(brick27.Location.X, brick27.Location.Y, brick27.Width, brick27.Height);
            recbrick28 = new Rectangle(brick28.Location.X, brick28.Location.Y, brick28.Width, brick28.Height);
            recbrick29 = new Rectangle(brick29.Location.X, brick29.Location.Y, brick29.Width, brick29.Height);
            recbrick30 = new Rectangle(brick30.Location.X, brick30.Location.Y, brick30.Width, brick30.Height);

            recArray[0] = recbrick01;
            recArray[1] = recbrick02;
            recArray[2] = recbrick03;
            recArray[3] = recbrick04;
            recArray[4] = recbrick05;
            recArray[5] = recbrick06;
            recArray[6] = recbrick07;
            recArray[7] = recbrick08;
            recArray[8] = recbrick09;
            recArray[9] = recbrick10;
            recArray[10] = recbrick11;
            recArray[11] = recbrick12;
            recArray[12] = recbrick13;
            recArray[13] = recbrick14;
            recArray[14] = recbrick15;
            recArray[15] = recbrick16;
            recArray[16] = recbrick17;
            recArray[17] = recbrick18;
            recArray[18] = recbrick19;
            recArray[19] = recbrick20;
            recArray[20] = recbrick21;
            recArray[21] = recbrick22;
            recArray[22] = recbrick23;
            recArray[23] = recbrick24;
            recArray[24] = recbrick25;
            recArray[25] = recbrick26;
            recArray[26] = recbrick27;
            recArray[27] = recbrick28;
            recArray[28] = recbrick29;
            recArray[29] = recbrick30;

            brickArray[0] = brick01;
            brickArray[1] = brick02;
            brickArray[2] = brick03;
            brickArray[3] = brick04;
            brickArray[4] = brick05;
            brickArray[5] = brick06;
            brickArray[6] = brick07;
            brickArray[7] = brick08;
            brickArray[8] = brick09;
            brickArray[9] = brick10;
            brickArray[10] = brick11;
            brickArray[11] = brick12;
            brickArray[12] = brick13;
            brickArray[13] = brick14;
            brickArray[14] = brick15;
            brickArray[15] = brick16;
            brickArray[16] = brick17;
            brickArray[17] = brick18;
            brickArray[18] = brick19;
            brickArray[19] = brick20;
            brickArray[20] = brick21;
            brickArray[21] = brick22;
            brickArray[22] = brick23;
            brickArray[23] = brick24;
            brickArray[24] = brick25;
            brickArray[25] = brick26;
            brickArray[26] = brick27;
            brickArray[27] = brick28;
            brickArray[28] = brick29;
            brickArray[29] = brick30;

            winn[0] = youWon;
            losee[0] = youLose;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;

            paddle.drawPaddle(paper);
            ball.drawBall(paper);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            paddle.movePaddle(e.X);
            this.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.moveBall();
            ball.collision();
            ball.hitPaddle(paddle.PaddleRec);
            this.Invalidate();

            for (int i = 0; i < recArray.Length; i++)
            {
                if (brickArray[i].Visible == true && recArray[i].IntersectsWith(ball.BallRec))
                {
                    ball.xspeed *= -1;
                    ball.yspeed *= -1;
                    brickArray[i].Visible = false;
                    toWin--;
                }

                if(toWin <= 0)
                {
                    winn[0].Visible = true;
                }

                if (ball.BallRec.Y > 600)
                {
                    losee[0].Visible = true; 
                }
            }

        }

    }
}
