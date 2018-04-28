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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        int k = 0;
        int l = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            label8.Text += "x,";
            k++;
            if (k == 3)
            {
                user_next();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label8.Text += "10,";
            k++;
            if (k == 3)
            {
                user_next();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label8.Text += "9,";
            k++;
            if (k == 3)
            {
                user_next();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label8.Text += "8,";
            k++;
            if (k == 3)
            {
                user_next();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label8.Text += "7,";
            k++;
            if (k == 3)
            {
                user_next();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label8.Text += "6,";
            k++;
            if (k == 3)
            {
                user_next();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label8.Text += "M,";
            k++;
            if (k == 3)
            {
                user_next();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            label8.Text = "";
            user_next();
        }
        public List<archer_comp> smena = new List<archer_comp>() { };
        private void user_next()
        {            
               
            if (l >= smena.Count())
                this.Close();
            label5.Text = smena[l].fio;
            label6.Text = smena[l].stand;
            label7.Text = smena[l].summ.ToString();
            
            if (k == 3)
            {
                label8.Text = label8.Text.Remove(label8.Text.Length - 1, 1);                
                string[] shots = label8.Text.Split(',');
                label8.Text = "";
                int sum = 0;
                foreach (string shoot in shots)
                {
                    if (shoot == "x")
                    {
                        sum += 10;
                    }
                    else if (shoot == "M")
                    {
                        sum += 0;
                    }
                    else 
                    {
                        sum += Convert.ToInt32(shoot);
                    }
                }
                smena[l].summ += sum;
                l++;
            }
            
        }
        
    }
}
