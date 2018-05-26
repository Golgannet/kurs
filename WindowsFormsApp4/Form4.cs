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
            CallBackMy.callbackEventHandler = new CallBackMy.callbackEvent(this.Reload);
            CallBackString.callbackEventHandler = new CallBackString.callbackEvent(this.get_shot);
            InitializeComponent();

        }
        public List<List<archer_comp>> comp_smena = null;
        private void Form4_Load(object sender, EventArgs e)
        {            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float col = (comp_smena.Count() / 8);
            int table_id = tabControl1.SelectedIndex;
            Form5 frm = new Form5();
            Form1 main = this.Owner as Form1;
            frm.numericUpDown1.Minimum = Convert.ToDecimal(col);
            frm.ShowDialog();
            main.smena_tables[table_id].Rows.Clear();
            foreach (archer_comp archer in comp_smena[table_id])
            {
                archer.stand = "1";
            }
            prosedures.func.archer_update(main.smena_tables[table_id], comp_smena[table_id]);
        }
        void Reload(int param)
        {
            //button2.Text += param.ToString();
        }
        void get_shot(string param)
        {
            //button2.Text += param.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 frm = new Form6();            
            foreach (archer_comp archer in comp_smena[tabControl1.SelectedIndex])
            {
                frm.name = archer.fio;
            }
            
        }
        //    Form4 f4 = new Form4();
        //        foreach (archer_comp archer in f4.comp_smena[f4.table_id])
        //        {
        //            archer.stand = "1";                   
        //        }
        //prosedures.func.archer_update(f.smena_tables[f4.table_id], f.comp_smena[f4.table_id]);
        ////Form1 f = new Form1();
        //Form4 f4 = new Form4();
        //f.this_smena.Clear();
        ////category_halder category = Form1.categories2[f4.table_id];
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
        //f.smena_tables[f4.table_id].Rows.Clear();
        //f.smena_ubdate();
        ////smena_ubdate();
    }
}
