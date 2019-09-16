using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FreedomFodgeFrameWork
{
    public class Gravity
    {
        public static void ApplyGravity(PictureBox pictureBox, ref int X, ref int Y, Game game)
        {
            List<PictureBox> boundaryPictures = game.GetMaze().GetBoundaryPictureBoxes();
            List<PictureBox> innerMazePictures = game.GetMaze().GetInnerPictureBoxes();
            int fallingSpeed = 2;
            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += (sender, e) => {
                 
                foreach (PictureBox boundaryPicture in boundaryPictures)
                {
                    if (pictureBox.Bounds.IntersectsWith(boundaryPicture.Bounds))
                    {
                        timer.Stop();
                        return;
                    }
                } 
                foreach (PictureBox innerMazePicture in innerMazePictures)
                {
                    if (pictureBox.Bounds.IntersectsWith(innerMazePicture.Bounds))
                    {
                        timer.Stop();
                        return;
                    }
                }
                pictureBox.Top += fallingSpeed;
 
            };
            timer.Start();
            X = pictureBox.Left;
            Y = pictureBox.Top;
        }
    }
}
