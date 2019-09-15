using FreedomFodgeFrameWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreedomFodgeFrameWork
{

    public class RandomEnemy : Enemy
    {
        EnemyDirection direction;
        public RandomEnemy(string imagePathLeft, string imagePathRight, Game game, int EnemySpeed, Form form, int xPosition, int yPosition,int EnemyHealth)
            : base(game, EnemySpeed, form, xPosition, yPosition,EnemyHealth)
        {
            this.imagePathLeft = imagePathLeft;
            this.imagePathRight = imagePathRight;
            this.pictureBox = new PictureBox();
            this.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.BackColor = Color.Transparent;
            this.pictureBox.Width = 30;
            this.pictureBox.Height = 30;
            form.Controls.Add(this.pictureBox);
            DisplayEnemy();
            movementTimer = new Timer();
            movementTimer.Interval = 70;
            movementTimer.Tick += MovementTimer_Tick;
            movementTimer.Start();
        }
        public void DisplayEnemy()
        {
            if (pictureBox != null)
            {
                pictureBox.ImageLocation = imagePathLeft;
                pictureBox.Location = new System.Drawing.Point(xPosition, yPosition);
            }
        }
        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            Movement(); 

        }
        public override void Movement()
        {
            direction = (EnemyDirection)random.Next(0, 4);

            switch (direction)
            {
                case EnemyDirection.Up:
                    pictureBox.Top -= EnemySpeed;
                    break;
                case EnemyDirection.Down:
                    pictureBox.Top += EnemySpeed;
                    break;
                case EnemyDirection.Left:
                    pictureBox.Left -= EnemySpeed;
                    pictureBox.ImageLocation = imagePathLeft;
                    break;
                case EnemyDirection.Right:
                    pictureBox.Left += EnemySpeed;
                    pictureBox.ImageLocation = imagePathRight;
                    break;
            }
            if (EnemyCollisions.BoundaryWallCollision(game)|| EnemyCollisions.PlayerEnemyCollision(game))
            {
                switch (direction)
                {
                    case EnemyDirection.Up:
                        pictureBox.Top += EnemySpeed;
                        break;
                    case EnemyDirection.Down:
                        pictureBox.Top -= EnemySpeed;
                        break;
                    case EnemyDirection.Left:
                        pictureBox.Left += EnemySpeed;
                        break;
                    case EnemyDirection.Right:
                        pictureBox.Left -= EnemySpeed;
                        break;
                }
            }
        }

    }
}

