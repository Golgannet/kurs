using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    class table_names
    {
        public string bowtype { get; set; }
        public bool pol;
        public char pol1
        {
            get
            {
                if (pol)
                {
                    return ('М');
                }
                else
                {
                    return ('Ж');
                }
            }
        }
        public string name { get; set; }
        public int id { get; set; }
        public string FullName
        {
            get
            {
                return name + "                " + bowtype + "                 " + pol1;
            }
        }

    }
    public class category_halder
    {
        public TextBox a;
        public ComboBox b;
        public ComboBox c;
        public ComboBox f;
        public CheckComboBoxTest.CheckedComboBox d;
        public string title()
        {
            if (c.Text != "и младше" && c.Text != "и старше")
            {
                return (a.Text + " " + b.Text + "-" + c.Text);
            }
            else
            {
                return (a.Text + " " + b.Text + " " + c.Text);
            }

        }
        public string query()
        {
            if (b.Text == "все года")
            {
                return ("select * from users ORDER BY bow_type  ASC, pol DESC, name  ASC");
            }
            else if (c.Text == "и младше")
            {
                return ("SELECT * from users where strftime('%Y',age) >= '" + b.Text + "' ORDER BY bow_type  DESC, pol DESC, name  ASC ;");
            }
            else if (c.Text == "и старше")
            {
                return ("SELECT * from users where strftime('%Y',age) <= '" + b.Text + "' ORDER BY bow_type  DESC, pol DESC, name  ASC ;");
            }
            else
            {
                return ("SELECT * from users where strftime('%Y',age) BETWEEN '" + b.Text + "' AND '" + c.Text + "'  ORDER BY bow_type  DESC, pol DESC, name  ASC ;");
            }

        }
    }
    class round_set
    {
        public string id_aecher;
        public string[] array_id_rounds;
    }
    public class round
    {
        public string id;
        public string result;
        public string distantion;
    }
    class checheckedComboBox_category
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    class competitioons
    {
        public string id;
        public string name;
        public string set_rounds;
        public string smena_id;
        public DateTime start_date;
        public DateTime end_date;
    }
    class set
    {
        public string id;
        public string id_archer;
        public string rounds;
        public string sparrings;
        public string category;
        public string latter;
        public string stand;
    }
    public class archer_comp
    {
        public string category_id;
        public string perfomanse_id;
        public string stand;
        public string letter;
        public string id;
        public int plase;
        public string fio;
        public DateTime age;
        public string rang;
        public string region;
        public string bow_type;
        public string vedomstvo;
        public string sp_organization;
        public List<round> rounds = new List<round>();        
        public bool pol;
        public string round        
        {
            get
            {
                string ret = "";                
                foreach (round round in rounds)
                {
                        if (round.result.ToString() != "")
                            ret += round.result.ToString() + "  ";
                        else
                            ret += "0  ";
                    }
                return ret;
            }
        }
        public char pols
        {
            get
            {
                if (pol)
                {
                    return ('М');
                }
                else
                {
                    return ('Ж');
                }
            }
        }
        public int summ;
        public byte tens;
        public byte x;
        public string normativ;
        public string description;

    }
    
}
