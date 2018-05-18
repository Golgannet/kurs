using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public List<List<archer_comp>> comp_smena = null;
        private void Form4_Load(object sender, EventArgs e)
        {

            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                //string s = main.textBox1.Text;
                //main.textBox1.Text = "OK";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.ShowDialog();
        }
    }
}
