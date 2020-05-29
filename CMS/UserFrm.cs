using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using CMS.Classes;
using System.Drawing;

namespace CMS
{
    public partial class UserFrm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\App\CMS\PatientHistory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd, cmd2;
        SqlDataAdapter adapt;

        Responsive ResponsiveObj;

        public int ID = 0;
      
        //Passing parameter from form 1
        public UserFrm(string tx)
        {
            InitializeComponent();
            label12.Text = tx;
            DisplayData();
            AddRtb();

            ResponsiveObj = new Responsive(Screen.PrimaryScreen.Bounds);
            ResponsiveObj.SetMultiplicationFactor();



        }

        private void UserFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'patientHistoryDataSet.Bill_tbl' table. You can move, or remove it, as needed.
            this.bill_tblTableAdapter.Fill(this.patientHistoryDataSet.Bill_tbl);
            // TODO: This line of code loads data into the 'patientHistoryDataSet.Patient' table. You can move, or remove it, as needed.
            this.patientTableAdapter.Fill(this.patientHistoryDataSet.Patient);
          
            //Responsive
            Width = ResponsiveObj.GetMetrics(Width, "Width");    // Form width and height set up.
            Height = ResponsiveObj.GetMetrics(Height, "Height");
            Left = Screen.GetBounds(this).Width / 2 - Width / 2;  // Form centering.
            Top = Screen.GetBounds(this).Height / 2 - Height / 2 - 30;  // 30 is a calibration factor.

