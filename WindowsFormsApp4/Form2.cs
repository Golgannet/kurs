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
using System.Data.Sql;
using System.IO;
using System.Data.SqlClient;

namespace WindowsFormsApp4
{
    public partial class Form2 : Form
    {
        public String dbFileName;
        public SQLiteConnection m_dbConn;
        public SQLiteCommand m_sqlCmd;
        public bool cost = false;
        string getname = "";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (prosedures.func.VerifyMD5Hash(textBox2.Text, getname))
            {
                
            }
            else
            {
                Application.Exit();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dTable = new DataTable();
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();
            dbFileName = "sample2.sqlite";            
            if (!File.Exists(dbFileName))
                SQLiteConnection.CreateFile(dbFileName);
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;
                          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {            
            string sqlQuery = "SELECT pasword from paswords where id IN ( SELECT id from users where login = '" + textBox1.Text + "' )";            
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
            DataTable dTable = new DataTable();
            SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConn);
            SQLiteDataReader pswd = command.ExecuteReader();

            while (pswd.Read())
            {
                getname = pswd[0].ToString();                
            }
            this.Close();            
            if (prosedures.func.VerifyMD5Hash(textBox2.Text, getname))
            {
               cost = true;
               this.Close();
            }
            else
            {
                MessageBox.Show("неверный логин или пароль");
            }

           // byte[] src = Convert.FromBase64String(prosedures.func.HashPassword("admin"));
            //textBox2.Text = (prosedures.func.GetMD5(textBox1.Text));
        }
    }
}
