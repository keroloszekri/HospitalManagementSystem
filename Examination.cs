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

namespace ADB.PL
{
    public partial class Examination : Form
    {
        string Count;
        Boolean i;
        public Examination()
        {
            InitializeComponent();
            MakeAllFalse();
        }

        public void MakeAllFalse()
        {
            foreach (Control c in groupBox2.Controls)
            {
                if (c is RichTextBox)
                    c.Enabled = false;
            }
            foreach (Control c in groupBox3.Controls)
            {
                if (c is TextBox)
                    c.Enabled = false;

            }
        }

        public void MakeAllTrue()
        {
            foreach (Control c in groupBox2.Controls)
            {
                if (c is RichTextBox)
                    c.Enabled = true;
            }
            foreach (Control c in groupBox3.Controls)
            {
                if (c is TextBox)
                    c.Enabled = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox2.Controls)
            {
                if (c is RichTextBox||c is TextBox) {

                    ((RichTextBox)c).Text = "";
                }
            }
            foreach (Control c in groupBox3.Controls)
            {
                if (c is RichTextBox || c is TextBox)
                {

                    ((TextBox)c).Text = "";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.DAL.con.Open();
                DAL.DAL.cmd = new MySqlCommand("select c.pa_national_id from patient c where '" + textBox5.Text + "'=(select pa_name from patient where  pa_national_id = c.pa_national_id  );", DAL.DAL.con);
                DAL.DAL.cmd.ExecuteNonQuery();
                Count = DAL.DAL.cmd.ExecuteScalar().ToString();
            }
            catch (Exception ee)
            {
                if (Count == null)
                {
                    MessageBox.Show("name not found");
                }
                else
                    MessageBox.Show(ee.Message);
            }
            finally
            {
                DAL.DAL.con.Close();
                if (Count != null)
                {
                    textBox6.Text = Count;
                    i = true;
                    MakeAllTrue();
                }
                else
                {
                    MakeAllFalse();
                    MessageBox.Show("name not found");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                DAL.DAL.con.Open();
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" )
                {
                    DAL.DAL.cmd = new MySqlCommand("insert into examination (pa_national_id,ex_general_lock,ex_pulse,ex_blood_pressure,ex_temperature,ex_respratory_rate,ex_head,ex_chest,ex_cardiac,ex_abdominal,ex_inspection,ex_superfical,ex_deep,ex_percussion,ex_ausculation,ex_provisional) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16)", DAL.DAL.con);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p1", textBox6.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p2", richTextBox1.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p3", textBox1.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p4", textBox3.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p5", textBox2.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p6", textBox4.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p7", richTextBox2.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p8", richTextBox3.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p9", richTextBox4.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p10", richTextBox5.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p11", richTextBox11.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p12", richTextBox6.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p13", richTextBox7.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p14", richTextBox8.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p15", richTextBox9.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p16", richTextBox10.Text);
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Examination_Load(object sender, EventArgs e)
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

                textBox5.AutoCompleteCustomSource = MyCollection;
                textBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox5.AutoCompleteMode = AutoCompleteMode.Suggest;
                DAL.DAL.con.Close();
            }
            catch { return; }
        }
    }
}
