using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CMS
{
    public partial class AdminForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\App\CMS\PatientHistory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        int ID = 0;
        public string pa;
        public AdminForm()
        {   
            InitializeComponent();
            DisplayData();
        }

        //DisplayData
        private void DisplayData()
        {
            con.Open();

            DataTable dt = new DataTable();
            cmd = new SqlCommand("select * from [User]", con);
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
            ID = 0;
        }  

        //Insert
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                cmd = new SqlCommand("insert into [User](UsrName,Psw) values(@Name,@Password)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record Inserted Successfully");

                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }  
        }

        //Update
        private void button2_Click(object sender, EventArgs e)
        {
             if (textBox1.Text != "" && textBox2.Text != "")
            {
                cmd = new SqlCommand("update [User] set UsrName=@name,Psw=@Password where Id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Record Updated Successfully");
                con.Close();
               

                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }  
            }
/*
        //Delete
        private void button3_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete [User] where Id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }  
        }
*/
        //Search
        private void button4_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from [User] where UsrName like '" + textBox1.Text + "%'", con);
            con.Open();
            adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            if (dt == null)
            {
                MessageBox.Show("Database Not found!");
            }
            else
            {

            }
        }

        //View
        private void button5_Click(object sender, EventArgs e)
        {
            ClearData();
            DisplayData();
        }

        //Select row to perform functions
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
            {
                ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            else {
                MessageBox.Show("Please Select Correct RowHeader!");
            }
            }

        //To close the form 
        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'patientHistoryDataSet.User' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.patientHistoryDataSet.User);
           

        }
      
        private void button3_Click(object sender, EventArgs e)
        {
               
        UserFrm uf = new UserFrm(pa);
            uf.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StatForm sf = new StatForm();
            sf.Show();
            this.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Dispose();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label1.Text = "Doctor Name";
            label2.Text = "Password";
            button1.Text = "ADD";
            button2.Text = "UPDATE";
            button3.Text = "Patients List";
            button4.Text = "SEARCH";
            button5.Text = "View All";
            button6.Text = "Statistics ";
            button7.Text = "LogOut";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label1.Text = "ဆရာ၀န္နာမည္";
            label2.Text = "လွ်ိဳ၀ွက္စာစု";
            button1.Text = "ထည့္သြင္း";
            button2.Text = "မြမ္းမံ";
            button3.Text = "လူနာမ်ားမွတ္တမ္း";
            button4.Text = "ရွာေဖြ";
            button5.Text = "အားလံုးရွာ";
            button6.Text = "စာရင္း";
            button7.Text = "ထြက္";
        }
    }
}
