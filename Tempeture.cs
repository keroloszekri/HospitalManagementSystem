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
    public partial class Form3 : Form
    {
        String Count, Count2;
        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                DAL.DAL.con.Open();
                if (textBox1.Text != "" )
                {
                    DAL.DAL.cmd = new MySqlCommand("insert into tempresture_chart values (@p1,@p2,@p3,@p4,@p5,@p6)", DAL.DAL.con);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p1", textBox1.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p3", textBox3.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p4", textBox4.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p5", textBox5.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@p6", textBox6.Text);
                   
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

        private void Form3_Load(object sender, EventArgs e)
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
            catch (Exception )
            {

            }
            finally
            {
                DAL.DAL.con.Close();

            }
        }
    }
}
