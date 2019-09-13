using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FreedomFodgeFrameWork.Interfaces;

namespace FreedomFodgeFrameWork
{
    public class PlayerCollision :ICollision
    {
        public bool BoundaryWallCollision(Game game)
        {
                Player player = game.GetPlayer();            
                int playerX = player.GetPlayerX();
                int playerY = player.GetPlayerY();
                int playerWidth = player.GetPictureBox().Width;
                int playerHeight = player.GetPictureBox().Height;
                List<PictureBox> boundaryPictures = game.GetMaze().GetBoundaryPictureBoxes();
                foreach (var boundaryPicture in boundaryPictures)
                {
                    int boundaryX = boundaryPicture.Location.X;
                    int boundaryY = boundaryPicture.Location.Y;
                    int boundaryWidth = boundaryPicture.Width;
                    int boundaryHeight = boundaryPicture.Height;

                    if (playerY + playerHeight >= boundaryY &&
                        playerY <= boundaryY + boundaryHeight &&
                        playerX + playerWidth >= boundaryX &&
                        playerX <= boundaryX + boundaryWidth)
                    {
                        return true;
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
                    return true;
                }
            }
            return false;
        }
        public bool FireCollision(Game game, PictureBox picturebox)
        {
          List<Enemy> enemiesList = game.GetEnemies(); 
            foreach (Enemy enemy in enemiesList)
            {
                if (picturebox.Bounds.IntersectsWith(enemy.GetPictureBox().Bounds))
                {
                    int health = enemy.GetEnemyHealth();
                    int PlayerScore = game.GetPlayer().GetPlayerScore();
                    health--;
                    PlayerScore+=10;
                    enemy.SetEnemyHealth(health);
                    if (health <= 0)
                    {
                        enemiesList.Remove(enemy);
                        enemy.GetPictureBox().Visible = false;
                        game.GetForm().Controls.Remove(enemy.GetPictureBox());
                    }
                    game.GetPlayer().SetPlayerScore(PlayerScore);
                    game.GetPlayer().UpdatePlayerScoreLabel();
                    game.UpdateEnemyLabel();
                    return true;
                }    
            }
            game.SetEnemies(enemiesList);
            game.UpdateEnemyLabel();

            return false;
        }
    }
}