            foreach (Control Ctl in this.Controls)
            {
              //  Ctl.Font = new Font(FontFamily.GenericMonospace, ResponsiveObj.GetMetrics((int)Ctl.Font.Size),FontStyle.Regular);
               
                Ctl.Width = ResponsiveObj.GetMetrics(Ctl.Width, "Width");
                Ctl.Height = ResponsiveObj.GetMetrics(Ctl.Height, "Height");
                Ctl.Top = ResponsiveObj.GetMetrics(Ctl.Top, "Top");
                Ctl.Left = ResponsiveObj.GetMetrics(Ctl.Left, "Left");
            }

        }

    
        //Adding Word in RichTextBox
        private void AddRtb()
        {
            string s = DateTime.Now.ToString();
            richTextBox2.Text += "Fever :     F\r\n" + "Blood Pressure :    /   mmHg\r\n" + "Weight : \r\n" + "Pulse Rate :    /min\r\n";
            richTextBox2.Text += "\r\n***" + s + "***\r\n";
            richTextBox3.Text += "Chief Complaint : \r\n" + "Underlying Diseases : \r\n" + "Remarks : \r\n";
            richTextBox3.Text += "\r\n****" + s + "****\r\n";

            richTextBox4.Text += "Diagnosis: \r\n"+"Treatment Plan : \r\n";
            richTextBox4.Text += "\r\n****" + s + "****\r\n";
            richTextBox5.Text += "Findings : \r\n";
            //   richTextBox5.Text += "\r\n**********" + textBox4.Text.ToString() + "**********\r\n";
            richTextBox6.Text += "Findings : \r\n";
               richTextBox6.Text += "\r\n****" + s + "****\r\n";
            //   richTextBox7.Text += "Findings : \r\n";
            //    richTextBox7.Text += "\r\n**********" + textBox4.Text.ToString() + "**********\r\n";
            richTextBox8.Text += "Findings : \r\n";
               richTextBox8.Text += "\r\n****" + s + "****\r\n";

        }
      
        //Adding Word in Rich Text Box
        private void AddRtb2()
        {
            string s = DateTime.Now.ToString();
            richTextBox2.Text += "Fever :     F\r\n" + "Blood Pressure :    /   mmHg\r\n" + "Weight : \r\n" + "Pulse Rate :    /min\r\n";
            richTextBox2.Text += "\r\n***" + s + "***\r\n";
            richTextBox3.Text += "Chief Complaint : \r\n" + "Underlying Diseases : \r\n" + "Remarks : \r\n";
            richTextBox3.Text += "\r\n****" + s + "****\r\n";
            richTextBox4.Text += "Diagnosis: \r\n" + "Treatment Plan : \r\n";
            richTextBox4.Text += "\r\n****" + s + "****\r\n";

            richTextBox6.Text += "Findings : \r\n";
            richTextBox6.Text += "\r\n****" + s + "****\r\n";
            richTextBox8.Text += "Findings : \r\n";
            richTextBox8.Text += "\r\n****" + s + "****\r\n";
        }

        //View in table
        private void DisplayData()
        {
            string str = label12.Text;

            con.Open();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            if (str == "")
            {
                cmd = new SqlCommand("select * from [Patient]", con);
                cmd2 = new SqlCommand("Select * from [Bill_tbl]", con);
                
                button2.Visible = false;
                button3.Visible = false;
                button3.Visible = false;
                button7.Visible = false;
                button9.Visible = false;
                button12.Visible = true;
            }
            else
            {
                cmd = new SqlCommand("select * from [Patient]", con);
               cmd2 = new SqlCommand("Select * from [Bill_tbl] where (Select Id from [User] where UsrName='" + str + "')=Doctor_id", con);
            }

            SqlDataAdapter adpt2 = new SqlDataAdapter(cmd2);
            adpt2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            adapt = new SqlDataAdapter(cmd);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        //Clear Data  
        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "Male";
            comboBox2.Text = "Status";
            comboBox3.Text = "Type";
            textBox5.Text = "";
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";
            richTextBox5.Text = "";
            richTextBox6.Text = "";
            richTextBox7.Text = "";
            richTextBox8.Text = "";
            richTextBox9.Text = "";
            AddRtb();

            ID = 0;
        }

        //MouseClick on Cellhead
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()!="")
            {
                ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();   
                richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                richTextBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                richTextBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                richTextBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                richTextBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                richTextBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                richTextBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                richTextBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                AddRtb2();
            }
            else { MessageBox.Show("Please Select Correct RowHeader!"); }
        }

        private void CellClickEvt(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() != "" )
            {
                ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            //     dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            richTextBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            richTextBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            richTextBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
            richTextBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            richTextBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            richTextBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            richTextBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                AddRtb2();
            }
            else { MessageBox.Show("Please Click on Correct Cell of the Patient table!"); }
        }

        //Add New User Button
        private void button3_Click_1(object sender, EventArgs e)
        {
            int name = textBox1.Text.Length;
            int phone = textBox2.Text.Length;
            int age = textBox3.Text.Length;
            int address = richTextBox1.Text.Length;

            if (dataGridView1.Rows.Count > 100)
            {
                MessageBox.Show("Data can't be added due to exceed the limit. Please Contact to Company!");
            }
            else
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    if (name < 25 && name > 0)
                    {
                        if (phone < 15 && phone > 4)
                        {
                            if (age < 4 && age > 0)
                            {
                                if (address < 250 && address >0)
                                {
                                    string str = label12.Text;
                                    cmd = new SqlCommand(@"INSERT INTO dbo.[Patient](Name,Phone,Age,Gender,M_Status,BloodType,DateVisit,Address,General_History,
                                        Present_Illness,Treatment,
                                        Other_History,Physical_Examination,Diagnosis,LabFinding,Doctor_id)
                                        SELECT @n,@p,@a,@s,@ms,@bt,@dv,@addr,
                                        @gh,@pi,@t,@oh,@pe,@d,@lf,(select Id from dbo.[User] where UsrName='" + str + "') WHERE NOT EXISTS (SELECT 1 FROM [Patient] d WHERE d.Name=@n AND d.Phone=@p)", con);

                                    con.Open();
                                    cmd.Parameters.AddWithValue("@n", textBox1.Text);
                                    cmd.Parameters.AddWithValue("@p", textBox2.Text);
                                    cmd.Parameters.AddWithValue("@a", textBox3.Text);
                                    cmd.Parameters.AddWithValue("@s", comboBox1.Text);
                                    cmd.Parameters.AddWithValue("@ms", comboBox2.Text);
                                    cmd.Parameters.AddWithValue("@bt", comboBox3.Text);
                                    cmd.Parameters.AddWithValue("@dv", dateTimePicker1.Text);
                                    cmd.Parameters.AddWithValue("@addr", richTextBox1.Text);
                                    cmd.Parameters.AddWithValue("@gh", richTextBox2.Text);
                                    cmd.Parameters.AddWithValue("@pi", richTextBox3.Text);
                                    cmd.Parameters.AddWithValue("@t", richTextBox4.Text);
                                    cmd.Parameters.AddWithValue("@oh", richTextBox5.Text);
                                    cmd.Parameters.AddWithValue("@pe", richTextBox6.Text);
                                    cmd.Parameters.AddWithValue("@d", richTextBox7.Text);
                                    cmd.Parameters.AddWithValue("@lf", richTextBox8.Text);
                                    cmd.ExecuteNonQuery();
                                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    adapt.Fill(ds);
                                    con.Close();
                                    MessageBox.Show("Patient Record Inserted Successfully");
                                    DisplayData();
                                    ClearData();
                                }
                                else
                                {
                                    MessageBox.Show("TextBox 'Address' is more or less than limit!");
                                }

                            }//end of IF for Age
                            else
                            {
                                MessageBox.Show("TextBox 'Age' is more or less than limit!");
                            }

                        }//end of IF for Phone
                        else
                        {
                            MessageBox.Show("TextBox 'Phone' is more or less than limit!");
                        }
                    }//if end of Name Validation
                    else
                    {
                        MessageBox.Show("TextBox 'Name' is more or less than limit!");
                    }

                }//end of text box validation

                else
                {
                    MessageBox.Show("Name and Phone are required to add new user!");
                }

            }
        }

        //Update
        private void button2_Click(object sender, EventArgs e)
        {
            int name = textBox1.Text.Length;
            int phone = textBox2.Text.Length;
            int age = textBox3.Text.Length;
            int address = richTextBox1.Text.Length;

            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (name < 25 && name > 0)
                {
                    if (phone < 15 && phone > 4)
                    {
                        if (age < 4 && age > 0)
                        {
                            if (address < 250 && address >=0)
                            {
                                cmd = new SqlCommand("update [Patient] set Name=@n,Phone=@p,Age=@a,Gender=@s,M_Status=@ms,BloodType=@bt,DateVisit=@dv,Address=@addr,General_History=@gh,Present_Illness=@pi,Treatment=@t,Other_History=@oh,Physical_Examination=@pe,Diagnosis=@d,LabFinding=@lf where Id=@id", con);
                                con.Open();
                                cmd.Parameters.AddWithValue("@id", ID);
                                cmd.Parameters.AddWithValue("@n", textBox1.Text);
                                cmd.Parameters.AddWithValue("@p", textBox2.Text);
                                cmd.Parameters.AddWithValue("@a", textBox3.Text);
                                cmd.Parameters.AddWithValue("@s", comboBox1.Text);
                                cmd.Parameters.AddWithValue("@ms", comboBox2.Text);
                                cmd.Parameters.AddWithValue("@bt", comboBox3.Text);
                                cmd.Parameters.AddWithValue("@dv", dateTimePicker1.Text);
                                cmd.Parameters.AddWithValue("@addr", richTextBox1.Text);
                                cmd.Parameters.AddWithValue("@gh", richTextBox2.Text);
                                cmd.Parameters.AddWithValue("@pi", richTextBox3.Text);
                                cmd.Parameters.AddWithValue("@t", richTextBox4.Text);
                                cmd.Parameters.AddWithValue("@oh", richTextBox5.Text);
                                cmd.Parameters.AddWithValue("@pe", richTextBox6.Text);
                                cmd.Parameters.AddWithValue("@d", richTextBox7.Text);
                                cmd.Parameters.AddWithValue("@lf", richTextBox8.Text);

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Patient Record Updated Successfully");

                                con.Close();
                                DisplayData();
                                ClearData();
                            }
                            else
                            {
                                MessageBox.Show("TextBox 'Address' is more or less than limit!");
                            }

                        }//end of IF for Age
                        else
                        {
                            MessageBox.Show("TextBox 'Age' is more or less than limit!");
                        }

                    }//end of IF for Phone
                    else
                    {
                        MessageBox.Show("TextBox 'Phone' is more or less than limit!");
                    }
                }//if end of Name Validation
                else
                {
                    MessageBox.Show("TextBox 'Name' is more or less than limit!");
                }

            }
            else
            {
                MessageBox.Show("Please Select Patient Record from Table!");
            }
        }

        //Search
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" || textBox5.Text != "")
            {
                AddRtb2();
                cmd = new SqlCommand("select * from [Patient] where Name like N'" + textBox4.Text + "%' and Phone like N'" + textBox5.Text + "%'", con);
                con.Open();
                adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();


                if (dt == null)
                {
                    MessageBox.Show("Patient Record is not found!");
                }
                else
                {

                }
            }
            else {
                MessageBox.Show("Please Enter Name or PhoneNumber to Search!");
            }
        }

        //View
        private void button5_Click(object sender, EventArgs e)
        {
            ClearData();
            DisplayData();
        }

        //Lougout
        private void button6_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Dispose();
        }

        //Closing form
        private void UserFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        //For Bill Table
        private void button9_Click(object sender, EventArgs e)
        { 
            if (textBox1.Text != "" && richTextBox9.Text != "")
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(richTextBox9.Text,"^[ 0-9]*$"))
                {
                    string str = label12.Text;
                    string PatientName = textBox1.Text;
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO dbo.[Bill_tbl](Date,Bill,Doctor_Name,Doctor_id,Patient_id) SELECT @d, @b,@docName, (select Id from dbo.[User] where UsrName = '" + str + "'),(select Id from dbo.[Patient] where Id=" + ID + ")", con);
                    con.Open();

                    cmd2.Parameters.AddWithValue("@d", dateTimePicker1.Text);
                    cmd2.Parameters.AddWithValue("@b", richTextBox9.Text);
                    cmd2.Parameters.AddWithValue("@docName", label12.Text);

                    SqlDataAdapter adapt2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    adapt2.Fill(ds2);
                    con.Close();
                    DisplayData();
                    ClearData();
                }
                else { MessageBox.Show("Please Enter Bill in English Number!"); }
            }
            else
            {
                MessageBox.Show("Please Select Row Head of Patient Record Table or Enter Bill Amount!");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label1.Text = "   အမည္";
            label5.Text = "     ဖုန္း  ";
            label3.Text = "အသက္";
            label2.Text = "      လိင္";
            label4.Text = "       အိမ္ေထာင္ ";
            label6.Text = "     ေသြးအုပ္စု";
            label8.Text = "     ရက္စြဲ";
            label7.Text = "     လိပ္စာ";
            label9.Text = " အမည္";
            label10.Text = " ဖုန္း  ";
            label14.Text = "မဂ္လာပါ";
            button2.Text = "မြမ္းမံ";
            button3.Text = "လူနာသစ္ထည့္သြင္း";
            button4.Text = "ရွာေဖြ";
            button5.Text = "အားလံုးရွာ";
            button6.Text = "ထြက္";
            button7.Text = "ရွင္းလင္း";
            button8.Text = "စံုစမ္း";
            button9.Text = "က်ေငြ";
            button12.Text = "ေနာက္သိဳ့";
            groupBox1.Text = "လူနာ မွတ္တမ္း";
            groupBox2.Text = "ဖ်ားနာ မွတ္တမ္း";
            groupBox3.Text = "အၿခား မွတ္တမ္း";
            groupBox4.Text = "ေရာဂါစစ္တမ္း နွင့္ ကုသမွဳမွတ္တမ္း";
            groupBox5.Text = "စစ္ေဆးခ်က္";
            groupBox6.Text = "မတည့္ေသာေဆးမ်ား";
            groupBox7.Text = "ဓါတ္ခြဲခန္း ေတြ့ရွိခ်က္";
            groupBox8.Text = "က်သင့္ေငြ";
            groupBox9.Text = "Current Condition";


        }

        private void button11_Click(object sender, EventArgs e)
        {
            label1.Text = "Name : ";
            label5.Text = "Phone : ";
            label3.Text = "Age : ";
            label2.Text = "Gender : ";
            label4.Text = "Marital Status : ";
            label6.Text = "Blood Type : ";
            label8.Text = "DateVisit : ";
            label7.Text = "Address : ";
            label9.Text = "Name : ";
            label10.Text = "Phone : ";
            label14.Text = "Welcome";
           button2.Text = "UPDATE";
            button3.Text = "ADD New Patient";
            button4.Text = "SEARCH";
            button5.Text = "View All";
            button6.Text = "LogOut";
            button7.Text = "CLEAR";
            button8.Text = "Contact Us";
            button9.Text = "Charge";
            button12.Text = "BACK";
            groupBox1.Text = "Personal History";
            groupBox2.Text = "History of Present Illness";
            groupBox3.Text = "Other History";
            groupBox4.Text = "Diagnosis and Treatment";
            groupBox5.Text = "Physical Examination";
            groupBox6.Text = "Allergic History";
            groupBox7.Text = "Laboratory Findings";
            groupBox9.Text = "Current Condition";
            groupBox8.Text = "Bill";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            af.Show();
            this.Dispose();
        }

        //Contact
        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contact Us!"  + Environment.NewLine + "www.initmyanmar.com" + Environment.NewLine + "initmyanmar@gmail.com" + Environment.NewLine + "Phone : +959455821510" + Environment.NewLine + "fax : 07450604");
        }

        //delete
        /*  private void button9_Click_1(object sender, EventArgs e)
          {
              if (ID != 0)
              {
                  cmd = new SqlCommand("delete [UsrTable] where Id=@id", con);
                  con.Open();
                  cmd.Parameters.AddWithValue("@id", ID);
                  cmd.ExecuteNonQuery();
                  con.Close();
                  MessageBox.Show("Patient Record Deleted Successfully!");
                  DisplayData();
                  ClearData();
              }
              else
              {
                  MessageBox.Show("Please Select Patient Record to Delete");
              }
          }

      */
    }
}
