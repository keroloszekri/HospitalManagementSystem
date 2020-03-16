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
    public partial class Lab_managment : Form
    {
        MySqlDataAdapter da;
        DataSet ds = new DataSet();
        public Lab_managment()
        {
            InitializeComponent();
            getpateint();
        }
        void getpateint()
        {
            dataGridView1.DataSource = null;
            ds.Tables.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            DAL.DAL.cmd = new MySqlCommand("get_all_laboratory_complete_blood_picture ", DAL.DAL.con);
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
                DAL.DAL.cmd = new MySql.Data.MySqlClient.MySqlCommand("Search_all_laboratory_complete_blood_picture", DAL.DAL.con);
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
            RPT.RPT_Lab myrpt = new RPT.RPT_Lab();
            myrpt.SetParameterValue("id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
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
