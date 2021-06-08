using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_final_project
{
    public partial class GameOver_Form : Form
    {
        public string score;
        public string best;
        public GameOver_Form()
        {
            InitializeComponent();
        }

        private void GameOver_Form_Load(object sender, EventArgs e)
        {
            label2.Text += score;
            label3.Text += best;
            this.TopLevel = true;
        }
    }
}
