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
using System.Security.Cryptography;
using System.Security.Policy;


namespace prosedures
{
    public static class func
    {


 


        static string md5 = null;
        public static string GetMD5(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }            
            return sBuilder.ToString();
        }

        public static bool VerifyMD5Hash(string input, string hash)
        {            
            string hashOfInput = GetMD5(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return 0 == comparer.Compare(hashOfInput, hash);
        }
        public static void category_check(string input, string hash)
        {
            int year = 2017;
            if (year != 2017)
            { 
                WindowsFormsApp4.Form1 form1 = new WindowsFormsApp4.Form1();
                string sqlQuery = "select id,age from users";
                SQLiteCommand m_sqlCmd = new SQLiteCommand();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, form1.m_dbConn);
                SQLiteCommand command = new SQLiteCommand(sqlQuery, form1.m_dbConn);
                SQLiteDataReader reader = command.ExecuteReader();
                DateTime thisDay = DateTime.Today;

                while (reader.Read())
                {
                    int age = thisDay.Year - Convert.ToDateTime(reader[1]).Year;
                    if (age <= 13)
                    {
                        m_sqlCmd.CommandText = "UPDATE users SET category = 1 WHERE id = '" + reader[0].ToString() + "' ;";
                        m_sqlCmd.ExecuteNonQuery();
                    }
                    else if (age >= 14 && age <= 17)
                    {
                        m_sqlCmd.CommandText = "UPDATE users SET category = 2 WHERE id = '" + reader[0].ToString() + "' ;";
                        m_sqlCmd.ExecuteNonQuery();
                    }
                    else if (age >= 18 && age <= 20)
                    {
                        m_sqlCmd.CommandText = "UPDATE users SET category = 3 WHERE id = '" + reader[0].ToString() + "' ;";
                        m_sqlCmd.ExecuteNonQuery();
                    }
                    else if (age >= 20)
                    {
                        m_sqlCmd.CommandText = "UPDATE users SET category = 4 WHERE id = '" + reader[0].ToString() + "' ;";
                        m_sqlCmd.ExecuteNonQuery();
                    }
                }
            }
        }


    }
}
