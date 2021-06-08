using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace _2048_final_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Game g = new Game();
        Bitmap bit = new Bitmap(400, 400);
        private string bitfile = "Score";
        string paths = "2048Save";
        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(paths))
                Loading();
            else
            {
                g.Reset();
                g.best = 0;
            }
            Call_drawing();
            pictureBox1.Refresh();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            save();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.BackgroundImage = bit;
            score.Text = g.score.ToString();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    g.Up();
                    break;
                case Keys.Down:
                    g.Down();
                    break;
                case Keys.Left:
                    g.Left();
                    break;
                case Keys.Right:
                    g.Right();
                    break;
                case Keys.F5:
                    g.Reset();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
            if (g.change)
            {
                g.Add();
                g.change = false;
            }
            Call_drawing();
            pictureBox1.Refresh();
            if (g.die)
                Gameover();
        }
        public void Call_drawing()
        {
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    Point p = new Point(i * 100 - 95, j * 100 - 95);
                    drawing(g.value[i, j], p);
                }
            }
        }
        public void drawing(int m, Point Coordinate)
        {
            Graphics gra = Graphics.FromImage(bit);

            switch (m)
            {
                case 0:
                    {
                        gra.FillRectangle(new SolidBrush(Color.BurlyWood), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 2:
                    {
                        gra.FillRectangle(new SolidBrush(Color.LightSalmon), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 4:
                    {
                        gra.FillRectangle(new SolidBrush(Color.Peru), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 8:
                    {
                        gra.FillRectangle(new SolidBrush(Color.Chocolate), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 16:
                    {
                        gra.FillRectangle(new SolidBrush(Color.Gray), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 32:
                    {
                        gra.FillRectangle(new SolidBrush(Color.DarkSeaGreen), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 64:
                    {
                        gra.FillRectangle(new SolidBrush(Color.Gold), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 128:
                    {
                        gra.FillRectangle(new SolidBrush(Color.HotPink), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 256:
                    {
                        gra.FillRectangle(new SolidBrush(Color.DarkOrange), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 512:
                    {
                        gra.FillRectangle(new SolidBrush(Color.LightPink), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 1024:
                    {
                        gra.FillRectangle(new SolidBrush(Color.DarkRed), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
                case 2048:
                    {
                        gra.FillRectangle(new SolidBrush(Color.Red), Coordinate.X, Coordinate.Y, 90, 90);
                    }
                    break;
            }

            switch (m)
            {
                case 2:
                case 4:
                case 8:
                    {
                        gra.DrawString(m.ToString(), new Font("隶书", 40.5f, FontStyle.Bold), new SolidBrush(Color.White), Coordinate.X + 22, Coordinate.Y + 17);
                    }
                    break;
                case 16:
                case 32:
                case 64:
                    {
                        gra.DrawString(m.ToString(), new Font("隶书", 40.5f, FontStyle.Bold), new SolidBrush(Color.White), Coordinate.X + 8, Coordinate.Y + 17);
                    }
                    break;
                case 128:
                case 256:
                case 512:
                    {
                        gra.DrawString(m.ToString(), new Font("隶书", 35.5f, FontStyle.Bold), new SolidBrush(Color.White), Coordinate.X + 0, Coordinate.Y + 20);
                    }
                    break;
                case 1024:
                case 2048:
                case 4096:
                case 8192:
                    {
                        gra.DrawString(m.ToString(), new Font("隶书", 30.5f, FontStyle.Bold), new SolidBrush(Color.White), Coordinate.X - 4, Coordinate.Y + 23);
                    }
                    break;
                default:
                    break;
            }
        }
        public void save()
        {
            FileStream fs = new FileStream(paths, FileMode.Create, FileAccess.Write);
            BinaryFormatter b_write = new BinaryFormatter();
            b_write.Serialize(fs, g);
            fs.Close();
        }
        public void Loading()
        {
            FileStream fs = new FileStream(paths, FileMode.Open, FileAccess.Read);
            BinaryFormatter b_read = new BinaryFormatter();
            g = (Game)b_read.Deserialize(fs);
            score.Text = g.score.ToString();
            best.Text = g.best.ToString();
            fs.Close();
        }
        public void Gameover()
        {

            GameOver_Form over = new GameOver_Form();
            over.score = g.score.ToString();
            if (g.score > g.best)
            {
                g.best = g.score;
                best.Text = g.best.ToString();
            }
            over.best = g.best.ToString();
            DialogResult s = over.ShowDialog();
            if (s == DialogResult.Retry)
            {
                g.Reset();
                Call_drawing();
                this.pictureBox1.Refresh();
            }
            else if (s == DialogResult.No)
            {
                g.Reset();
                this.Close();
            }
        }
    }
}
