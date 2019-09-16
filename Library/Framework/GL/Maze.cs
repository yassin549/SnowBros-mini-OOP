using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FreedomFodgeFrameWork
{
    public class Maze
    {
        private char[,] maze;
        private int numRows;
        private int numColumns;
        private Form form;      
        private List<PictureBox> BoundaryPictureBoxes = new List<PictureBox>();
        private List<PictureBox> InnerPictureBoxes = new List<PictureBox>();   
        public List<PictureBox> GetBoundaryPictureBoxes()
        {
            return BoundaryPictureBoxes;
        }
        public List<PictureBox> GetInnerPictureBoxes()
        { return InnerPictureBoxes; }
        public Maze(Form form, string filePath, char targetedChar, string imagePath, char targetedChar2, string imagePath2)
        {
            this.form = form;
            DisplayMaze(filePath,targetedChar,imagePath,targetedChar2,imagePath2);
        }
        private void LoadMaze(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            numRows = lines.Length;
            numColumns = lines[0].Length;
            maze = new char[numRows, numColumns];
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    maze[i, j] = lines[i][j];
                }
            }
        }
        public void DisplayMaze(string filePath, char targetedChar, string imagePath, char targetedChar2, string imagePath2)
        {
            LoadMaze(filePath);
            int pictureBoxWidth = 20; 
            int pictureBoxHeight = 20;
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    if (maze[i, j] == targetedChar)
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Load(imagePath);
                        pictureBox.Width = pictureBox.Image.Width;
                        pictureBox.Height = pictureBox.Image.Height;
                        pictureBox.Location = new System.Drawing.Point(j * pictureBoxWidth, i * pictureBoxHeight);
                        form.Controls.Add(pictureBox);
                        BoundaryPictureBoxes.Add(pictureBox);
                    }
                    if (maze[i, j] == targetedChar2)
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Load(imagePath2);
                        pictureBox.Width = pictureBox.Image.Width;
                        pictureBox.Height = pictureBox.Image.Height;
                        pictureBox.Location = new System.Drawing.Point(j * pictureBoxWidth, i * pictureBoxHeight);
                        form.Controls.Add(pictureBox);
                        BoundaryPictureBoxes.Add(pictureBox);
                    }
                }
            }

        }


    }
}
