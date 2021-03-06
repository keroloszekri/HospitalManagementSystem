﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADB.PL
{
    public partial class Examination_mange : Form
    {
        MySqlDataAdapter da;
        DataSet ds = new DataSet();
        public Examination_mange()
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
            DAL.DAL.cmd = new MySqlCommand("get_examination", DAL.DAL.con);
            DAL.DAL.cmd.CommandType = CommandType.StoredProcedure;
            da = new MySqlDataAdapter(DAL.DAL.cmd);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want update this person", "Confirm update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    da.Update(ds);
                    //dataGridView1.Columns[0].ReadOnly = true;
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
            Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                ds.Tables.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                DAL.DAL.cmd = new MySql.Data.MySqlClient.MySqlCommand("search_examination", DAL.DAL.con);
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PL.FRM_user_print p = new PL.FRM_user_print();
                RPT.RPT_examination myrpt = new RPT.RPT_examination();
                myrpt.SetParameterValue("id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                p.crystalReportViewer1.ReportSource = myrpt;
                p.Show();
                this.Cursor = Cursors.Default;
            }
            catch { return; }
        }
    }
}
