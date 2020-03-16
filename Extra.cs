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
    public partial class Form7 : Form
    {
        Boolean b = false;
        public Form7()
        {
            InitializeComponent();
            pictureBox1.Hide();
            pictureBox2.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DAL.DAL.con.Open();
            try
            {
                if (b)
                {

                    DAL.DAL.cmd = new MySqlCommand("insert into others(pa_national_id,urine_analysis,stool_anylsis,Alpha_feta,Ascitic_fluid,other)values('" + textBox1.Text + "','" + textBox2.Text + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + comboBox1.Text + "','" + richTextBox3.Text + "')", DAL.DAL.con);
                    DAL.DAL.cmd.ExecuteNonQuery();
                    MessageBox.Show("Successed entry");
                    DAL.DAL.con.Close();
                    foreach (Control c in groupBox1.Controls)
                    {
                        if (c is TextBox)
                            c.Text = "";
                        if (c is RichTextBox)
                            c.Text = "";
                        if (c is ComboBox)
                            c.Text = "";
                    }

                }
                else
                    MessageBox.Show("Please enter correct id");
            }
            catch (Exception ex)
            {
                if (!b)
                    MessageBox.Show("Please enter correct id");
                else
                    MessageBox.Show(ex.ToString());
            }
            DAL.DAL.con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DAL.DAL.con.Open();
            try
            {
                DAL.DAL.cmd = new MySqlCommand("select pa_name from patient where pa_national_id= '" + textBox1.Text + "'", DAL.DAL.con);
                string count = DAL.DAL.cmd.ExecuteScalar().ToString();
                if (count != null)
                {
                    b = true;
                    pictureBox1.Hide();
                    pictureBox2.Show();
                }

            }
            catch (Exception )
            {
                b = false;
                pictureBox2.Hide();
                pictureBox1.Show();
                //MessageBox.Show(ex.ToString());
            }
            DAL.DAL.con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c is TextBox)
                    c.Text = "";
                if (c is RichTextBox)
                    c.Text = "";
                if (c is ComboBox)
                    c.Text = "";
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        string Count2;
        private void Form7_Load(object sender, EventArgs e)
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

                textBox7.AutoCompleteCustomSource = MyCollection;
                textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox7.AutoCompleteMode = AutoCompleteMode.Suggest;
                DAL.DAL.con.Close();
            }
            catch { return; }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DAL.DAL.con.Open();
                DAL.DAL.cmd = new MySqlCommand("select pa_national_id from patient  where pa_name='" + textBox7.Text + "'", DAL.DAL.con);
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
