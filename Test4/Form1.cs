using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void DBShow()
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "Select * from db";
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    textBox6.Text += mySqlDataReader["EntityName"].ToString().ToString() + "\r\n";
                    textBox7.Text += mySqlDataReader["FieldName"].ToString().ToString() + "\r\n";
                    textBox8.Text += mySqlDataReader["Type"].ToString().ToString() + "\r\n";
                    textBox9.Text += mySqlDataReader["FieldRequirement"].ToString().ToString() + "\r\n";
                    textBox10.Text += mySqlDataReader["Description"].ToString().ToString() + "\r\n";
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBShow();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO db(EntityName, FieldName, Type, FieldRequirement, Description) VALUE (?EntityName, ?FieldName, ?Type, ?FieldRequirement, ?Description)";
                command.Parameters.Add("?EntityName", MySqlDbType.VarChar).Value = textBox1.Text;
                command.Parameters.Add("?FieldName", MySqlDbType.VarChar).Value = textBox2.Text;
                command.Parameters.Add("?Type", MySqlDbType.VarChar).Value = textBox3.Text;
                command.Parameters.Add("?FieldRequirement", MySqlDbType.VarChar).Value = textBox4.Text;
                command.Parameters.Add("?Description", MySqlDbType.VarChar).Value = textBox5.Text;
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                connection.Close();
            }
            DBShow();
        }
    }
}
