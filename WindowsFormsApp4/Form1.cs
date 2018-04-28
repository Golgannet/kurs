
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private String dbFileName;
        //private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        //bool a = false;
        public SQLiteConnection m_dbConn;





        public Form1()

        {
            InitializeComponent();
        }

        public void competition_list()
        {
            string sqlQuery = "SELECT id,name,start_date from competition";
            SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            int year_buf = 0;            
            while (reader.Read())
            {
                int year = Convert.ToDateTime(reader["start_date"]).Year;
                if (year != year_buf )
                {
                    TreeNode yNode = new TreeNode();
                    yNode.Text = (year.ToString());
                    string name = "y" + year.ToString();
                    yNode.Name = name;                    
                    treeView1.Nodes.Add(yNode);
                    year_buf = year;
                    treeView1.SelectedNode = yNode;
                }                
                TreeNode newNode = new TreeNode(year.ToString());
                newNode.Name = reader["id"].ToString();
                newNode.Text = reader["name"].ToString();
                treeView1.SelectedNode.Nodes.Add(newNode);                
                //archer.category_id = reader1["id_category"].ToString();
            }
            //treeView1.ad
            //return ("");
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            checkedComboBox1.Items.Add(12);
            checkedComboBox1.Items.Add(18);
            checkedComboBox1.Items.Add(30);
            checkedComboBox1.Items.Add(40);
            checkedComboBox1.Items.Add(50);
            checkedComboBox1.Items.Add(60);
            checkedComboBox1.Items.Add(70);
            checkedComboBox1.Items.Add(90);
            checkedComboBox1.Items.Add(70 / 50);
            checkedComboBox1.Items.Add(90 / 70);
            checkedComboBox1.Items.Add(70 / 60);
            int i5 = 1;
            foreach (DataGridViewColumn obj in dataGridView3.Columns)
            {
                i5++;
                DataGridViewColumn col = new DataGridViewColumn();
                col.Width = obj.Width;
                col.CellTemplate = new DataGridViewTextBoxCell();
                col.HeaderText = obj.HeaderText;
                col.Name = "col" + i5.ToString();
                dataGridView2.Columns.Add(col);
            }
            category_halder item = new category_halder();
            item.a = textBox2;
            item.b = comboBox3;
            item.c = comboBox4;
            item.d = checkedComboBox1;
            categories.Add(item);

            m_sqlCmd = new SQLiteCommand();
            dbFileName = "sample2.sqlite";
            //lbStatusText.Text = "Disconnected";
            if (!File.Exists(dbFileName))
                SQLiteConnection.CreateFile(dbFileName);
            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();    
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            Form2 example2 = new Form2();
            if (!example2.cost)
            {
                example2.ShowDialog();
                this.Show();
            }
            if (File.Exists("this_comp.txt"))
            {
                FileStream file1 = new FileStream("this_comp.txt", FileMode.Open);
                StreamReader reader = new StreamReader(file1);
                string id = (reader.ReadToEnd());
                reader.Close();
                if (id != "")
                {
                    splitContainer2.Enabled = false;
                }

            }
            competition_list();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;
            //textBox1.Text = treeView1.SelectedNode.Text;
            if (dataGridView2.RowCount == 0)
            {
                dataGridView2.Columns.Add("tn_ob", "tn_ob2");
                dataGridView2.Columns.Add("tn_ob", "tn_ob2");
                dataGridView2.Columns.Add("tn_ob", "tn_ob2");
            }
            // sqlQuery = "SELECT users.COLUMN_NAME FROM all_tab_columns users;";
            sqlQuery = "select * from competition where id = 1";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
            adapter.Fill(dTable);
           // for (int i = 0; i < dTable.Rows.Count; i++)
                //dataGridView2.Rows.Add(dTable.Rows[i].ItemArray);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            competitioons competition = new competitioons();
            competition.name = textBox3.Text;
            competition.start_date = (dateTimePicker1.Value);
            competition.end_date = (dateTimePicker2.Value);
            string sqlQuery = "";
            SQLiteCommand command = new SQLiteCommand();
            int i = 0;
            foreach (CheckedListBox l1 in tables)
            {
                category_halder a = categories[i];
                sqlQuery = "INSERT INTO  categories (name, min_date, max_date)VALUES ('" + a.a.Text + "','" + a.b.Text + "','" + a.c.Text + "');";
                command = new SQLiteCommand(sqlQuery, m_dbConn);
                command.ExecuteNonQuery();
                sqlQuery = "SELECT id FROM categories WHERE id=last_insert_rowid();";
                command = new SQLiteCommand(sqlQuery, m_dbConn);
                int id_category = Convert.ToInt32(command.ExecuteScalar());
                List<string> dist = new List<string>() { };
                dist.AddRange(a.d.Text.Split(','));
                if (dist.Count() == 1)
                    dist.Add(dist[0]);
                foreach (Object obj in l1.CheckedItems)
                {
                    table_names archer = (table_names)obj;
                    set set = new set();
                    set.id_archer = archer.id.ToString();
                    set.category = id_category.ToString();
                    for (int j = 0; j < dist.Count(); j++)
                    {
                        if (j != 0)
                            set.rounds += ",";
                        sqlQuery = "INSERT INTO  rounds (distantion) values ('" + dist[i] + "');";
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        command.ExecuteNonQuery();
                        sqlQuery = "SELECT id FROM rounds WHERE id=last_insert_rowid();";
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        int id_round = Convert.ToInt32(command.ExecuteScalar());
                        set.rounds += id_round;
                    }
                    sqlQuery = "INSERT INTO  'set' (id_archer, array_id_rounds,id_category )VALUES ('" + set.id_archer + "','" + set.rounds + "','" + set.category + "');";
                    command = new SQLiteCommand(sqlQuery, m_dbConn);
                    command.ExecuteNonQuery();
                    sqlQuery = "SELECT id FROM 'set' WHERE id=last_insert_rowid();";
                    command = new SQLiteCommand(sqlQuery, m_dbConn);
                    string id_set = command.ExecuteScalar().ToString();
                    competition.set_rounds += id_set + ",";
                }
                i++;
            }
            competition.set_rounds = competition.set_rounds.Substring(0, competition.set_rounds.Length - 1);
            sqlQuery = "INSERT INTO  competition (name, array_rounds_sets, start_date, end_date)VALUES ('" + competition.name + "','" + competition.set_rounds + "','" + competition.start_date.ToString("yyyy-MM-dd") + "','" + competition.end_date.ToString("yyyy-MM-dd") + "');";
            command = new SQLiteCommand(sqlQuery, m_dbConn);
            command.ExecuteNonQuery();
            if (!File.Exists("this_comp.txt"))
            {
                File.Create("this_comp.txt").Close(); ;
            }
            sqlQuery = "SELECT id FROM 'competition' order by id desc limit 1";
            command = new SQLiteCommand(sqlQuery, m_dbConn);
            string id = command.ExecuteScalar().ToString();
            File.AppendAllText("this_comp.txt", id);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            //listBox2.Items.Add(listBox1.SelectedItem);
            //listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[0];
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            // listBox2.Items.RemoveAt(listBox2.SelectedIndex);
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void dynamik_button(string text, string d1, string d2)
        {

        }

        List<category_halder> categories = new List<category_halder>() { };

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            categories.Clear();
            panel1.Controls.Clear();
            for (var i = 0; i < Convert.ToInt32(comboBox2.Text); i++)
            {
                TextBox a = textBox2;
                ComboBox b = comboBox3;
                ComboBox c = comboBox4;
                CheckComboBoxTest.CheckedComboBox d = checkedComboBox1;
                TextBox temp1 = new TextBox();
                ComboBox temp2 = new ComboBox();
                ComboBox temp3 = new ComboBox();
                CheckComboBoxTest.CheckedComboBox temp4 = new CheckComboBoxTest.CheckedComboBox();
                temp1.Name = "temp1." + i.ToString();
                temp2.Name = "temp2." + i.ToString();
                temp3.Name = "temp3." + i.ToString();
                temp4.Name = "temp4." + i.ToString();
                temp1.Width = a.Width;
                temp4.Width = d.Width;
                foreach (var obj in b.Items)
                    temp2.Items.Add(obj);
                foreach (var obj in c.Items)
                    temp3.Items.Add(obj);
                foreach (var obj in d.Items)
                    temp4.Items.Add(obj);
                temp1.Location = new Point(a.Location.X, a.Location.Y + a.Height * i + 25 * i);
                temp2.Location = new Point(b.Location.X, b.Location.Y + b.Height * i + 24 * i);
                temp3.Location = new Point(c.Location.X, c.Location.Y + c.Height * i + 24 * i);
                temp4.Location = new Point(d.Location.X, d.Location.Y + d.Height * i + 24 * i);
                temp2.DropDownStyle = ComboBoxStyle.DropDownList;
                temp3.DropDownStyle = ComboBoxStyle.DropDownList;
                panel1.Controls.Add(temp1);
                panel1.Controls.Add(temp2);
                panel1.Controls.Add(temp3);
                panel1.Controls.Add(temp4);
                category_halder item = new category_halder();
                item.a = temp1;
                item.b = temp2;
                item.c = temp3;
                item.d = temp4;
                categories.Add(item);
            }

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        List<CheckedListBox> tables = new List<CheckedListBox>() { };
        private void button4_Click(object sender, EventArgs e)
        {
            tables.Clear();
            tabControl2.TabPages.Clear();
            foreach (category_halder category in categories)
            {
                string title = (category.title());
                TabPage myTabPage = new TabPage(title);
                tabControl2.TabPages.Add(myTabPage);
                CheckedListBox l1 = new CheckedListBox();
                l1.Dock = DockStyle.Fill;
                myTabPage.Controls.Add(l1);
                CheckedListBox c1 = new CheckedListBox();
                string sqlQuery;
                DataTable dTable = new DataTable();
                sqlQuery = category.query();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConn);
                SQLiteDataReader reader = command.ExecuteReader();
                dTable = new DataTable();
                adapter.Fill(dTable);
                command = new SQLiteCommand(sqlQuery, m_dbConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    table_names archer = new table_names();
                    archer.name = reader["name"].ToString();
                    archer.id = Convert.ToInt32(reader["id"]);
                    archer.bowtype = reader["bow_type"].ToString();
                    archer.pol = Convert.ToBoolean(reader["pol"]);
                    c1.DisplayMember = archer.name;
                    c1.Items.Add(archer);
                }
                c1.Dock = DockStyle.Fill;
                c1.DisplayMember = "FullName";
                c1.ValueMember = "id";
                l1.Controls.Add(c1);
                tables.Add(c1);

            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            //string sqlQuery = "select * from users ";
            //DataTable dTable = new DataTable();
            //SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
            //SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConn);
            //SQLiteDataReader reader = command.ExecuteReader();
            //dTable = new DataTable();
            //adapter.Fill(dTable);
            //command = new SQLiteCommand(sqlQuery, m_dbConn);
            //reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    string a2 = reader["age"].ToString();
            //    string bufer = "";
            //    char[] array1 = new char[] { a2[6], a2[7], a2[8], a2[9], a2[5], a2[3], a2[4], a2[5], a2[0], a2[1] };
            //    foreach (char a3 in array1)
            //    {
            //        bufer += a3;
            //    }
            //    string sqlQuery1 = "UPDATE users SET age = '" + bufer + "' WHERE id = " + reader["id"].ToString() + " ;";
            //    SQLiteCommand command1 = new SQLiteCommand(sqlQuery1, m_dbConn);
            //    command1.ExecuteNonQuery();

            //}
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (!File.Exists("this_comp.txt"))
            {
                File.Create("this_comp.txt").Close(); ;
            }
            string sqlQuery = "SELECT id FROM 'competition' order by id desc limit 1";
            SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConn);
            string id = command.ExecuteScalar().ToString();
            File.AppendAllText("this_comp.txt", id);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Random rnd = new Random();
            for (int i = (data.Count()) - 1; i >= 1; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = data[j];
                data[j] = data[i];
                data[i] = temp;
            }
            foreach (int k in data)
            {
                label3.Text += k.ToString() + " ";
            }
        }

        List<archer_comp> this_comp = new List<archer_comp>() { };
        private void button6_Click_1(object sender, EventArgs e)
        {
            FileStream file1 = new FileStream("this_comp.txt", FileMode.Open);
            StreamReader reader2 = new StreamReader(file1);
            string id2 = (reader2.ReadToEnd());
            reader2.Close();
            string sqlQuery = "SELECT * from competition where competition.id = " + id2 + ";";
            string sqlQuery2 = "";
            string categ = "";
            SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                competitioons thiscomp = new competitioons();
                thiscomp.name = reader["name"].ToString();
                thiscomp.set_rounds = reader["array_rounds_sets"].ToString();
                thiscomp.sparrings = reader["id_sparrings"].ToString();
                thiscomp.start_date = Convert.ToDateTime(reader["start_date"]);
                thiscomp.end_date = Convert.ToDateTime(reader["end_date"]);
                string[] sets = thiscomp.set_rounds.Split(',');
                string pol_buf = "";
                string pol_buf1 = "";
                string category_buf = "";
                string category_buf1 = "";
                string bow_buf = "";
                string bow_buf1 = "";
                string desc = "";
                foreach (string id in sets)
                {
                    sqlQuery = "SELECT * from 'set' where id = " + id + " ORDER BY id_category asc;";
                    command = new SQLiteCommand(sqlQuery, m_dbConn);
                    SQLiteDataReader set_reader = command.ExecuteReader();
                    while (set_reader.Read())
                    {
                        set set1 = new set();
                        archer_comp archer = new archer_comp();
                        set1.id = set_reader["id"].ToString();
                        set1.id_archer = set_reader["id_archer"].ToString();
                        set1.rounds = set_reader["array_id_rounds"].ToString();
                        set1.stand = set_reader["stand"].ToString();
                        set1.latter = set_reader["latter"].ToString();
                        set1.category = set_reader["id_category"].ToString();
                        category_buf = set1.category;
                        archer.id = set1.id_archer;
                        string[] rounds = set1.rounds.Split(',');
                        foreach (string id1 in rounds)
                        {
                            sqlQuery = "SELECT * from 'rounds' where id = " + id1 + ";";
                            command = new SQLiteCommand(sqlQuery, m_dbConn);
                            SQLiteDataReader round_reader = command.ExecuteReader();
                            while (round_reader.Read())
                            {
                                round round1 = new round();
                                round1.id = round_reader["id"].ToString();
                                round1.result = round_reader["result"].ToString();
                                round1.distantion = round_reader["distantion"].ToString();
                                if (round1.result != "")
                                {
                                    archer.summ += Convert.ToInt32(round1.result);
                                }
                                archer.rounds.Add(round1);
                            }
                        }
                        sqlQuery = "select * from `users` where id = '" + archer.id + "' ORDER BY bow_type  ASC, pol DESC";
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        SQLiteDataReader archer_reader = command.ExecuteReader();
                        while (archer_reader.Read())
                        {
                            archer.fio = archer_reader["name"].ToString();
                            archer.age = Convert.ToDateTime(archer_reader["age"]);
                            archer.region = archer_reader["region"].ToString();
                            archer.rang = archer_reader["rang"].ToString();
                            archer.sp_organization = archer_reader["sp_organization"].ToString();
                            archer.vedomstvo = archer_reader["vedomost"].ToString();
                            archer.pol = Convert.ToBoolean(archer_reader["pol"].ToString());
                            pol_buf = archer_reader["pol"].ToString();
                            bow_buf = archer_reader["bow_type"].ToString();
                        }
                        if (category_buf != category_buf1 || pol_buf != pol_buf1 || bow_buf != bow_buf1)
                        {
                            sqlQuery = "SELECT * from categories where id = " + category_buf.ToString() + "";
                            SQLiteCommand command3 = new SQLiteCommand(sqlQuery, m_dbConn);
                            SQLiteDataReader cat_reader = command3.ExecuteReader();
                            dataGridView3.Rows.Add();
                            dataGridView3.Rows.Add();
                            while (cat_reader.Read())
                            {

                                int number;
                                if (Int32.TryParse(cat_reader["max_date"].ToString(), out number))
                                {
                                    categ = cat_reader["name"].ToString() + "  " + cat_reader["min_date"].ToString() + "-" + cat_reader["max_date"].ToString() + "  " + archer.pols + "  ";
                                }
                                else
                                {
                                    categ = cat_reader["name"].ToString() + "  " + cat_reader["min_date"].ToString() + "  " + cat_reader["max_date"].ToString() + "  " + archer.pols + "  ";
                                }
                                dataGridView3.Rows.Add("", "", categ + bow_buf);
                                if (bow_buf == "Классический лук")
                                {
                                    desc = (categ + "КЛ");
                                    checkedComboBox3.Items.Add(categ + "КЛ");
                                    sqlQuery2 = "UPDATE 'set' SET description = '" + categ + "КЛ" + "' WHERE id = " + set1.id.ToString() + " ;";
                                }
                                else if (bow_buf == "Блочный лук")
                                {
                                    desc = (categ + "БЛ");
                                    checkedComboBox3.Items.Add(categ + "БЛ");
                                    sqlQuery2 = "UPDATE 'set' SET description = '" + categ + "БЛ" + "' WHERE id = " + set1.id.ToString() + " ;";
                                }
                                SQLiteCommand upd_command = new SQLiteCommand(sqlQuery2, m_dbConn);
                                upd_command.ExecuteNonQuery();
                            }
                            dataGridView3.Rows.Add
                            (
                                archer.stand,
                                archer.plase,
                                archer.fio,
                                archer.age.ToString("dd:MM:yyyy"),
                                archer.rang,
                                archer.region,
                                archer.vedomstvo,
                                archer.sp_organization,
                                archer.round,
                                archer.tens,
                                archer.x,
                                archer.summ
                            );
                        }
                        else
                        {
                            SQLiteCommand upd_command = new SQLiteCommand("UPDATE 'set' SET description = '" + categ + "БЛ" + "' WHERE id = " + set1.id.ToString() + " ;", m_dbConn);
                            upd_command.ExecuteNonQuery();
                            dataGridView3.Rows.Add(
                            archer.stand,
                            archer.plase,
                            archer.fio,
                            archer.age.ToString("dd:MM:yyyy"),
                            archer.rang,
                            archer.region,
                            archer.vedomstvo,
                            archer.sp_organization,
                            archer.round,
                            archer.tens,
                            archer.x,
                            archer.summ);
                        }
                        category_buf1 = category_buf;
                        pol_buf1 = pol_buf;
                        bow_buf1 = bow_buf;
                        archer.description = desc;
                        this_comp.Add(archer);
                    }

                }

            }


        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        List<category_halder> categories2 = new List<category_halder>() { };
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            categories2.Clear();
            panel3.Controls.Clear();
            for (var i = 0; i < (numericUpDown2.Value); i++)
            {
                TextBox a = textBox4;
                CheckComboBoxTest.CheckedComboBox d = checkedComboBox3;
                d.DisplayMember = "FullName";
                d.ValueMember = "id";
                TextBox temp1 = new TextBox();
                CheckComboBoxTest.CheckedComboBox temp2 = new CheckComboBoxTest.CheckedComboBox();
                temp1.Name = "temp1." + i.ToString();
                temp2.Name = "temp2." + i.ToString();
                temp1.Text = ("смена " + (i + 1).ToString());
                temp1.Width = a.Width;
                temp2.Width = d.Width;
                foreach (var obj in d.Items)
                    temp2.Items.Add(obj);
                temp1.Location = new Point(a.Location.X, a.Location.Y + a.Height * i + 35 * i);
                temp2.Location = new Point(d.Location.X, d.Location.Y + d.Height * i + 36 * i);
                panel3.Controls.Add(temp1);
                panel3.Controls.Add(temp2);
                category_halder item = new category_halder();
                item.a = temp1;
                item.d = temp2;
                categories2.Add(item);
            }
        }
        List<DataGridView> smena_tables = new List<DataGridView>() { };
        private void button7_Click(object sender, EventArgs e)
        {

            foreach (category_halder category in categories2)
            {
                //button7.Text = checkedComboBox2.Text;
                TabPage myTabPage = new TabPage(category.a.Text);
                tabControl3.TabPages.Add(myTabPage);
                DataGridView l1 = new DataGridView();
                l1.Dock = DockStyle.Fill;
                myTabPage.Controls.Add(l1);
                int i5 = 1;
                foreach (DataGridViewColumn obj in dataGridView3.Columns)
                {
                    i5++;
                    DataGridViewColumn col = new DataGridViewColumn();
                    col.Width = obj.Width;
                    col.CellTemplate = new DataGridViewTextBoxCell();
                    col.HeaderText = obj.HeaderText;
                    col.Name = "col" + i5.ToString();
                    l1.Columns.Add(col);
                }
                
                l1.ReadOnly = true;
                l1.AllowUserToAddRows = false;
                l1.AllowUserToDeleteRows = false;
                string antibugg;
                foreach (string categ in category.d.Text.Split(','))
                {
                    l1.Rows.Add("", "", categ);
                    antibugg = categ;
                    if (categ[0] == ' ')
                        antibugg = categ.Remove(0, 1);
                    foreach (archer_comp archer in this_comp)
                    {
                        if (archer.description == antibugg)
                        {
                            l1.Rows.Add(
                            archer.stand,
                            archer.plase,
                            archer.fio,
                            archer.age.ToString("dd:MM:yyyy"),
                            archer.rang,
                            archer.region,
                            archer.vedomstvo,
                            archer.sp_organization,
                            archer.round,
                            archer.tens,
                            archer.x,
                            archer.summ);
                        }
                    }
                    l1.Rows.Add();
                }
                smena_tables.Add(l1);
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }
        private void smena_ubdate()
        {
            foreach (category_halder category in categories2)
            {
                category_halder category1 = categories2[tabControl3.SelectedIndex - 1];
                string antibugg;
                DataGridView l1 = smena_tables[tabControl3.SelectedIndex-1];
                foreach (string categ in category.d.Text.Split(','))
                {
                    l1.Rows.Add("", "", categ);
                    antibugg = categ;
                    if (categ[0] == ' ')
                        antibugg = categ.Remove(0, 1);
                    foreach (archer_comp archer in this_comp)
                    {
                        if (archer.description == antibugg)
                        {
                            l1.Rows.Add(
                            archer.stand,
                            archer.plase,
                            archer.fio,
                            archer.age.ToString("dd:MM:yyyy"),
                            archer.rang,
                            archer.region,
                            archer.vedomstvo,
                            archer.sp_organization,
                            archer.round,
                            archer.tens,
                            archer.x,
                            archer.summ);
                        }
                    }
                    l1.Rows.Add();
                }
            }
        }

        private void tabControl3_TabIndexChanged(object sender, EventArgs e)
        {
        }


        public List<archer_comp> this_smena = new List<archer_comp>() { };
        private void button8_Click(object sender, EventArgs e)
        {
            this_smena.Clear();
            category_halder category = categories2[tabControl3.SelectedIndex - 1];
            string antibugg;
            foreach (string categ in category.d.Text.Split(','))
            {
                antibugg = categ;
                if (categ[0] == ' ')
                    antibugg = categ.Remove(0, 1);
                foreach (archer_comp archer in this_comp)
                {
                    this_smena.Add(archer);
                }
            }
            
            double sheild_col = Convert.ToInt32(numericUpDown1.Value);
            int l = Convert.ToInt32(numericUpDown1.Value);
            double archer_col = this_smena.Count();
            numericUpDown1.Minimum = Convert.ToDecimal(archer_col / 8);
            if (archer_col % 8 != 0)
                numericUpDown1.Minimum++;
            //archer_comp archer = this_smena[k];
            //archer.stand += letters2[j];
            string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H" };
            List<string> letters2 = new List<string>() { };
            Random rnd = new Random();            
            smena_ubdate();
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            button7.Text = "";
            //string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H" };
            //List<string> letters2 = new List<string>() { };
            //Random rnd = new Random();
            //int x = 5;
            //for (int p = 0; p < x; p++)
            //{
            //    letters2.Add(letters[p]);
            //}
            //for (int p = (letters2.Count) - 1; p >= 1; p--)
            //{
            //    int o = rnd.Next(p + 1);
            //    var temp = letters2[o];
            //    letters2[o] = letters2[o];
            //    letters2[p] = temp;
            //}
            //foreach (string letter in letters2)
            //{
            //    button7.Text += letter;
            //}


            //List<string> data = new List<string>() {};            
            //for (int p = 0; p < 5; p++)
            //{
            //    data.Add(letters[p]);
            //}
            //Random rnd = new Random();
            //for (int i = (data.Count()) - 1; i >= 1; i--)
            //{
            //    int j = rnd.Next(i + 1);
            //    var temp = data[j];
            //    data[j] = data[i];
            //    data[i] = temp;
            //}
            //foreach (string k in data)
            //{
            //    button7.Text += k.ToString() + " ";
            //}
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl3_Click(object sender, EventArgs e)
        {

            if (tabControl3.SelectedIndex != 0)
            {
                panel2.Visible = true;
                panel3.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                label4.Visible = false;
                numericUpDown2.Visible = false;
            }
            else
            {
                panel2.Visible = false;
                panel3.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                label4.Visible = false;
                numericUpDown2.Visible = false;
            }
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.smena = this_smena;
            f3.ShowDialog();
            int i = 0;
            foreach (archer_comp ar in f3.smena)
            {
                this_smena[i].summ = ar.summ;
                i++;

            }
            smena_ubdate();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)                     //не доделан
        {
            if (treeView1.SelectedNode.Name[0] == 'y')
            {
            }
            else
            {
                int sportsmen_count = 0;
                string getname = "";
                DateTime start = new DateTime();
                DateTime end = new DateTime(); 
                string sqlQuery = "SELECT * from competition where competition.id = '" + treeView1.SelectedNode.Name + "';";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    getname = reader["array_rounds_sets"].ToString();
                    start = Convert.ToDateTime(reader["start_date"]);
                    end = Convert.ToDateTime(reader["end_date"]);
                }
                string[] comp_sets = getname.Split(',');
                reader.Close();
                reader = null;
                foreach (string id in comp_sets)
                {
                    archer_comp archer = new archer_comp();
                    round_set set = new round_set();
                    sqlQuery = "select * from `set` where id = '" + id + "'";
                    adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                    command = new SQLiteCommand(sqlQuery, m_dbConn);
                    SQLiteDataReader reader1 = command.ExecuteReader();
                    while (reader1.Read())
                    {
                        sportsmen_count++;
                        set.id_aecher = reader1["id"].ToString();
                        set.array_id_rounds = reader1["array_id_rounds"].ToString().Split(',');
                        archer.stand = reader1["stand"].ToString() + reader1["latter"].ToString();
                        archer.category_id = reader1["id_category"].ToString();
                    }
                    archer.id = set.id_aecher;
                    reader1.Close();
                    reader = null;
                    foreach (string id1 in set.array_id_rounds)
                    {
                        round round = new round();
                        sqlQuery = "select * from `rounds` where id = '" + id1 + "' ";
                        adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            round.id = reader["id"].ToString();
                            round.result = reader["result"].ToString();
                            round.distantion = reader["distantion"].ToString();
                        }
                        reader.Close();
                        reader = null;
                        archer.rounds.Add(round);
                    }
                    sqlQuery = "select * from `users` where id = '" + archer.id + "' ";
                    adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                    command = new SQLiteCommand(sqlQuery, m_dbConn);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        archer.fio = reader["name"].ToString();
                        archer.age = Convert.ToDateTime(reader["age"]);
                        archer.region = reader["region"].ToString();
                        archer.rang = reader["rang"].ToString();
                        archer.sp_organization = reader["sp_organization"].ToString();
                        archer.vedomstvo = reader["vedomost"].ToString();
                    }
                    //archer_comp archer1 = archer;
                    dataGridView2.Rows.Add(
                        archer.plase,
                        archer.stand,
                        archer.fio,
                        archer.age,
                        archer.rang,
                        archer.region,
                        archer.vedomstvo,
                        archer.sp_organization,
                        archer.round,
                        archer.tens,
                        archer.x,
                        archer.summ);
                }
                label13.Text = (treeView1.SelectedNode.Text);
                label12.Text = (start.ToString("dd:MM:yyyy"));
                label19.Text = (sportsmen_count.ToString());
                label20.Text = (end.ToString("dd:MM:yyyy"));
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
/*  */
