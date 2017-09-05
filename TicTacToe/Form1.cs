using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        Button[,] buttons;
        Node n = new Node();
        static int computer = 1;
        public Form1()
        {
            InitializeComponent();
            buttons = new Button[3, 3];
            for(int i =0;i<3;i++)
                for(int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Click += new EventHandler(click);
                    buttons[i, j].Text = "";
                    buttons[i, j].Dock = DockStyle.Fill;
                    TableLayoutPanel1.Controls.Add(buttons[i, j], i, j);
                }
        }
    }
}
