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
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace GymProject
{
    public partial class MainForm : Form
    {
        public string ggg1 = "Премиум1_";
        public string ggg2 = "Премиум1м_";
        public string ggg3 = "Премиум3м_";
        public string ggg4 = "Премиум6м_";

        private Application application;
        private Workbook workBook;
        private Worksheet worksheet;


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
            TextLable();
            BdtoView();
            DateCheck();
            //Lockers();

            
        }

        public void TrenersToCB()
        {
            
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
            MaleOpencomboBox1.Items.Clear();
            MaleLockedcomboBox3.Items.Clear();
            feMaleOpencomboBox2.Items.Clear();
            FeMaleLockedcomboBox4.Items.Clear();
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
        private void paswordtextBox2_Click(object sender, EventArgs e)
        {
            paswordtextBox2.Text = "";
            paswordtextBox2.PasswordChar = '*';
        }
        

        private void button6_Click(object sender, EventArgs e)
        {
            
            UserDb.Open();
            string query = "select * from Treners where Login like @login " +
                "and Pasword like @password";
            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
            cmd.Parameters.Add("@login", System.Data.DbType.String).Value = logintextBox1.Text;
            cmd.Parameters.Add("@password", System.Data.DbType.String).Value = paswordtextBox2.Text;
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                pictureBox4.Visible = false;
                
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
                    label2.Text = "Добро пожаловать господин" + reader["Name"];
                }
            }
            UserDb.Close();
        }

        public void ApendText(RichTextBox richTextBox, string text, System.Drawing.Font font)
        {
            richTextBox.Select(richTextBox.TextLength, 0);

            richTextBox.SelectionFont = font;
            richTextBox.AppendText(text);
            richTextBox.SelectionFont = richTextBox.Font;
        }

        public void TextLable()
        {
            UserDb.Open();
            string query = "select * from Hals ";
            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int i1 = 0;
            int i2 = 0;
            int i3 = 0;
            int i4 = 0;
            while (reader.Read())
            {
                if (reader["MaleLockkedLoockers"].ToString() != ""
                    || reader["FeMaleLockkedLoockers"].ToString() != "")
                {
                    if (reader["Name"].ToString().Equals("Аэробика"))
                    {
                        i4++;
                    }
                    else if (reader["Name"].ToString().Equals("Бассейн"))
                    {
                        i1++;
                    }
                    else if (reader["Name"].ToString().Equals("Тренажорный зал"))
                    {
                        i2++;
                    }
                    else if (reader["Name"].ToString().Equals("Зал единоборств"))
                    {
                        i3++;
                    }
                }
                
                
            }
            
            string basPip = i1.ToString();
            string gymPip = i2.ToString();
            string fightPip = i3.ToString();
            string aeroPip = i4.ToString();
            

            System.Drawing.Font font = new System.Drawing.Font(basikRITB.Font, FontStyle.Bold);


            ApendText(basikRITB, "Бассейн ", font);
            basikRITB.AppendText("длиной 25 метров с 7 дорожками, оборудован системой очистки воды \n" + basPip + " человек");
            ApendText(gymRITB, "Тренажёрный зал ", font);
            gymRITB.AppendText("с новым и надёжным оборудованием для силовых и кардио тренировок \n" + gymPip + " человек");
            ApendText(fightRITB, "Зал единоборств ", font);
            fightRITB.AppendText("оборудован рингом, татами для борьбы, настенные боксёрские подушки, подвесные груши, " +
                "которые позволяют отрабатывать любые виды ударов, развивать манёвренность \n" + fightPip + " человек");
            ApendText(AeroRITB, "Зал аэробики ", font);
            AeroRITB.AppendText("оборудован хореографическим станком и ростовыми зеркалами, оснащён спортивным " +
                "оборудованием (степ-платформы, скакалки, гантели и т.д.). \n" + aeroPip + " человек");

            UserDb.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UserDb.Open();
            string query1 = "Select * from Clients where Name like (@Name)";
            SQLiteCommand cmd1 = new SQLiteCommand(query1, UserDb);
            cmd1.Parameters.Add("@Name", DbType.String).Value = $"{textBox1.Text} {textBox2.Text} {textBox3.Text}";
            SQLiteDataReader reader = cmd1.ExecuteReader();
            if (reader.HasRows)
            {
                MessageBox.Show("Извините но такой клиент уже существует", "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

                
            }
            else
            {
                if (textBox1.Text.ToString().Equals("") || textBox2.Text.ToString().Equals("") ||
                               textBox3.Text.ToString().Equals("") || TarifCB.Text.ToString().Equals("") ||
                               (MaleRB.Checked == false & FeMaleRB.Checked == false) || textBox6.Text.ToString().Equals(""))
                {
                    MessageBox.Show("Заполните все строки!", "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

                }
                else
                {


                    string gender = "";
                    if (MaleRB.Checked)
                    {
                        gender = "Мужской";
                    }
                    else
                    {
                        gender = "Женский";
                    }
                    string query = "insert into Clients (Gender, Name, Age,Tarif, Loocker, Treners, Date, TruDate) values (@Gender, @Name, @Age, @Tarif, @Loocker, @Treners, @Date, @TruDate)";
                    SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
                    cmd.Parameters.Add("@Gender", DbType.String).Value = gender;
                    cmd.Parameters.Add("@Name", DbType.String).Value = $"{textBox1.Text} {textBox2.Text} {textBox3.Text}";
                    cmd.Parameters.Add("@Age", DbType.Int32).Value = Convert.ToInt32(textBox6.Text);
                    cmd.Parameters.Add("@Tarif", DbType.String).Value = TarifCB.Text;
                    if (gender.Equals("Мужской"))
                    {
                        if (MaleLockedcomboBox3.Text.Equals(""))
                        {
                            MessageBox.Show("Пожалуйста выберите шкафчик!", "Сообщение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                            return;
                        }
                        cmd.Parameters.Add("@Loocker", DbType.String).Value = MaleLockedcomboBox3.Text;
                    }
                    else
                    {
                        if (FeMaleLockedcomboBox4.Text.Equals(""))
                        {
                            MessageBox.Show("Пожалуйста выберите шкафчик!", "Сообщение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                            return;
                        }
                        cmd.Parameters.Add("@Loocker", DbType.String).Value = FeMaleLockedcomboBox4.Text;
                    }
                    if (TarifCB.Text.Equals($"{ggg1}{trenersComboBox2.Text}") || TarifCB.Text.Equals($"{ggg2}{trenersComboBox2.Text}") ||
                        TarifCB.Text.Equals($"{ggg3}{trenersComboBox2.Text}") || TarifCB.Text.Equals($"{ggg4}{trenersComboBox2.Text}"))
                    {
                        cmd.Parameters.Add("@Treners", DbType.String).Value = trenersComboBox2.Text;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Treners", DbType.String).Value = trenersComboBox1.Text;
                    }


                    // Добавление времени в зависимости от тарифа
                    DateTime time = DateTime.Now;
                    cmd.Parameters.Add("@TruDate", DbType.String).Value = time.ToShortDateString();
                    if (TarifCB.Text.Equals($"{ggg1}{trenersComboBox2.Text}") || TarifCB.Text.Equals($"Про1_{trenersComboBox1.Text}") ||
                        TarifCB.Text.Equals($"Стандарт1"))
                    {
                        
                        time.AddDays(1);
                        cmd.Parameters.Add("@Date", DbType.String).Value = time.ToShortDateString();
                        
                    }
                    else if (TarifCB.Text.Equals($"{ggg2}{trenersComboBox2.Text}") || TarifCB.Text.Equals($"Про1м_{trenersComboBox1.Text}") ||
                        TarifCB.Text.Equals($"Стандарт1м"))
                    {
                        time.AddMonths(1);
                        cmd.Parameters.Add("@Date", DbType.String).Value = time.ToShortDateString();
                        
                    }
                    else if (TarifCB.Text.Equals($"{ggg3}{trenersComboBox2.Text}") || TarifCB.Text.Equals($"Про3м_{trenersComboBox1.Text}") ||
                        TarifCB.Text.Equals($"Стандарт3м"))
                    {
                        time.AddMonths(3);
                        cmd.Parameters.Add("@Date", DbType.String).Value = time.ToShortDateString();
                        
                    }
                    else if (TarifCB.Text.Equals($"{ggg4}{trenersComboBox2.Text}") || TarifCB.Text.Equals($"Про6м_{trenersComboBox1.Text}") ||
                        TarifCB.Text.Equals($"Стандарт6м"))
                    {
                        time.AddMonths(6);
                        cmd.Parameters.Add("@Date", DbType.String).Value = time.ToShortDateString();
                        
                    }

                    cmd.ExecuteNonQuery();


                }
            }
            UserDb.Close();
            BdtoView();
            LoockerToCB();
            DateCheck();
        }


        private void trenersComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TarifCB.Items.Add($"Про1_{trenersComboBox1.Text}");
            TarifCB.Items.Add($"Про1м_{trenersComboBox1.Text}");
            TarifCB.Items.Add($"Про3м_{trenersComboBox1.Text}");
            TarifCB.Items.Add($"Про6м_{trenersComboBox1.Text}");
        }

        private void trenersComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TarifCB.Items.Add($"{ggg1}{trenersComboBox2.Text}");
            TarifCB.Items.Add($"{ggg2}{trenersComboBox2.Text}");
            TarifCB.Items.Add($"{ggg3}{trenersComboBox2.Text}");
            TarifCB.Items.Add($"{ggg4}{trenersComboBox2.Text}");
        }

        private void BdtoView()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black; ;
            dataGridView1.EnableHeadersVisualStyles = false;
            UserDb.Open();

            string query = "select ID, Name, Age, Gender, Tarif, TruDate from Clients";
            
            

            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
            System.Data.DataTable dt = new System.Data.DataTable();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            UserDb.Close();
        }
        private void DateCheck()
        {
            //UserDb.Open();

            //string query = "select * from Clients";
            //SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
            //SQLiteDataReader reader = cmd.ExecuteReader();
            //DateTime now = DateTime.Now;

            //int temp = 0;
            //int i = 1;
            //DateTime date;
            //while (reader.Read())
            //{

            //    dataGridView1.Rows[i].Cells["ID"].Value = reader["ID"].ToString();
            //    dataGridView1.Rows[i].Cells[1].Value = reader["Name"].ToString();
            //    dataGridView1.Rows[i].Cells[2].Value = reader["Age"].ToString();
            //    dataGridView1.Rows[i].Cells[3].Value = reader["Gender"].ToString();
            //    dataGridView1.Rows[i].Cells[4].Value = reader["Tarif"].ToString();

            //    date = Convert.ToDateTime(reader["Date"]);
            //    temp = Convert.ToInt32(now);

            //    if (Convert.ToInt32(reader["Date"]) > temp)
            //    {
            //        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ChekingColumn", HeaderText = "✔" });
            //        dataGridView1.Rows[i].Cells["ChekingColumn"].Value = reader["Date"].ToString();
            //        i++;
            //    }
            //    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Data", HeaderText = "Data" });
            //    dataGridView1.Rows[i].Cells["Data"].Value = reader["TruDate"].ToString();
            //}


            //UserDb.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UserDb.Open();
            string query = "Select * from Clients";
            SQLiteCommand cmd = new SQLiteCommand(query, UserDb);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int i = 1;
            
            while (reader.Read())
            {
                application = new Application
                {
                    DisplayAlerts = false
                };
                const string template = "template.xlsx";


                workBook = application.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, template));


                worksheet = workBook.ActiveSheet as Worksheet;


                worksheet.Cells[i, 1] = DateTime.Now;
                worksheet.Cells[i, 2] = reader["ID"].ToString();
                worksheet.Cells[i, 3] = reader["Gender"].ToString();
                worksheet.Cells[i, 4] = reader["Name"].ToString();
                worksheet.Cells[i, 5] = reader["Age"].ToString();
                worksheet.Cells[i, 6] = reader["Tarif"].ToString();
                worksheet.Cells[i, 7] = reader["Loocker"].ToString();
                worksheet.Cells[i, 8] = reader["Treners"].ToString();
                worksheet.Cells[i, 9] = reader["TruDate"].ToString();

                i++;
            }
            

            application.Visible = true;
            TopMost = true;

            button9.Enabled = true;

            UserDb.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string savedFileName = "book1.xlsm";
            workBook.SaveAs(Path.Combine(Environment.CurrentDirectory, savedFileName));
            CloseExcel();
        }
        private void CloseExcel()
        {
            if (application != null)
            {
                int excelProcessId = -1;
                GetWindowThreadProcessId(application.Hwnd, ref excelProcessId);

                Marshal.ReleaseComObject(worksheet);
                workBook.Close();
                Marshal.ReleaseComObject(workBook);
                application.Quit();
                Marshal.ReleaseComObject(application);

                application = null;
                // Прибиваем висящий процесс
                try
                {
                    Process process = Process.GetProcessById(excelProcessId);
                    process.Kill();
                }
                finally { }
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);

        




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
