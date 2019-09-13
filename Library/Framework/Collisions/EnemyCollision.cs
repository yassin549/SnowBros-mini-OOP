using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FreedomFodgeFrameWork.Interfaces;

namespace FreedomFodgeFrameWork
{
    public class EnemyCollision :ICollision
    {
        public bool BoundaryWallCollision(Game game)
        {
            List<Enemy> EnemiesList = game.GetEnemies();
            List<PictureBox> boundaryPictures = game.GetMaze().GetBoundaryPictureBoxes();
            foreach (var enemy in EnemiesList)
            {
                PictureBox enemyPictureBox = enemy.GetPictureBox();
                int enemyX = enemyPictureBox.Location.X;
                int enemyY = enemyPictureBox.Location.Y;
                int enemyWidth = enemyPictureBox.Width;
                int enemyHeight = enemyPictureBox.Height;

                foreach (var boundaryPicture in boundaryPictures)
                {
                    int boundaryX = boundaryPicture.Location.X;
                    int boundaryY = boundaryPicture.Location.Y;
                    int boundaryWidth = boundaryPicture.Width;
                    int boundaryHeight = boundaryPicture.Height;

                    if (enemyY + enemyHeight-2 >= boundaryY &&
                        enemyY <= boundaryY + boundaryHeight &&
                        enemyX + enemyWidth >= boundaryX &&
                        enemyX <= boundaryX + boundaryWidth)
                    {
                        return true;
                    }
                }
            }
            return false;

        }
        public bool PlayerEnemyCollision(Game game)
        {
            List<Enemy> EnemiesList = game.GetEnemies();
            Player player = game.GetPlayer();

            int playerX = player.GetPlayerX();
            int playerY = player.GetPlayerY();
            int playerWidth = player.GetPictureBox().Width;
            int playerHeight = player.GetPictureBox().Height;
            foreach (var enemy in EnemiesList)
            {
                PictureBox enemyPictureBox = enemy.GetPictureBox();
                int enemyX = enemyPictureBox.Location.X;
                int enemyY = enemyPictureBox.Location.Y;
                int enemyWidth = enemyPictureBox.Width;
                int enemyHeight = enemyPictureBox.Height;

                if (playerY + playerHeight >= enemyY &&
                    playerY <= enemyY + enemyHeight &&
                    playerX + playerWidth >= enemyX &&
                    playerX <= enemyX + enemyWidth)
                {
                    int PlayerHealth = game.GetPlayer().GetPlayerHealth();
                    if (PlayerHealth >= 0)
                    {
                        PlayerHealth = PlayerHealth - 10;
                        game.GetPlayer().UpdatePlayerHealth(PlayerHealth);
                        game.GetPlayer().LivesDecrement();
                    }
                    return true;
                  
                }
            }

            return false;
        }
        public bool FireCollision(Game game, PictureBox picturebox)
        {
            Player player = game.GetPlayer();

            if (picturebox.Bounds.IntersectsWith(player.GetPictureBox().Bounds))
            {
                return true;
            }

            return false;
        }

    }
}
