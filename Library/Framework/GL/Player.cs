using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FreedomFodgeFrameWork.Interfaces;

namespace FreedomFodgeFrameWork
{
    public class Player :IMovement
    {
        private Form form;
        private int PlayerX;
        private int PlayerY;
        private string LeftImage1;
        private string LeftImage2;
        private string RightImage1;
        private string RightImage2;
        private string UpImage;
        private string DownImage;
        private string FireImage;
        private PictureBox pictureBox;
        private int PlayerSpeed;
        private bool isImage1Visible = true;
        private Game game;
        private List<Fire> PlayerFires = new List<Fire>();
        private int PlayerHealth;
        private int BulletSpeed;
        private int PlayerLives;
        private ProgressBar playerHealthBar;
        private int InitialHealth;
        private Label playerLivesLabel;
        private Label playerScoreLabel;
        private int PlayerScore;
        public Player(Form form, Game game, int PlayerX, int PlayerY, int Speed, System.Drawing.Point Boundary, string LeftImage1, string LeftImage2, string RightImage1, string RightImage2, string UpImage, string DownImage, string FireImage, int playerHealth, int BulletSpeed, int PlayerLives)
        {
            this.form = form;
            this.PlayerX = PlayerX;
            this.PlayerY = PlayerY;
            this.PlayerSpeed = Speed;
            this.FireImage = FireImage;
            this.LeftImage1 = LeftImage1;
            this.LeftImage2 = LeftImage2;
            this.RightImage1 = RightImage1;
            this.RightImage2 = RightImage2;
            this.UpImage = UpImage;
            this.DownImage = DownImage;
            this.game = game;
            this.PlayerHealth = playerHealth;
            this.InitialHealth = playerHealth;
            this.BulletSpeed = BulletSpeed;
            this.PlayerLives = PlayerLives;
            this.PlayerScore = 0;
            DisplayPlayer(Boundary);
            Movement();
            InitializePlayerHealthBar();
            InitializePlayerLivesLabel();

            InitializePlayerScoreLabel();

        }

        ICollision PlayerCollision = new PlayerCollision();

        public int GetPlayerX() { return PlayerX; }
        public int GetPlayerY() { return PlayerY; }
        public int GetPlayerHealth() { return PlayerHealth; }
        public void SetPlayerHealth(int health) { PlayerHealth = health; }
        public int GetPlayerScore() { return PlayerScore; }
        public void SetPlayerScore(int score) { PlayerScore = score; }

