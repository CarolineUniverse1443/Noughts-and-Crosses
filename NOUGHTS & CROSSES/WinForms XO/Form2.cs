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
    public partial class Form2 : Form
    {
        public int ly;
        public int pr;

        public Form2()
        {
            InitializeComponent();
            ly = label1.Location.Y;
            label1.Location = new Point(ClientSize.Width / 2 - label1.Width / 2,ly);
            ly = pictureBox1.Location.Y;
            pictureBox1.Location = new Point(ClientSize.Width / 2 - pictureBox1.Width / 2, ly);
            ly = progressBar1.Location.Y;
            progressBar1.Location = new Point(ClientSize.Width / 2 - progressBar1.Width / 2, ly);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(label1.Text.Length < 10)
            {
                label1.Text += ".";
            }
            else
            {
                label1.Text = "Loading";
            }
            pr += 5;
            progressBar1.Value = pr;
            if(pr == 100)
            {
                pr = 0;
                timer1.Stop();
                label1.Text = "Done";
                ly = label1.Location.Y;
                label1.Location = new Point(ClientSize.Width / 2 - label1.Width / 2, ly);
                this.Hide();

                Form1 f1 = new Form1();
                f1.Show();
            }
        }
    }
}
