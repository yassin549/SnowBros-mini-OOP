using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FreedomFodgeFrameWork;
 

namespace FreedomFodge
{
    public partial class Form1 : Form
    {

        private string filePath = "C:\\Users\\Administrator\\Desktop\\OOP GAME\\Level1Maze.txt";
        Game game;
        
        public Form1()
        {
            InitializeComponent();

            game = Game.GetInstance(this);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Point Boundary = new Point(this.Width, this.Height);
            game.SetMaze(new Maze(this, filePath, '@', ImagePaths.MazeTile1, '#', ImagePaths.MazeTile2));
            game.SetPlayer(new Player(this,game, 1400, 924,15, Boundary, ImagePaths.LeftPlayerImage1, ImagePaths.LeftPlayerImage2,ImagePaths.RightPlayerImage1,ImagePaths.RightPlayerImage2,ImagePaths.UpPlayerImage,ImagePaths.DownPlayerImage,ImagePaths.PlayerFire,1000,10,2));
            game.AddEnemies(new RandomEnemy(ImagePaths.LeftEnemy1Image,ImagePaths.RightEnemy1Image,game,20,this,450,200, 1));
            game.AddEnemies(new RandomEnemy(ImagePaths.LeftEnemy1Image, ImagePaths.RightEnemy1Image, game, 20, this,500, 450,1));
            game.AddEnemies(new RandomEnemy(ImagePaths.LeftEnemy3Image, ImagePaths.RightEnemy3Image, game, 20, this, 570, 800,1));
            game.AddEnemies(new RandomEnemy(ImagePaths.LeftEnemy3Image, ImagePaths.RightEnemy3Image, game, 20, this, 1400, 580,1));
            game.AddEnemies(new VerticalEnemy(ImagePaths.Enemy4Image, game, 7, ImagePaths.EnemyFire2, this, 1400, 800,10));
            game.AddEnemies(new VerticalEnemy(ImagePaths.Enemy4Image,  game, 11,ImagePaths.EnemyFire2, this, 1400, 280,10));
            game.AddEnemies(new HorizontalEnemy(ImagePaths.LeftEnemy2Image, ImagePaths.RightEnemy2Image, game, ImagePaths.EnemyFire3, 11, this, 700, 349,5));
            game.UpdateEnemyLabel();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
           
        }
 
         
         
    }
}
