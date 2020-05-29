using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace CMS
{

    public partial class Form1 : Form
    {
        public string pa;

        public Form1()
        {
            InitializeComponent();

        }

        public Form1(string v)
        {
            this.v = v;
        }


        //Connection String
        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\App\CMS\PatientHistory.mdf;Integrated Security=True;Connect Timeout=30";
        private string v;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please provide UserName and Password");
                return;

            }

            else if (comboBox1.Text == "Admin")
            {
                if (textBox1.Text == "admin" && textBox2.Text == "admin")
                {
                    AdminForm fm = new AdminForm();
                    fm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Admin UserName or Passoword!");
                }
            }

            else
            {
                try
                {
                    //Create SqlConnection
                    SqlConnection con = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand("Select * from [User] where UsrName=@username and Psw=@password", con);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text);
                    con.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapt.Fill(ds);
                    con.Close();

                    pa = textBox1.Text;

                    int count = ds.Tables[0].Rows.Count;

                    if (count == 1)
                    {
                        //Parameter passing to form 2
                        UserFrm fm = new UserFrm(pa);
                        fm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("User Name are Password are not Correct!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }//end of else 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
