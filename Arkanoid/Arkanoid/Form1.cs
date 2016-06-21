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
    public partial class ArkanoidForm : Form
    {
        Graphics paper;
        Paddle paddle = new Paddle();
        Ball ball = new Ball();
        const int BRICKS_COUNT = 30;

        private Dictionary<string, Rectangle> RecBricks = new Dictionary<string, Rectangle>();
        
        public ArkanoidForm()
        {
            InitializeComponent();

            for (int i = 1; i <= BRICKS_COUNT; i++ )
            {
                PictureBox brick = (this.Controls.Find("brick" + i, true).First() as PictureBox);
                RecBricks.Add("brick" + i, new Rectangle(brick.Location.X, brick.Location.Y, brick.Width, brick.Height));
            }

            serialPort1.PortName = "COM5";
            serialPort1.BaudRate = 9600;
            serialPort1.Open();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;
            paddle.drawPaddle(paper);
            ball.drawBall(paper);
        }

        //private void Form1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    paddle.movePaddle(e.X);
        //    this.Invalidate();
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.moveBall();
            ball.collision();
            ball.hitPaddle(paddle.PaddleRec);
            this.Invalidate();

            if (ball.BallRec.Y > 600) youLose.Visible = true;
            if (RecBricks.Count == 0) youWon.Visible = true;
            foreach (KeyValuePair<string, Rectangle> kvp in RecBricks)
            {
                if (kvp.Value.IntersectsWith(ball.BallRec))
                {
                    ball.xspeed *= -1;
                    ball.yspeed *= -1;
                    this.Controls.Remove(this.Controls.Find(kvp.Key, true).FirstOrDefault());
                    RecBricks.Remove(kvp.Key);
                    char[] x = new char[1];
                    x[0] = 'X';
                    serialPort1.Write(x, 0, 1);
                    break;               
                }

            }

        }

        private void ArkanoidForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            paddle.movePaddlebyKeyboard(e.KeyChar);
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            char data = Convert.ToChar(serialPort1.ReadChar());
            paddle.movePaddlebyKeyboard(data);

        }
    }
}
