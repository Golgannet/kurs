
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
            comboBox1.SelectedItem = comboBox1.Items[1];
            comboBox3.SelectedItem = comboBox3.Items[0];
            checkedComboBox3.ValueMember = "id";
            checkedComboBox3.DisplayMember = "name";
            //comboBox3.ValueMember = "id";
            //comboBox3.DisplayMember = "name";
            //checkedComboBox1.Items.Add(12);
            //checkedComboBox1.Items.Add(18);
            //checkedComboBox1.Items.Add(30);
            //checkedComboBox1.Items.Add(40);
            //checkedComboBox1.Items.Add(50);
            //checkedComboBox1.Items.Add(60);
            //checkedComboBox1.Items.Add(70);
            //checkedComboBox1.Items.Add(90);
            //checkedComboBox1.Items.Add("M1");
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
            item.f = comboBox1;
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
                //sqlQuery = "drop table if exists 'smena" + (i).ToString() + "';";
                //command = new SQLiteCommand(sqlQuery, m_dbConn);
                //command.ExecuteNonQuery();
                sqlQuery = " CREATE TABLE 'smena" + (i).ToString() + "' ( 'id' INTEGER PRIMARY KEY AUTOINCREMENT, 'id_perfomanse' INTEGER, 'sheild' INTEGER, 'letter' INTEGER )";
                command = new SQLiteCommand(sqlQuery, m_dbConn);
                command.ExecuteNonQuery();
                sqlQuery = "INSERT INTO   `smena_info` (title, table_name, distantion)VALUES ('" + a.a.Text + "','smena"+ (i).ToString() + "','"+ a.f.Text +"');";
                command = new SQLiteCommand(sqlQuery, m_dbConn);
                command.ExecuteNonQuery();
                sqlQuery = "SELECT id FROM smena_info WHERE id=last_insert_rowid();";
                command = new SQLiteCommand(sqlQuery, m_dbConn);
                string id_smena = command.ExecuteScalar().ToString();
                competition.smena_id += id_smena + ",";
                foreach (Object obj in l1.CheckedItems)
                {
                    table_names archer = (table_names)obj;                    
                    if ( a.f.Text != "M1")
                    {
                        string dist = (a.f.Text[0] + a.f.Text[1]).ToString();
                        sqlQuery = "INSERT INTO  `rounds` (distantion)VALUES ('" + dist + "');INSERT INTO  `rounds` (distantion)VALUES ('" + dist + "');";
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        command.ExecuteNonQuery();
                        sqlQuery = "SELECT id FROM rounds WHERE id=last_insert_rowid();";
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        int id_round = Convert.ToInt32(command.ExecuteScalar());
                        sqlQuery = "INSERT INTO  performance (id_user,id_rounds) values ('" + archer.id.ToString() + "','" + (id_round-1).ToString() +"," + id_round.ToString() + "');";
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        if (archer.pol)
                        {
                            sqlQuery = "INSERT INTO  `rounds` (distantion)VALUES ('90');INSERT INTO  `rounds` (distantion)VALUES ('70');INSERT INTO  `rounds` (distantion)VALUES ('50');INSERT INTO  `rounds` (distantion)VALUES ('30');";
                        }
                        else
                        {
                            sqlQuery = "INSERT INTO  `rounds` (distantion)VALUES ('70');INSERT INTO  `rounds` (distantion)VALUES ('60');INSERT INTO  `rounds` (distantion)VALUES ('50');INSERT INTO  `rounds` (distantion)VALUES ('30');";
                        }
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        command.ExecuteNonQuery();
                        sqlQuery = "SELECT id FROM rounds WHERE id=last_insert_rowid();";
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        int id_round = Convert.ToInt32(command.ExecuteScalar());
                        sqlQuery = "INSERT INTO  perfomanse (id_user,id_rounds) values ('" + archer.id.ToString() + "','" + (id_round - 3).ToString() + "','" + (id_round -2).ToString() + "','" + (id_round - 1).ToString() + "','" + id_round.ToString() + "');";
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        command.ExecuteNonQuery();
                    }
                    sqlQuery = "SELECT id FROM rounds WHERE id=last_insert_rowid();";
                    command = new SQLiteCommand(sqlQuery, m_dbConn);
                    string id_perfomanse = command.ExecuteScalar().ToString();                    
                    competition.set_rounds += id_perfomanse + ",";
                    sqlQuery = "INSERT INTO   'smena" + (i).ToString() + "' (id_perfomanse)VALUES ('" + id_perfomanse + "');";
                    command = new SQLiteCommand(sqlQuery, m_dbConn);
                    command.ExecuteNonQuery();
                }                
                i++;
            }
            competition.smena_id = competition.smena_id.Substring(0, competition.smena_id.Length - 1);
            competition.set_rounds = competition.set_rounds.Substring(0, competition.set_rounds.Length - 1);
            sqlQuery = "INSERT INTO  competition (name, id_performance, start_date, end_date, id_smena)VALUES ('" + competition.name + "','" + competition.set_rounds + "','" + competition.start_date.ToString("yyyy-MM-dd") + "','" + competition.end_date.ToString("yyyy-MM-dd") + "','" + competition.smena_id + "');";
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
                ComboBox f = comboBox1;
                TextBox temp1 = new TextBox();
                ComboBox temp2 = new ComboBox();
                ComboBox temp3 = new ComboBox();
                ComboBox temp4 = new ComboBox();
                temp1.Name = "temp1." + i.ToString();
                temp2.Name = "temp2." + i.ToString();
                temp3.Name = "temp3." + i.ToString();
                temp4.Name = "temp4." + i.ToString();
                temp1.Width = a.Width;
                temp4.Width = f.Width;
                foreach (var obj in b.Items)
                    temp2.Items.Add(obj);
                foreach (var obj in c.Items)
                    temp3.Items.Add(obj);
                foreach (var obj in f.Items)
                    temp4.Items.Add(obj);
                temp4.SelectedItem = comboBox1.Items[1];
                temp2.SelectedItem = comboBox3.Items[0];
                temp1.Location = new Point(a.Location.X, a.Location.Y + a.Height * i + 25 * i);
                temp2.Location = new Point(b.Location.X, b.Location.Y + b.Height * i + 24 * i);
                temp3.Location = new Point(c.Location.X, c.Location.Y + c.Height * i + 24 * i);
                temp4.Location = new Point(f.Location.X, f.Location.Y + f.Height * i + 24 * i);
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
                item.f = temp4;
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
          
        private void button6_Click_1(object sender, EventArgs e)                                                                               // работает- не трожь
        {
            dataGridView3.Rows.Clear();
            FileStream file1 = new FileStream("this_comp.txt", FileMode.Open);
            StreamReader reader2 = new StreamReader(file1);
            string competition_id = (reader2.ReadToEnd());
            reader2.Close();
            string sqlQuery = "select id_smena from competition where id = '" + competition_id + "' ";
            SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConn);
            SQLiteDataReader id_smena = command.ExecuteReader();
            List<string> smena_id_array = new List<string>();
            while (id_smena.Read())
            {
                string[] smena_id = id_smena[0].ToString().Split(',');
                foreach (string smena in smena_id)
                {
                    smena_id_array.Add(smena);
                }
            }
            checkedComboBox3.Items.Clear();
            foreach (string smena_id in smena_id_array)
            {
                sqlQuery = "select * from smena_info where id = '" + smena_id + "' ";
                command = new SQLiteCommand(sqlQuery, m_dbConn);
                SQLiteDataReader cat = command.ExecuteReader();
                while (cat.Read())
                {
                    checheckedComboBox_category categ = new checheckedComboBox_category();
                    categ.id = cat["id"].ToString();
                    categ.name = cat["title"].ToString();
                    checkedComboBox3.Items.Add(categ);
                }
                
            }
                foreach (string table_id in smena_id_array)
            {
                List<archer_comp> smena = new List<archer_comp>();
                sqlQuery = "SELECT table_name FROM smena_info WHERE id= '"+ table_id + "';";
                command = new SQLiteCommand(sqlQuery, m_dbConn);
                string name_table = command.ExecuteScalar().ToString();
                sqlQuery = "select sheild,plase,letter,name,age,rang,region,vedomost,sp_organization,id_rounds,id_user,id_perfomanse from (select * from (select * from (select name,age,rang,region,sp_organization, vedomost,pol, id  as iid from users where id in( SELECT id_user FROM performance WHERE id IN (SELECT id_perfomanse FROM '" + name_table + "'  ))) a inner join  ( SELECT * FROM performance WHERE id IN (SELECT id_perfomanse  FROM '" + name_table + "'  )) d on a.iid = d.id_user) c inner join '" + name_table + "' on c.id = '" + name_table + "'.id_perfomanse)";
                command = new SQLiteCommand(sqlQuery, m_dbConn);
                SQLiteDataReader archer_reader = command.ExecuteReader();
                archer_comp archer = new archer_comp();
                while (archer_reader.Read())
                {
                    archer.fio = archer_reader["name"].ToString();
                    archer.age = Convert.ToDateTime(archer_reader["age"]);
                    archer.region = archer_reader["region"].ToString();
                    archer.rang = archer_reader["rang"].ToString();
                    archer.sp_organization = archer_reader["sp_organization"].ToString();
                    archer.vedomstvo = archer_reader["vedomost"].ToString();
                    archer.rounds.Clear();
                    string id_r = archer_reader["id_rounds"].ToString();
                    foreach (string id in id_r.Split(','))
                    {
                        sqlQuery = "select * from rounds where id = '" + id + "'";
                        command = new SQLiteCommand(sqlQuery, m_dbConn);
                        SQLiteDataReader round_reader = command.ExecuteReader();
                        while (round_reader.Read())
                        {                           
                            round r = new round();
                            r.id = round_reader["id"].ToString();
                            r.result = round_reader["result"].ToString();
                            r.series = round_reader["array_series"].ToString();
                            r.distantion = round_reader["distantion"].ToString();
                            archer.rounds.Add(r);
                            archer.nine = Convert.ToByte(round_reader["nine"]);
                            archer.tens = Convert.ToByte(round_reader["ten"]);
                            archer.x = Convert.ToByte(round_reader["x"]);
                        }
                    }
                    dataGridView3.Rows.Add
                    (
                        archer.stand,
                        archer.letter,
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
                dataGridView3.Rows.Add();
                dataGridView3.Rows.Add();
            }            
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        public List<category_halder> categories2 = new List<category_halder>() { };
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            categories2.Clear();
            panel3.Controls.Clear();
            for (var i = 0; i < (numericUpDown2.Value); i++)
            {
                TextBox a = textBox4;
                CheckComboBoxTest.CheckedComboBox d = checkedComboBox3;
                d.DisplayMember = "name"; 
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
                temp2.DisplayMember = "name";
                temp2.ValueMember = "id";
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



        private void button_Click(object sender, EventArgs e)
        {
            

            (this.Controls["l1"] as DataGridView).Rows.Clear();

        }

        public List<DataGridView> smena_tables = new List<DataGridView>() { };
        public List<List<archer_comp>> comp_smena = new List<List<archer_comp>>() { };
        public List<archer_comp> this_comp = new List<archer_comp>() { };
        private void button7_Click(object sender, EventArgs e)
        {           
            Form4 f = new Form4();
            f.Owner = this;
            f.Show();            
            foreach (category_halder category in categories2)
            {
                int i = 0;                
                TabPage myTabPage = new TabPage(category.a.Text);
                DataGridView l1 = new DataGridView();
                List<archer_comp> smena = new List<archer_comp>();
                l1.Show();                
                tabControl3.TabPages.Add(myTabPage);
                f.tabControl1.TabPages.Add(myTabPage);
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
                string[] selected = category.d.Text.Split(',');
                int k = 0;
                List<string> list_id = new List<string>() { };
                for (int j = 0; j < selected.Count(); j++)
                {                    
                    foreach (object cat in category.d.Items)
                    {
                        checheckedComboBox_category categh = (checheckedComboBox_category)cat;
                        if (categh.name == selected[j] || " "+categh.name == selected[j])
                        {
                            list_id.Add(categh.id);                           
                        }
                    }                    
                }
                foreach (string categ_id in list_id)
                {
                    string sqlQuery = "SELECT table_name  FROM smena_info WHERE id= '" + categ_id + "';";
                    SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConn);
                    string name_table = command.ExecuteScalar().ToString();
                    sqlQuery = "select sheild,plase,letter,name,age,rang,region,vedomost,sp_organization,id_rounds,id_user,id_perfomanse from (select * from (select * from (select name,age,rang,region,sp_organization, vedomost,pol, id  as iid from users where id in( SELECT id_user FROM performance WHERE id IN (SELECT id_perfomanse FROM '" + name_table + "'  ))) a inner join  ( SELECT * FROM performance WHERE id IN (SELECT id_perfomanse  FROM '" + name_table + "'  )) d on a.iid = d.id_user) c inner join '" + name_table + "' on c.id = '" + name_table + "'.id_perfomanse)";
                    command = new SQLiteCommand(sqlQuery, m_dbConn);
                    SQLiteDataReader archer_reader = command.ExecuteReader();                    
                    while (archer_reader.Read())
                    {
                        archer_comp archer = new archer_comp();
                        archer.fio = archer_reader["name"].ToString();
                        archer.id = archer_reader["id_perfomanse"].ToString();
                        archer.age = Convert.ToDateTime(archer_reader["age"]);
                        archer.region = archer_reader["region"].ToString();
                        archer.rang = archer_reader["rang"].ToString();
                        archer.sp_organization = archer_reader["sp_organization"].ToString();
                        archer.vedomstvo = archer_reader["vedomost"].ToString();
                        string[] round_id = archer_reader["id_rounds"].ToString().Split(',');
                        foreach (string id in round_id)
                        {
                            sqlQuery = "select * from rounds where id = '" + id + "'";
                            command = new SQLiteCommand(sqlQuery, m_dbConn);
                            SQLiteDataReader round_reader = command.ExecuteReader();
                            while (round_reader.Read())
                            {
                                round r = new round();
                                r.id = round_reader["id"].ToString();
                                r.result = round_reader["result"].ToString();
                                r.series = round_reader["array_series"].ToString();
                                r.distantion = round_reader["distantion"].ToString();
                                archer.rounds.Add(r);
                                archer.nine = Convert.ToByte(round_reader["nine"]);
                                archer.tens = Convert.ToByte(round_reader["ten"]);
                                archer.x = Convert.ToByte(round_reader["x"]);
                            }
                        }
                        smena.Add(archer);
                        l1.Rows.Add
                        (
                            archer.stand,
                            archer.letter,
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
                    l1.Rows.Add();
                }
                comp_smena.Add(smena);
                smena_tables.Add(l1);
                i++;
            }
            f.comp_smena = comp_smena;
        }

        private static Point GetLocation(Button added_button2)
        {
            return added_button2.Location;
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }
        public void smena_ubdate()
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
    }//эта очень важная штука
}
/*checheckedComboBox_category categh = (checheckedComboBox_category)category.d.Items[1];
 *
 * sqlQuery = "SELECT id FROM categories WHERE id=last_insert_rowid();";
                command = new SQLiteCommand(sqlQuery, m_dbConn);
                int id_category = Convert.ToInt32(command.ExecuteScalar());
 * 
 * 
 * 
 * select sheild,plase,letter,name,age,rang,vedomost,sp_organization,id_rounds,id_user,id_perfomanse from (select * from (select * from (select name,age,rang,region,sp_organization, vedomost,pol, id  as iid from users where id in( SELECT id_user FROM performance WHERE id IN (SELECT id_perfomanse FROM smena1  ))) a
inner join  ( SELECT * FROM performance WHERE id IN (SELECT id_perfomanse  FROM smena1  )) d on a.iid = d.id_user) c inner join smena1 on c.id = smena1.id_perfomanse)    */
