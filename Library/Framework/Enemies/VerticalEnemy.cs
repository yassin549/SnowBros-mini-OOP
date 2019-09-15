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
    public class VerticalEnemy : Enemy
    {
        private EnemyDirection direction;
        private string FireImage;
        private List<PictureBox> pictureBoxes;
        private long lastBulletTime;
        public VerticalEnemy(string imagePath, Game game, int EnemySpeed,string FireImage, Form form, int xPosition, int yPosition, int EnemyHealth)
            : base(game, EnemySpeed, form, xPosition, yPosition,EnemyHealth)
        {
            this.imagePathLeft = imagePath;
            this.pictureBox = new PictureBox();
            this.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox.Width = 50;
            this.pictureBox.Height = 50;
            pictureBox.BackColor = Color.Transparent;
            direction = EnemyDirection.Up;
            this.FireImage= FireImage;
            form.Controls.Add(this.pictureBox);
            DisplayEnemy();
            movementTimer = new Timer();
            movementTimer.Interval = 50;
            movementTimer.Tick += MovementTimer_Tick;
            movementTimer.Start();
            pictureBoxes = new List<PictureBox>();
            lastBulletTime = 0;
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
            bullet.Width = 10;
            bullet.Height = 10;
            bullet.BackColor = Color.Transparent;
            bullet.ImageLocation = FireImage;
            int bulletX = this.pictureBox.Left + (this.pictureBox.Width / 2);
            int bulletY = this.pictureBox.Top;
            bullet.Location = new Point(bulletX, bulletY);
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
                bullet.Left -= 5;
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
                case EnemyDirection.Up:
                    pictureBox.Top -= EnemySpeed;
                    break;
                case EnemyDirection.Down:
                    pictureBox.Top += EnemySpeed;
                    break;
            }
            if (EnemyCollisions.BoundaryWallCollision(game))
            {
                if (direction == EnemyDirection.Up)
                {
                    direction = EnemyDirection.Down;
                }
                else if (direction == EnemyDirection.Down)
                {
                    direction = EnemyDirection.Up;
                }
            }
            if (EnemyCollisions.PlayerEnemyCollision(game))
            {
                if (direction == EnemyDirection.Up)
                {
                    direction = EnemyDirection.Down;
                }
                else if (direction == EnemyDirection.Down)
                {
                    direction = EnemyDirection.Up;
                }
            }   
            switch (direction)
            {
                case EnemyDirection.Up:
                    pictureBox.Top -= EnemySpeed;
                    break;
                case EnemyDirection.Down:
                    pictureBox.Top += EnemySpeed;
                    break;
            }
        }

    }

}
