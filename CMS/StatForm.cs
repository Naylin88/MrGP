using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CMS
{
    public partial class StatForm : Form
    {
        public StatForm()
        {
            InitializeComponent();
        }

        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\App\CMS\PatientHistory.mdf;Integrated Security=True;Connect Timeout=30";

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SELECT Date,Bill,Doctor_Name FROM [Bill_tbl] WHERE Date >=@d1 and Date <= @d2", con);
            con.Open();
            cmd.Parameters.AddWithValue("@d1",dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@d2", dateTimePicker2.Value);
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            //Calculate Total

            int total = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows) {

                total += Convert.ToInt32(r.Cells[1].Value);
            }
            label1.Text = total.ToString();
        }

        private void StatForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'patientHistoryDataSet.Patient' table. You can move, or remove it, as needed.
            this.patientTableAdapter.Fill(this.patientHistoryDataSet.Patient);

        }
        //Logout
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Dispose();
        }

        //form closing event
        private void StatFromCls(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            af.Show();
            this.Dispose();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label2.Text = "ေန့စြဲ့ၿဖင့္ ၀င္ေငြရွာရန္";
            label4.Text = "ေန့စြဲ အစ ";
            label5.Text = "ေန့စြဲ အဆံုး ";
            button1.Text = "တြက္ခ်က္";
            button2.Text = "ထြက္";
            button3.Text = "ေနာက္သိဳ့";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label2.Text = "*Income Query by Dates*";
            label4.Text = "Start Date";
            label5.Text = "End Date";
            button1.Text = "Calculate";
            button2.Text = "LogOut";
            button3.Text = "BACK";
        }
    }
}