        public void SetPlayerLives(int Lives) { PlayerLives = Lives; }
        public PictureBox GetPictureBox() { return pictureBox; }
        public List<Fire> GetPlayerFires()
        {
            return this.PlayerFires;
        }
        private void DisplayPlayer(System.Drawing.Point Boundary)
        {
            Image image = Image.FromFile(LeftImage1);
            int pictureBoxWidth = image.Width;
            int pictureBoxHeight = image.Height;
            PlayerX = Math.Max(0, Math.Min(PlayerX, Boundary.X - pictureBoxWidth));
            PlayerY = Math.Max(0, Math.Min(PlayerY, Boundary.Y - pictureBoxHeight));
            pictureBox = new PictureBox();
            pictureBox.Width = pictureBoxWidth + 10;
            pictureBox.Height = pictureBoxHeight + 10;
            pictureBox.Location = new System.Drawing.Point(PlayerX, PlayerY);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BackColor = Color.Transparent;
            form.Controls.Add(pictureBox);
            ShowImage(LeftImage1);
        }
        public void Movement()
        {
            form.KeyDown += HandleKeyDown;
            form.KeyUp += HandleKeyUp;
        }
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    MoveLeft();
                    break;
                case Keys.Right:
                    MoveRight();
                    break;
                case Keys.Up:
                    MoveUp();
                    break;
                case Keys.Down:
                    MoveDown();
                    break;
                case Keys.Space:
                    GeneratePlayerFires();
                    RemoveInvisibleFires();
                    break;
            }
        }
        private void RemoveInvisibleFires()
        {
            PlayerFires.RemoveAll(fire => !fire.IsVisible);
        }
        private void GeneratePlayerFires()
        {
            int fireX = pictureBox.Left + (pictureBox.Width / 2);
            int fireY = pictureBox.Top + (pictureBox.Height / 2);
            if (isImage1Visible)
            {
                PlayerFires.Add(new Fire("left", form, pictureBox, FireImage, fireX, fireY, game, BulletSpeed));
            }
            else
            {
                PlayerFires.Add(new Fire("right", form, pictureBox, FireImage, fireX, fireY, game, BulletSpeed));
            }
        }
        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    ShowDefaultImage();
                    break;
                case Keys.Down:
                    ShowDefaultImage();
                    break;
            }
        }
        public void LivesDecrement()
        {
            if (PlayerHealth <= 0)
            {
                PlayerHealth = InitialHealth;

                PlayerLives--;
                playerLivesLabel.Text = "Lives: " + PlayerLives;
                if (PlayerLives == 0)
                {
                    form.Hide();
                    MessageBox.Show("Game Over");
                    Application.Exit();
                }
            }
        }
        private void MoveLeft()
        {
            ShowImage(LeftImage2);
            Timer timer = new Timer();
            timer.Interval = 40;
            timer.Tick += (sender, e) =>
            {
                PlayerX -= PlayerSpeed;
                playerHealthBar.Location = new Point(PlayerX, PlayerY - playerHealthBar.Height);

                if (PlayerCollision.BoundaryWallCollision(game))
                {
                    PlayerX += PlayerSpeed;
                }
                if (PlayerCollision.PlayerEnemyCollision(game))
                {
                    PlayerX += PlayerSpeed;
                     
                }
                UpdatePlayerPosition();
                ShowImage(LeftImage1);
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
        private void InitializePlayerHealthBar()
        {
            playerHealthBar = new ProgressBar();
            playerHealthBar.Minimum = 0;
            playerHealthBar.Maximum = Math.Max(1, PlayerHealth);
            playerHealthBar.Value = PlayerHealth;
            playerHealthBar.Width = pictureBox.Width;
            playerHealthBar.Height = 10;
            playerHealthBar.BackColor = Color.Red;
            playerHealthBar.ForeColor = Color.Green;
            playerHealthBar.Style = ProgressBarStyle.Continuous;
            playerHealthBar.Visible = true;
            playerHealthBar.Location = new Point(PlayerX, PlayerY - playerHealthBar.Height);
            form.Controls.Add(playerHealthBar);
        }
        public void UpdatePlayerHealth(int newHealth)
        {
            PlayerHealth = newHealth;
            playerHealthBar.Value = PlayerHealth;

        }
        private void MoveRight()
        {
            ShowImage(RightImage2);

            Timer timer = new Timer();
            timer.Interval = 40;
            timer.Tick += (sender, e) =>
            {
                PlayerX += PlayerSpeed;
                playerHealthBar.Location = new Point(PlayerX, PlayerY - playerHealthBar.Height);

                if (PlayerCollision.BoundaryWallCollision(game))
                {
                    PlayerX -= PlayerSpeed;
                }
                if (PlayerCollision.PlayerEnemyCollision(game))
                {
                    PlayerX -= PlayerSpeed;
                     
                }
                UpdatePlayerPosition();
                ShowImage(RightImage1);
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
        private void MoveUp()
        {
            ShowImage(UpImage);
            Timer timer = new Timer();
            timer.Interval = 40;
            timer.Tick += (sender, e) =>
            {
                PlayerY -= PlayerSpeed;
                playerHealthBar.Location = new Point(PlayerX, PlayerY - playerHealthBar.Height);

                if (PlayerCollision.BoundaryWallCollision(game))
                {
                    PlayerY += PlayerSpeed;
                }
                if (PlayerCollision.PlayerEnemyCollision(game))
                {
                    PlayerY += PlayerSpeed;
                     
                }
                UpdatePlayerPosition();
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();

        }
        private void MoveDown()
        {

            ShowImage(DownImage);
            Timer timer = new Timer();
            timer.Interval = 40;
            timer.Tick += (sender, e) =>
            {
                PlayerY += PlayerSpeed;
                playerHealthBar.Location = new Point(PlayerX, PlayerY - playerHealthBar.Height);

                if (PlayerCollision.BoundaryWallCollision(game))
                {
                    PlayerY -= PlayerSpeed;
                }
                if (PlayerCollision.PlayerEnemyCollision(game))
                {
                    PlayerY -= PlayerSpeed;
                     
                }
                UpdatePlayerPosition();
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
        private void ShowDefaultImage()
        {
            if (isImage1Visible)
                ShowImage(LeftImage1);
            else
                ShowImage(RightImage1);
        }
        private void InitializePlayerLivesLabel()
        {
            playerLivesLabel = new Label();
            playerLivesLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            playerLivesLabel.ForeColor = Color.White;
            playerLivesLabel.BackColor = Color.Transparent;
            playerLivesLabel.AutoSize = true;
            playerLivesLabel.Location = new Point(20, 20);
            UpdatePlayerLivesLabel();
            form.Controls.Add(playerLivesLabel);
        }
        private void UpdatePlayerLivesLabel()
        {
            playerLivesLabel.Text = "Lives: " + PlayerLives;
        }
        private void InitializePlayerScoreLabel()
        {
            playerScoreLabel = new Label();
            playerScoreLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            playerScoreLabel.ForeColor = Color.White;
            playerScoreLabel.BackColor = Color.Transparent;
            playerScoreLabel.AutoSize = true;
            playerScoreLabel.Location = new Point(20, 40); 
            UpdatePlayerScoreLabel();
            form.Controls.Add(playerScoreLabel);
        }
        public void UpdatePlayerScoreLabel()
        {
            playerScoreLabel.Text = "Score: " + PlayerScore;
        }
        private void UpdatePlayerPosition()
        {
            int pictureBoxWidth = pictureBox.Width;
            int pictureBoxHeight = pictureBox.Height;
            PlayerX = Math.Max(0, Math.Min(PlayerX, form.ClientSize.Width - pictureBoxWidth));
            PlayerY = Math.Max(0, Math.Min(PlayerY, form.ClientSize.Height - pictureBoxHeight));
            pictureBox.Location = new System.Drawing.Point(PlayerX, PlayerY);
        }
        private void ShowImage(string imagePath)
        {
            pictureBox.Image = Image.FromFile(imagePath);
            isImage1Visible = (imagePath == LeftImage1 || imagePath == LeftImage2);
        }
    }
}