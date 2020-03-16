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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
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

                textBox11.AutoCompleteCustomSource = MyCollection;
                textBox11.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox11.AutoCompleteMode = AutoCompleteMode.Suggest;
                DAL.DAL.con.Close();
            }
            catch { return; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DAL.DAL.cmd = new MySqlCommand("insert into laboratory_complete_blood_picture(pa_national_id,Complete_parameters,Complete_hp,Complete_mcv,Complete_mch,Complete_reticulocyte,Complete_platelets,Complete_tlc,Complete_neutrophil,Complete_lymphocyte) values(@a,@b,@c,@e,@f,@g,@d,@h,@m,@n)", DAL.DAL.con);
            DAL.DAL.cmd.Parameters.AddWithValue("@a", textBox1.Text);
            DAL.DAL.cmd.Parameters.AddWithValue("@b", textBox2.Text);
            DAL.DAL.cmd.Parameters.AddWithValue("@c", textBox3.Text);
            DAL.DAL.cmd.Parameters.AddWithValue("@e", textBox4.Text);
            DAL.DAL.cmd.Parameters.AddWithValue("@f", textBox5.Text);
            DAL.DAL.cmd.Parameters.AddWithValue("@g", textBox6.Text);
            DAL.DAL.cmd.Parameters.AddWithValue("@d", textBox7.Text);
            DAL.DAL.cmd.Parameters.AddWithValue("@h", textBox8.Text);
            DAL.DAL.cmd.Parameters.AddWithValue("@m", textBox9.Text);
            DAL.DAL.cmd.Parameters.AddWithValue("@n", textBox10.Text);
            DAL.DAL.con.Open();
            DAL.DAL.cmd.ExecuteNonQuery();
            MessageBox.Show("Added successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DAL.DAL.con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        string Count2;
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DAL.DAL.con.Open();
                DAL.DAL.cmd = new MySqlCommand("select pa_national_id from patient  where pa_name='" + textBox11.Text + "'", DAL.DAL.con);
                //DAL.DAL.cmd.ExecuteNonQuery();
                Count2 = DAL.DAL.cmd.ExecuteScalar().ToString();
                textBox1.Text = Count2;

            }
            catch (Exception)
            {

            }
            finally
            {
                DAL.DAL.con.Close();

            }
        }
    }
}
