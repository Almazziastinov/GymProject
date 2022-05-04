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

namespace GymProject
{
    public partial class MainForm : Form
    {
        private SQLiteConnection UserDb = new SQLiteConnection("Data Source=UsersDb.db; Version=3");
        public MainForm()
        {
            InitializeComponent();
            label2.Text = "";  
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TrenersToCB();
            LoockerToCB();
            //Lockers();
        }

        public void TrenersToCB()
        {
            UserDb = new SQLiteConnection("Data Source=UsersDb.db; Version=3");
            UserDb.Open();
            string query = $"select ID, Name, Subjects from Treners";
            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                trenersComboBox1.Items.Add(reader["ID"] +" "+ reader["Name"] +" "+ reader["Subjects"]);
                trenersComboBox2.Items.Add(reader["ID"] + " " + reader["Name"] +" "+ reader["Subjects"]);
            }
            UserDb.Close();
        }

        

        public void LoockerToCB()
        {
            
            UserDb.Open();
            string query = $"select * from Hals";
            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
            SQLiteDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                if (reader["MaleOpenLoockeers"].ToString().Equals("") 
                    & reader["MaleLockkedLoockers"].ToString().Equals("") 
                    & reader["FeMaleLockkedLoockers"].ToString().Equals(""))
                {
                    feMaleOpencomboBox2.Items.Add(reader["FeMaleOpenLoockers"]);
                }
                else if (reader["FeMaleOpenLoockers"].ToString().Equals("")
                    & reader["MaleLockkedLoockers"].ToString().Equals("")
                    & reader["FeMaleLockkedLoockers"].ToString().Equals(""))
                {
                    MaleOpencomboBox1.Items.Add(reader["MaleOpenLoockeers"]);
                }
                else if (reader["FeMaleOpenLoockers"].ToString().Equals("")
                    & reader["MaleLockkedLoockers"].ToString().Equals("")
                    & reader["MaleOpenLoockeers"].ToString().Equals(""))
                {
                    FeMaleLockedcomboBox4.Items.Add(reader["FeMaleLockkedLoockers"]);
                }
                else if (reader["FeMaleOpenLoockers"].ToString().Equals("")
                    & reader["FeMaleLockkedLoockers"].ToString().Equals("")
                    & reader["MaleOpenLoockeers"].ToString().Equals(""))
                {
                    MaleLockedcomboBox3.Items.Add(reader["MaleLockkedLoockers"]);
                }
            }
            UserDb.Close();
        }
        private void MaleOpencomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDb.Open();
            string query = "update Hals set (MaleLockkedLoockers, MaleOpenLoockeers) = (@Item2, @Item) " +
                "where MaleLockkedLoockers like (@Item) ";
            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
            cmd.Parameters.Add("@Item", System.Data.DbType.String).Value = MaleLockedcomboBox3.Text;
            cmd.Parameters.Add("@Item2", System.Data.DbType.String).Value = null;
            cmd.ExecuteNonQuery();
            MaleLockedcomboBox3.Items.Clear();


            
            MaleLockedcomboBox3.Text = MaleOpencomboBox1.SelectedItem.ToString();
            MaleLockedcomboBox3.Items.Add(MaleOpencomboBox1.SelectedItem);

            string query2 = "update Hals set (MaleLockkedLoockers, MaleOpenLoockeers) = (@Item, @Item2) " +
                "where MaleOpenLoockeers like (@Item) ";
            SQLiteCommand cmd2 = new SQLiteCommand(query2, UserDb);
            cmd2.Parameters.Add("@Item", System.Data.DbType.String).Value = MaleLockedcomboBox3.Text;
            cmd2.Parameters.Add("@Item2", System.Data.DbType.String).Value = null;
            cmd2.ExecuteNonQuery();

            UserDb.Close();
        }
        private void feMaleOpencomboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDb.Open();
            string query = "update Hals set (FeMaleLockkedLoockers, FeMaleOpenLoockers) = (@Item2, @Item) " +
                "where FeMaleLockkedLoockers like (@Item) ";
            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
            cmd.Parameters.Add("@Item", System.Data.DbType.String).Value = FeMaleLockedcomboBox4.Text;
            cmd.Parameters.Add("@Item2", System.Data.DbType.String).Value = null;
            cmd.ExecuteNonQuery();

            FeMaleLockedcomboBox4.Items.Clear();
            FeMaleLockedcomboBox4.Text = feMaleOpencomboBox2.SelectedItem.ToString();
            FeMaleLockedcomboBox4.Items.Add(feMaleOpencomboBox2.SelectedItem);

            string query2 = "update Hals set (FeMaleLockkedLoockers, FeMaleOpenLoockers) = (@Item, @Item2) " +
                "where FeMaleOpenLoockers like (@Item) ";
            SQLiteCommand cmd2 = new SQLiteCommand(query2, UserDb);
            cmd2.Parameters.Add("@Item", System.Data.DbType.String).Value = FeMaleLockedcomboBox4.Text;
            cmd2.Parameters.Add("@Item2", System.Data.DbType.String).Value = null;
            cmd2.ExecuteNonQuery();

            UserDb.Close();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage3"];
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage4"];
        }
        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage5"];
        }
        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage6"];
        }
        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
        }

        

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Column == 1 && e.Row == 0)
                e.Graphics.DrawRectangle(new Pen(Color.DimGray), e.CellBounds);
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            logintextBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserDb = new SQLiteConnection("Data Source=UsersDb.db; Version=3");
            UserDb.Open();
            string query = "select * from Treners where Login like '%' || @login || '%' " +
                "and Pasword like '%' || @password || '%' ";
            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
            cmd.Parameters.Add("@login", System.Data.DbType.String).Value = logintextBox1.Text;
            cmd.Parameters.Add("@password", System.Data.DbType.String).Value = paswordtextBox2.Text;
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Gender gender;
                Treners trener;
                List<Clients> clients = new List<Clients>();

                while (reader.Read())
                {
                    if (reader["Gender"].Equals("Male"))
                    {
                        gender = Gender.male;
                        trener = new Treners(Convert.ToInt32(reader["ID"]), gender, reader["Login"].ToString(), 
                            reader["Pasword"].ToString(), reader["Name"].ToString(), reader["Subjects"].ToString(), clients);
                    }
                    else
                    {
                        gender = Gender.female;

                    } 
                }
            }
            UserDb.Close();
        }



        //public void Lockers()
        //{
        //    UserDb = new SQLiteConnection("Data Source=UsersDb.db; Version=3");
        //    UserDb.Open();


        //    int t = 61;
        //    for (int i = 0; i < 20; i++)
        //    {
        //        if (t % 2 != 0)
        //        {
        //            string query = "Insert into Hals (Name, MaleOpenLoockeers, FeMaleOpenLoockers) Values ('Зал единоборств', '0', @login)";
        //            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
        //            cmd.Parameters.Add("@login", System.Data.DbType.String).Value = t.ToString();
        //            cmd.ExecuteNonQuery();
        //        }
        //        else
        //        {
        //            string query = "Insert into Hals (Name, MaleOpenLoockeers, FeMaleOpenLoockers) Values ('Зал единоборств', @login, '0')";
        //            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
        //            cmd.Parameters.Add("@login", System.Data.DbType.String).Value = t.ToString();
        //            cmd.ExecuteNonQuery();
        //        }
        //        t++;
        //    }

        //    UserDb.Close();

        //}
    }
}
