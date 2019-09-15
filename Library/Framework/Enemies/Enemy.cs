using FreedomFodgeFrameWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FreedomFodgeFrameWork
{
    public class Enemy :IMovement
    {
        protected string imagePathLeft;
        protected string imagePathRight; 
        protected Form form;
        protected int xPosition;
        protected int yPosition;
        protected int EnemyHealth;
        protected PictureBox pictureBox;
        protected Random random;
        protected Timer movementTimer;
        protected int EnemySpeed;
        protected Game game;
        protected ICollision EnemyCollisions;
        protected List<Fire> EnemyFires = new List<Fire>();
        private Label RemainingEnemiesLabel;
        public Enemy(Game game, int EnemySpeed, Form form, int xPosition, int yPosition,int EnemyHealth)
        {
            this.form = form;
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.EnemySpeed = EnemySpeed;
            this.EnemyHealth = EnemyHealth;
            this.game = game;
            random = new Random();
            EnemyCollisions = new EnemyCollision();
        } 
        public int GetEnemyHealth()
        {
            return EnemyHealth;
        }
        public void SetEnemyHealth(int EnemyHealth)
        {
            this.EnemyHealth=EnemyHealth;
        }
        public PictureBox GetPictureBox()
        {
            return pictureBox;
        }
        public virtual void Movement()
        {   
        }  
    }
}
