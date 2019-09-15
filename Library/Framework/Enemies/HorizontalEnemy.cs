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

    public class HorizontalEnemy : Enemy
    {
        private EnemyDirection direction;
        private string FireImage;
        private List<PictureBox> pictureBoxes;
        private long lastBulletTime;
        public HorizontalEnemy(string imagePathLeft, string imagePathRight, Game game, string FireImage, int EnemySpeed, Form form, int xPosition, int yPosition,int EnemyHealth)
            : base(game, EnemySpeed, form, xPosition, yPosition,EnemyHealth)
        {
            this.imagePathLeft = imagePathLeft;
            this.imagePathRight = imagePathRight;
            this.pictureBox = new PictureBox();
            this.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox.Width = 30;
            this.FireImage = FireImage;
            this.pictureBox.Height = 30;
            pictureBox.BackColor = Color.Transparent;
            direction = EnemyDirection.Left;

            form.Controls.Add(this.pictureBox);
            DisplayEnemy();
            movementTimer = new Timer();
            movementTimer.Interval = 50;
            movementTimer.Tick += MovementTimer_Tick;
            movementTimer.Start();
            pictureBoxes = new List<PictureBox>();
            lastBulletTime = 0; 
        }

        private ICollision Collision = new EnemyCollision();

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

            long currentTime = DateTime.Now.Ticks;
            long elapsedTime = currentTime - lastBulletTime;

            if (elapsedTime >= TimeSpan.FromSeconds(2).Ticks)
            {
                GenerateBullet();
                lastBulletTime = currentTime;
            }
        }

        public void GenerateBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            bullet.Width = 45;
            bullet.Height = 45;
            bullet.BackColor = Color.Transparent;
            bullet.ImageLocation = FireImage;
            bullet.Location = new Point(this.pictureBox.Left + (this.pictureBox.Width / 2), this.pictureBox.Bottom);
            form.Controls.Add(bullet);
            pictureBoxes.Add(bullet);
            MoveFire(bullet);
        }

        public void MoveFire(PictureBox bullet)
        {
            Timer bulletTimer = new Timer();
            bulletTimer.Interval = 1;
            bulletTimer.Tick += (sender, e) =>
            {
                bullet.Top += 5;
                if (bullet.Bottom > form.ClientSize.Height || WallFireCollision(bullet))
                {
                    EraseBullet(bullet);
                    return;
                }
                if (bullet.Bounds.IntersectsWith(game.GetPlayer().GetPictureBox().Bounds))
                { 
                    EraseBullet(bullet);
                    int PlayerHealth = game.GetPlayer().GetPlayerHealth();
                    if (PlayerHealth >= 0)
                    {
                        PlayerHealth = PlayerHealth - 5;
                        game.GetPlayer().UpdatePlayerHealth(PlayerHealth);
                        game.GetPlayer().LivesDecrement();
                    }
                }
            };
            bulletTimer.Start();
        }
        public void EraseBullet(PictureBox bullet)
        {
            form.Controls.Remove(bullet);
            pictureBoxes.Remove(bullet);
        }
        public bool WallFireCollision(PictureBox firePictureBox)
        {
            List<PictureBox> boundaryPictures = game.GetMaze().GetBoundaryPictureBoxes();
            foreach (PictureBox boundaryPictureBox in boundaryPictures)
            {
                if (firePictureBox.Bounds.IntersectsWith(boundaryPictureBox.Bounds))
                {
                    return true;
                }
            }
            return false;
        }
        public override void Movement()
        {
            switch (direction)
            {
                case EnemyDirection.Right:
                    pictureBox.Left -= EnemySpeed;
                    pictureBox.ImageLocation = imagePathRight;
                    break;
                case EnemyDirection.Left:
                    pictureBox.Left += EnemySpeed;
                    pictureBox.ImageLocation = imagePathLeft;
                    break;
            }
            if (EnemyCollisions.BoundaryWallCollision(game))
            {
                if (direction == EnemyDirection.Left)
                {
                    direction = EnemyDirection.Right;
                }
                else if (direction == EnemyDirection.Right)
                {
                    direction = EnemyDirection.Left;
                }
            }
            if (EnemyCollisions.PlayerEnemyCollision(game))
            {
                if (direction == EnemyDirection.Left)
                {
                    direction = EnemyDirection.Right;
                }
                else if (direction == EnemyDirection.Right)
                {
                    direction = EnemyDirection.Left;
                }
            }
        }
    }
}
