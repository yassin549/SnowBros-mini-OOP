﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreedomFodge
    {
    public partial class Form0 : Form
        {
        public Form0()
            {
            InitializeComponent();
            }

        private void button2_Click(object sender, EventArgs e)
            {
            Environment.Exit(0);
            }

        private void button1_Click(object sender, EventArgs e)
            {
            Form f = new Form1();
            f.Show();
            this.Hide();
            }
        }
    }
