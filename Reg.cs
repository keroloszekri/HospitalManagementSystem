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
    public partial class Form1 : Form
    {
        Boolean b = true;
        public Form1()
        {
            InitializeComponent();
            panel2.Hide();
        }

        public void cl()
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
                else if (c is ComboBox)
                    c.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Save")
            {
                DAL.DAL.con.Open();
                if (b)
                {
                    try
                    {
                        DAL.DAL.cmd = new MySqlCommand("insert into patient(pa_national_id,pa_name,pa_age,pa_sex,pa_material_state,pa_address,pa_complaint,pa_date,pa_history_of_illeness,pa_past_history,pa_family_history,pa_medical_history)values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + dateTimePicker1.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')", DAL.DAL.con);
                        DAL.DAL.cmd.ExecuteNonQuery();
                        MessageBox.Show("Successed entry");
                        DAL.DAL.con.Close();
                        cl();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                    MessageBox.Show("Please enter onther id");
                DAL.DAL.con.Close();
            }
            else
            {
                DAL.DAL.con.Open();

                try
                {
                    DAL.DAL.cmd = new MySqlCommand("update patient set pa_date = '" + dateTimePicker1.Text + "',pa_name = '" + textBox2.Text + "',pa_sex = '" + comboBox1.Text + "',pa_material_state='" + comboBox2.Text + "',pa_address='" + textBox10.Text + "',pa_complaint='" + textBox11.Text + "',pa_history_of_illeness='" + textBox6.Text + "',pa_past_history='" + textBox7.Text + "',pa_family_history='" + textBox8.Text + "',pa_age ='"+textBox3.Text+"',pa_medical_history='" + textBox9.Text + "' where pa_national_id='" + textBox1.Text + "'", DAL.DAL.con);
                    DAL.DAL.cmd.ExecuteNonQuery();
                    MessageBox.Show("Successed update");
                    DAL.DAL.con.Close();
                    cl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                DAL.DAL.con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cl();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DAL.DAL.con.Open();
            try
            {
                
                DAL.DAL.cmd = new MySqlCommand("select * from patient where pa_national_id= '" + textBox1.Text + "'", DAL.DAL.con);
                MySqlDataAdapter da = new MySqlDataAdapter(DAL.DAL.cmd);
                DataTable dt = new DataTable();
                
                da.Fill(dt);
                textBox2.Text = dt.Rows[0][1].ToString();
                textBox3.Text = dt.Rows[0][2].ToString();
                textBox6.Text = dt.Rows[0][8].ToString();
                textBox7.Text = dt.Rows[0][9].ToString();
                textBox8.Text = dt.Rows[0][10].ToString();
                textBox9.Text = dt.Rows[0][11].ToString();
                textBox10.Text = dt.Rows[0][5].ToString();
                textBox11.Text = dt.Rows[0][6].ToString();
                dateTimePicker1.Text = dt.Rows[0][7].ToString();
                comboBox1.Text = dt.Rows[0][3].ToString();
                comboBox2.Text = dt.Rows[0][4].ToString();
                button1.Text = "Update data";
            }
            catch (Exception)
            {
                b = true;
                panel2.Hide();
                button1.Text = "Save";
                DAL.DAL.con.Close();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                //MessageBox.Show(ex.ToString());
            }
            DAL.DAL.con.Close();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) == false && Char.IsControl(e.KeyChar) == false)
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) == false && Char.IsControl(e.KeyChar) == false)
                e.Handled = true;
        }
    }
}
