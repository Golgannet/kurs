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

        private void button8_Click(object sender, EventArgs e)
        {
            CallBackMy.callbackEventHandler(Convert.ToInt32(numericUpDown1.Value));
            Close();
        }
        

        
    }
}
