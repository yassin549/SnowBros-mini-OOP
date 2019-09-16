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
    public class Fire
    {
        private string direction;
        private Form form;
        private PictureBox pictureBox;
        private string FireImage;
        private int FireX;
        private int FireY;
        private Game game;
        public Boolean IsVisible;
        public int BulletSpeed;
        public Fire(string direction, Form form, PictureBox pictureBox, string fireImage, int fireX, int fireY, Game game,int BulletSpeed)
        {
            this.direction = direction;
            this.form = form;
            this.pictureBox = pictureBox;
            FireImage = fireImage;
            FireX = fireX;
            FireY = fireY;
            this.game = game;
            this.BulletSpeed = BulletSpeed;
            GenerateBullet();            
        }
        public PictureBox GetPictureBox(PictureBox pictureBox)
        {
            return pictureBox;
        } 
        public void GenerateBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.SizeMode = PictureBoxSizeMode.Zoom;
            bullet.Width = 20;
            bullet.Height = 20;
            bullet.BackColor = Color.Transparent;
            bullet.Image = Image.FromFile(FireImage);
            if (direction == "left")
            {
                bullet.Location = new Point(FireX, FireY);

            }
            else if (direction == "right")
            {
                bullet.Location = new Point(FireX , FireY );
            }
            else if (direction == "down")
            {
                bullet.Location = new Point(FireX  , FireY);
            }
            form.Controls.Add(bullet);
 
            MoveBullet(bullet);
        }
        ICollision FireCollision = new PlayerCollision();
        public void MoveBullet(PictureBox bullet)
        {
            Timer bulletTimer = new Timer();
            bulletTimer.Interval = 2;
            bulletTimer.Tick += (sender, e) =>
            {
                if (direction == "left")
                {
                    bullet.Left -= BulletSpeed;

                }
                else if (direction == "right")
                {
                    bullet.Left += BulletSpeed;
                }
                else if (direction == "down")
                {
                    bullet.Top += BulletSpeed;
                }
                if (WallFireCollision(bullet))
                {
                    EraseBullet(bullet);
                    return;
                }
                if (FireCollision.FireCollision(game, bullet))
                {
                    EraseBullet(bullet);
                    return;
                }
                if (bullet.Left < 0 || bullet.Left > form.ClientSize.Width)
                {
                    EraseBullet(bullet);
                    return;
                }
            };
            bulletTimer.Start();
        } 
        public void EraseBullet(PictureBox bullet)
        {
            bullet.Visible = false;
            form.Controls.Remove(bullet);
            IsVisible = false;
            
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
    }
}
