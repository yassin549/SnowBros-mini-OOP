using FreedomFodgeFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FreedomFodgeFrameWork.Interfaces;
using System.Drawing;

namespace FreedomFodgeFrameWork
{
    public class Game
    {
        private static Game instance;
        private Maze Maze;
        private Player Player;
        private Form Form;
        private Label RemainingEnemiesLabel;
        private List<Enemy> Enemies = new List<Enemy>();
        private Game(Form form)
        {
            this.Form = form;
            InitializeEnemiesLabel();
        }

        public static Game GetInstance(Form form)
        {
            if (instance == null)
            {
                instance = new Game(form);
            }
            return instance;
        }
        public Maze GetMaze()
        {
            return this.Maze;
        }
        public Player GetPlayer()
        {
            return this.Player;
        }
        public Form GetForm() { return this.Form; }
        public void SetMaze(Maze maze)
        {
            this.Maze = maze;
        }
        public void SetPlayer(Player player)
        {
            this.Player = player;
        }
        public void AddEnemies(Enemy enemy)
        { Enemies.Add(enemy); }
        public List<Enemy> GetEnemies()
        {
            return Enemies;
        }
        public void SetEnemies(List<Enemy> Enemies)
        {
            this.Enemies = Enemies;
        }
        private void InitializeEnemiesLabel()
        {
            RemainingEnemiesLabel = new Label();
            RemainingEnemiesLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            RemainingEnemiesLabel.ForeColor = Color.White;
            RemainingEnemiesLabel.BackColor = Color.Transparent;
            RemainingEnemiesLabel.AutoSize = true;
            RemainingEnemiesLabel.Location = new Point(10,60);
            UpdateEnemyLabel();
            Form.Controls.Add(RemainingEnemiesLabel);
        }
        public void Update()
        {
            Player.Movement();
            foreach (var enemy in Enemies)
            {
                enemy.Movement();
            }
        }
        public void UpdateEnemyLabel()
        { 
            RemainingEnemiesLabel.Text = "Remaining Enemies : " + Enemies.Count;
        }

    }
}
