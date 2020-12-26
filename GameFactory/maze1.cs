﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameFactory
{
    public partial class maze1 : Form
    {
        public int playerA_score = 0, playerB_score = 0;
        public bool TurnAB = true;

        //A미로 생성
        public char[,] mazeA = new char[11, 11] {                                    
            {'2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2'},
            {'2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2'},
            {'2', '0', '2', '0', '2', '0', '2', '0', '2', '0', '2'},
            {'2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2'},
            {'2', '0', '2', '0', '2', '0', '2', '0', '2', '0', '2'},
            {'2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2'},
            {'2', '0', '2', '0', '2', '0', '2', '0', '2', '0', '2'},
            {'2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2'},
            {'2', '0', '2', '0', '2', '0', '2', '0', '2', '0', '2'},
            {'2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2'},
            {'2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2'} };

        //B미로 생성
        public char[,] mazeB = new char[11, 11] {                                    
            {'2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2'},
            {'2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2'},
            {'2', '0', '2', '0', '2', '0', '2', '0', '2', '0', '2'},
            {'2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2'},
            {'2', '0', '2', '0', '2', '0', '2', '0', '2', '0', '2'},
            {'2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2'},
            {'2', '0', '2', '0', '2', '0', '2', '0', '2', '0', '2'},
            {'2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2'},
            {'2', '0', '2', '0', '2', '0', '2', '0', '2', '0', '2'},
            {'2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2'},
            {'2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2'} };

        public maze1()
        {
            InitializeComponent();          
        }

        //게임시작
        private void button1_Click(object sender, EventArgs e)        
        {
            maze2 form2 = new maze2(this);
            form2.Show();
        }
    }
}