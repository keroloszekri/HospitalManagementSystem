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
    public partial class electrolytes : Form
    {
        string Count;
        bool t = false;
        public electrolytes()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.DAL.con.Open();
                DAL.DAL.cmd = new MySqlCommand("select c.pa_national_id from patient c where '" + textBox5.Text + "'=(select pa_name from patient where  pa_national_id = c.pa_national_id  );", DAL.DAL.con);
                DAL.DAL.cmd.ExecuteNonQuery();
                Count = DAL.DAL.cmd.ExecuteScalar().ToString();
                if (Count != null)
                {
                    textBox6.Text = Count;
                    t = true;
                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true; 
                    textBox7.Enabled = true;
                    textBox8.Enabled = true;
                    textBox9.Enabled = true;
                   

                }
                else
                {
                    MessageBox.Show("name not found");
                }

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
            }
        }

        private void electrolytes_Load(object sender, EventArgs e)
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
            if (t==false)
            {
          
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox8.Enabled = false;
            textBox7.Enabled = false;
            textBox9.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                DAL.DAL.con.Open();
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox7.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox9.Text != "" && textBox8.Text != "")
                {
                    DAL.DAL.cmd = new MySqlCommand("insert into electrolytes (electrolytes_parameters,electrolytes_NA,electrolytes_K,electrolytes_Mg,electrolytes_Phophorus,electrolytes_urea,electrolytes_creatine,pa_national_id) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", DAL.DAL.con);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p1", textBox1.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p3", textBox7.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p4", textBox9.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p5", textBox3.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p6", textBox4.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p7", textBox8.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p8", textBox6.Text);
                    DAL.DAL.cmd.ExecuteNonQuery();
                    MessageBox.Show("data saved sucsess");
                }
                else
                {
                    MessageBox.Show("you must full missing data");
                }
               
               
            }
            catch(Exception ee)
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
        textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
