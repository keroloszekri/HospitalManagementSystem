using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace ADB.PL
{
    public partial class Doctor_patient_manag : Form
    {
        MySqlDataAdapter da;
        DataSet ds = new DataSet();
        public Doctor_patient_manag()
        {
            InitializeComponent();
            getpateint();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        void getpateint()
        {
            dataGridView1.DataSource = null;
            ds.Tables.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            DAL.DAL.cmd = new MySqlCommand("doctor_patient ", DAL.DAL.con);
            DAL.DAL.cmd.CommandType = CommandType.StoredProcedure;
            da = new MySqlDataAdapter(DAL.DAL.cmd);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                ds.Tables.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                DAL.DAL.cmd = new MySql.Data.MySqlClient.MySqlCommand("doctor_patient_search", DAL.DAL.con);
                DAL.DAL.cmd.CommandType = CommandType.StoredProcedure;
                DAL.DAL.cmd.Parameters.AddWithValue("@skey", "%" + textBox5.Text + "%");
                MySqlDataAdapter da = new MySqlDataAdapter(DAL.DAL.cmd);
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            PL.FRM_user_print p = new PL.FRM_user_print();
            RPT.RPT_Selected_Comment myrpt = new RPT.RPT_Selected_Comment();
            myrpt.SetParameterValue("id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            myrpt.SetParameterValue("cdate", Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value.ToString()).Date);
            p.crystalReportViewer1.ReportSource = myrpt;
            p.Show();
            this.Cursor = Cursors.Default;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
