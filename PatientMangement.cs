using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADB.PL
{
    public partial class PatientMangement : Form
    {
        MySqlDataAdapter da;
        DataSet ds = new DataSet();
        public PatientMangement()
        {
            InitializeComponent();
            getpateint();
           

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void PatientMangement_Load(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                ds.Tables.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                DAL.DAL.cmd = new MySql.Data.MySqlClient.MySqlCommand("patientmanagement", DAL.DAL.con);
                DAL.DAL.cmd.CommandType = CommandType.StoredProcedure;
                DAL.DAL.cmd.Parameters.AddWithValue("@input","%"+ textBox5.Text+"%");
                MySqlDataAdapter da = new MySqlDataAdapter( DAL.DAL.cmd);
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].ReadOnly = true;
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want delete this person", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DAL.DAL.con.Open();
                    DAL.DAL.cmd = new MySql.Data.MySqlClient.MySqlCommand("delete from patient where pa_national_id =@pa_national_id", DAL.DAL.con);
                    DAL.DAL.cmd.Parameters.AddWithValue("@pa_national_id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    DAL.DAL.cmd.ExecuteNonQuery();
                    DAL.DAL.con.Close();
                    getpateint();
                    dataGridView1.Columns[0].ReadOnly = true;
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void getpateint()
        {
            dataGridView1.DataSource = null;
            ds.Tables.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            DAL.DAL.cmd = new MySqlCommand("patientmanagement",DAL.DAL.con);
            DAL.DAL.cmd.CommandType = CommandType.StoredProcedure;
            DAL.DAL.cmd.Parameters.AddWithValue("@input","%%");
            da = new MySqlDataAdapter(DAL.DAL.cmd);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want update this person", "Confirm update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    da.Update(ds);
                    dataGridView1.Columns[0].ReadOnly = true;
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                getpateint();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            PL.FRM_user_print p = new PL.FRM_user_print();
            RPT.RPT_patiant_info myrpt = new RPT.RPT_patiant_info();
            myrpt.SetParameterValue("id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            p.crystalReportViewer1.ReportSource = myrpt;
            p.Show();
            this.Cursor = Cursors.Default;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
