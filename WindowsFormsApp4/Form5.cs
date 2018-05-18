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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        public List<List<archer_comp>> comp_smena = null;
        Form1 f = new Form1();
        private void button8_Click(object sender, EventArgs e)
        {            
            Form4 f4 = new Form4();
            foreach (archer_comp archer in f4.comp_smena[f4.tabControl1.SelectedIndex])
            {
                archer.stand = "1";                   
            }
            prosedures.func.archer_update(f.smena_tables[f4.tabControl1.SelectedIndex], f.comp_smena[f4.tabControl1.SelectedIndex]);
            ////Form1 f = new Form1();
            //Form4 f4 = new Form4();
            //f.this_smena.Clear();
            ////category_halder category = Form1.categories2[f4.tabControl1.SelectedIndex];
            //string antibugg;
            //foreach (string categ in category.d.Text.Split(','))
            //{
            //    antibugg = categ;
            //    if (categ[0] == ' ')
            //        antibugg = categ.Remove(0, 1);
            //    foreach (archer_comp archer in f.this_comp)
            //    {
            //        f.this_smena.Add(archer);
            //    }
            //}
            //double sheild_col = Convert.ToInt32(numericUpDown1.Value);
            //int l = Convert.ToInt32(numericUpDown1.Value);
            //double archer_col = f.this_smena.Count();                        
            //string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H" };
            //List<string> letters2 = new List<string>() { };
            //Random rnd = new Random();            
            //double x;
            //int k = 0;
            //for (var i = 0; i < l; i++)
            //{
            //    x = Math.Ceiling(archer_col / sheild_col);
            //    for (var j = 0; j < x; j++)
            //    {
            //        if (k >= f.this_smena.Count())
            //            break;
            //        archer_comp archer = f.this_smena[k];
            //        archer.stand = (i + 1).ToString();
            //        k++;
            //        archer_col--;
            //    }
            //    sheild_col--;
            //}
            //f.smena_tables[f4.tabControl1.SelectedIndex].Rows.Clear();
            //f.smena_ubdate();
            ////smena_ubdate();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //Form1 f = new Form1();
            numericUpDown1.Minimum = Convert.ToDecimal(f.this_smena.Count() / 8);
        }
    }
}
