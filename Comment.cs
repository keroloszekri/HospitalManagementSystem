using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace ADB
{
    public partial class Form5 : Form
    {
        String Count,Count2;
        public Form5()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                DAL.DAL.con.Open();
                if (textBox1.Text != "" && richTextBox1.Text != "")
                {
                    DAL.DAL.cmd = new MySqlCommand("insert into comment (pa_national_id,comment,com_date) values (@p1,@p2,@p3)", DAL.DAL.con);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p1", textBox1.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p2", richTextBox1.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p3", dateTimePicker1.Value.Date);
                    DAL.DAL.cmd.ExecuteNonQuery();
                    MessageBox.Show("data saved sucsess");
                }
                else
                {
                    MessageBox.Show("you must full missing data");
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                DAL.DAL.con.Close();
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
         try 
            {
                DAL.DAL.cmd = new MySqlCommand("SELECT pa_name FROM patient", DAL.DAL.con);
                DAL.DAL.con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(DAL.DAL.cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MyCollection.Add(dt.Rows[i][0].ToString());
                }
               
                textBox2.AutoCompleteCustomSource = MyCollection;
                textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
                DAL.DAL.con.Close();
            }
            catch { return; }
            richTextBox1.Enabled = false;
            textBox1.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
           }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text !="" && richTextBox1.Text !="")
            {
                this.Cursor = Cursors.WaitCursor;
            PL.FRM_user_print p = new PL.FRM_user_print();
                RPT.RPT_Selected_Comment myrpt = new RPT.RPT_Selected_Comment();
                myrpt.SetParameterValue("id", textBox1.Text);
                myrpt.SetParameterValue("cdate", dateTimePicker1.Value.Date);

                //RPT.RPT_Comment myrpt = new RPT.RPT_Comment();
                //myrpt.SetParameterValue("id",textBox1.Text);
                p.crystalReportViewer1.ReportSource = myrpt;
            p.Show();
            this.Cursor = Cursors.Default;
            }
           
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                DAL.DAL.con.Open();
                DAL.DAL.cmd = new MySqlCommand("select c.pa_national_id from patient c where '" + textBox2.Text + "'=(select pa_name from patient where  pa_national_id = c.pa_national_id  );", DAL.DAL.con);
                DAL.DAL.cmd.ExecuteNonQuery();
                Count = DAL.DAL.cmd.ExecuteScalar().ToString();
                textBox1.Text = Count;
                if (textBox1.Text != "")
                {
                    richTextBox1.Enabled = true;
                }
                DAL.DAL.cmd = new MySqlCommand("select pa_national_id from patient  where pa_name='" + textBox2.Text + "'", DAL.DAL.con);
                DAL.DAL.cmd.ExecuteNonQuery();
                Count2 = DAL.DAL.cmd.ExecuteScalar().ToString();
                if (Count2 != textBox1.Text)
                {
                    richTextBox1.Enabled = false;
                    textBox1.Text = "";

                }

            }
            catch (Exception)
            {

            }
            finally
            {
                DAL.DAL.con.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
        }

    }
}
