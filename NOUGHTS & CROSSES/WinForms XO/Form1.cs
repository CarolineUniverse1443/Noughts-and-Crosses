using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_XO
{
    public partial class Form1 : Form
    {
        private int bw = 80, bh = 80;
        private int dx = 10, dy = 10;
        private List<Button> btn = new List<Button>();
        public int m = 3;//size of matrix
        public int x, y, i, j, k, pos;
        public int move; //move = 0 - crosses go, move = 1 - noughts go, move = 2 - game over
        public Random r = new Random();
        public int crWin = 0, nWin = 0, draw = 0;
        public int ly;//y location of label1
        public bool mode;
        public Button back = new Button();
        public int fs = 0;//first move hasn`t yet been made
        public int count = 0;//number of moves in computer mode
        public bool n1 = false;//noughts don`t go first

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = SystemColors.Control;
            if (btn.Text == "X")
            {
                btn.ForeColor = Color.Red;
            }
            else if (btn.Text == "O")
            {
                btn.ForeColor = Color.Green;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        public void BtnVis()
        {
            label1.Visible = false;
            label2.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button1.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            back.Visible = true;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            for (i = 0; i < m * m; i++)
            {
                btn[i].Visible = true;
            }           
        }

        public void BackToMenu()
        {
            label1.Visible = true;
            label2.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button1.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            back.Visible = false;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            for (i = 0; i < m * m; i++)
            {
                btn[i].Visible = false;
            }

            crWin = 0;
            nWin = 0;
            draw = 0;
            NewGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BtnVis();
            mode = true;//two players mode
            Form1.ActiveForm.Text = "Two players";
        }    

        private void button3_Click(object sender, EventArgs e)
        {
            BtnVis();
            mode = false;//computer mode
            Form1.ActiveForm.Text = "Computer";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult mess = new DialogResult();
            if (fs == 1)//first move has been made
            {
                mess = MessageBox.Show("Current game will disappear. Continue?", "Warning", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mess == DialogResult.Yes)
                {
                    BackToMenu();
                }
            }
            else
            {
                BackToMenu();
            }            
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.ForeColor = SystemColors.ControlDark;
            button4.BackColor = SystemColors.Control;
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            button4.ForeColor = Color.Black;
            button4.BackColor = SystemColors.Control;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("   ~ Current game ~ \n Crosses wins:         "
                + crWin +"\n Noughts wins:       " + nWin + "\n Draws:                     " + draw, 
                "*** Scores ***", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.ForeColor = SystemColors.ControlDark;
            button5.BackColor = SystemColors.Control;
        }

        private void button5_MouseMove(object sender, MouseEventArgs e)
        {
            button5.ForeColor = Color.Black;
            button5.BackColor = SystemColors.Control;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                move = 0;
                n1 = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                move = 1;
                n1 = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createBtn();
            for (i = 0; i < m * m; i++)
            {
                btn[i].Visible = false;
            }
            back.Visible = false;
        }

        public void NewGame()
        {
            for (i = 0; i < m * m; i++)
            {
                btn[i].Text = string.Empty;
            }
            k = 0;

            if(n1)
            {
                move = 1;
            }
            else
            {
                move = 0;
            }
            pos = 0;
            fs = 0;
            count = 0;
        }

        //create game field with empty cells
        public void createBtn()
        {
            y = dy;
            this.Controls.Add(back);//use button as a background
            back.Size = new Size(3*bw+2*dx,3*bh+2*dy);
            back.BackColor = Color.Black;
            back.FlatAppearance.BorderSize = 0;
            back.FlatStyle = FlatStyle.Flat;
            back.Location = new Point(dx, dy);
            back.Enabled = false;

            k = 0;
            for (i = 0; i < m; i++)
            {
                x = dx;
                for (j = 0; j < m; j++)
                {

                    Button butt = new Button();
                    btn.Add(butt);
                    btn[k].SetBounds(x, y, bw, bh);
                    btn[k].Font = new Font("Gill Sans", 40, FontStyle.Bold);
                    btn[k].FlatAppearance.BorderSize = 0;
                    btn[k].FlatStyle = FlatStyle.Flat;
                    this.btn[k].Click += new System.EventHandler(this.Button_Click);
                    this.btn[k].MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_MouseMove);
                    this.btn[k].MouseLeave += new System.EventHandler(this.button1_MouseLeave);
                    this.Controls.Add(this.btn[k]);

                    x = x + bw + dx;
                    k++;
                }
                y = y + bh + dy;
            }
            back.SendToBack();
        }

        //if middle element of the line or the column is the same as the previous and
        //the next one, the player won
        public void checkField()
        {
            if (move != 2)
            {
                k = 0;
                for (i = 0; i < m; i++)
                {
                    for (j = 0; j < m; j++)
                    {
                        if (j == 1)//horizontals
                        {
                            if ((btn[k].Text != "") && (btn[k - 1].Text == btn[k].Text) && (btn[k].Text == btn[k + 1].Text))
                            {
                                if (btn[k].Text == "X")
                                {
                                    MessageBox.Show("Crosses win!");
                                    crWin++;
                                }
                                else if(btn[k].Text == "O")
                                {
                                    MessageBox.Show("Noughts win!");
                                    nWin++;
                                }
                                move = 2;
                            }
                        }
                        if (i == 1)//verticals
                        {
                            if ((btn[k].Text != "") && (btn[k - 3].Text == btn[k].Text) && (btn[k].Text == btn[k + 3].Text))
                            {
                                if (btn[k].Text == "X")
                                {
                                    MessageBox.Show("Crosses win!");
                                    crWin++;
                                }
                                else if (btn[k].Text == "O")
                                {
                                    MessageBox.Show("Noughts win!");
                                    nWin++;
                                }
                                move = 2;
                            }
                        }
                        if (i == 1 && j == 1)//diagonals
                        {
                            if ((btn[k].Text != "") && ((btn[0].Text == btn[k].Text) && (btn[k].Text == btn[8].Text)||
                                (btn[2].Text == btn[k].Text) && (btn[k].Text == btn[6].Text)))
                            {
                                if (btn[k].Text == "X")
                                {
                                    MessageBox.Show("Crosses win!");
                                    crWin++;
                                }
                                else if (btn[k].Text == "O")
                                {
                                    MessageBox.Show("Noughts win!");
                                    nWin++;
                                }
                                move = 2;
                            }
                        }
                        k++;
                    }
                    if (move == 2)
                    {
                        break;
                    }
                }
                //draw
                pos = 0;//cell is occupied
                for (i = 0; i < m * m; i++)
                {
                    if (btn[i].Text == "")
                    {
                        pos = 1;//cell is empty
                        break;
                    }
                }
                if (pos == 0 && move != 2)
                {
                    MessageBox.Show("It`s a draw!");
                    draw++;
                    move = 2;
                }
            }
        }

        public void GameOver()
        {
            if (move == 2)
            {
                MessageBox.Show("Game is over!"); 
            }
        }
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(3 * bw + 4 * dx, 3 * bh + 5 * dy + button1.Height);
            button1.Location = new Point(ClientSize.Width / 2 - button1.Width / 2, 3 * bh + 4 * dy);
            button1.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            ly = label1.Location.Y;
            label1.Location = new Point(ClientSize.Width / 2 - label1.Width / 2, ly);
            ly = button2.Location.Y;
            button2.Location = new Point(ClientSize.Width / 2 - button2.Width / 2, ly);
            ly = button3.Location.Y;
            button3.Location = new Point(ClientSize.Width / 2 - button3.Width / 2, ly);
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            ly = button1.Location.Y;
            button4.Location = new Point(ClientSize.Width / 2 - button1.Width - 20, ly - 7);
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            ly = button1.Location.Y;
            button5.Location = new Point(ClientSize.Width / 2 + button1.Width - 12, ly - 7);
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.ForeColor = Color.White;
            btn.BackColor = Color.DeepPink;
        }

        //computer can made a move, if cell is empty and game is not over
        public void Skynet()
        {
            int a;
            bool find = false;
            while(!find)
            {
                a = r.Next(0, 9);//find random empty cell
                if (btn[a].Text == "" && move != 2)
                {
                    if (n1 == false)
                    {
                        btn[a].Text = "O";
                        btn[a].ForeColor = Color.Green;
                    }
                    else
                    {
                        btn[a].Text = "X";
                        btn[a].ForeColor = Color.Red;
                    }
                    find = true;
                }
            }
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            
            if (btn.Text == "")
            {
                fs = 1;//first move has been made
                if (mode)
                {
                    if (move == 0)
                    {
                        btn.Text = "X";
                        btn.ForeColor = Color.Red;
                        move = 1;
                    }
                    else if (move == 1)
                    {
                        btn.Text = "O";
                        btn.ForeColor = Color.Green;
                        move = 0;
                    }
                }
                else
                {
                    if (move != 2)
                    {
                        if (!n1)
                        {
                            btn.Text = "X";
                            btn.ForeColor = Color.Red;
                        }
                        else
                        {
                            btn.Text = "O";
                            btn.ForeColor = Color.Green;
                        }
                        count++;
                        checkField();
                        if (move != 2 && count < 8)
                        {
                            Skynet();
                            count++;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This position is already occupied!");
            }
            checkField();
            GameOver();
        }
    }
}
