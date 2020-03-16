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
using System.Drawing.Imaging;

namespace ADB
{
    public partial class Form2 : Form
    {
        String Count, Count2;
        Boolean b = false;
        public Form2()
        {
            InitializeComponent();
            pictureBox1.Hide();
            pictureBox2.Hide();
            
        }

        public void fu()
        {
            textBox1.Clear();
            foreach (Control c in panel1.Controls)
            {
                if (c is RichTextBox)
                {
                    c.Text = "";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean s = false;
            DAL.DAL.con.Open();
            try
            {
                if (b)
                {
                    s = true;
                    DAL.DAL.cmd = new MySqlCommand("insert into imaging_studies(pa_national_id,im_chest_X_array,im_abdominal,im_MSCT,im_others)values('" + textBox1.Text + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + richTextBox4.Text + "')", DAL.DAL.con);
                    DAL.DAL.cmd.ExecuteNonQuery();
                    MessageBox.Show("Successed entry");
                    DAL.DAL.con.Close();
                    fu();
                }
                else
                    MessageBox.Show("Please enter correct id");
            }
            catch (Exception ex)
            {
                if(!b)
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
            catch (Exception)
            {
                b = false;
                pictureBox2.Hide();
                pictureBox1.Show();
                //MessageBox.Show(ex.ToString());
            }
            DAL.DAL.con.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
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

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
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
    }
}
