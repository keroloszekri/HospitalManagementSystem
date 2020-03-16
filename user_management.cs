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
    public partial class user_management : Form
    {
        MySqlDataAdapter da;
        DataSet ds;
        public user_management()
        {
            InitializeComponent();
            getuser();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void user_management_Load(object sender, EventArgs e)
        {

        }
        void getuser()
        {
            DAL.DAL.cmd = new MySql.Data.MySqlClient.MySqlCommand("select us_id as'UserName',us_name as'Name',us_pass as'Password',us_type as'Type' from user", DAL.DAL.con);
             da = new MySqlDataAdapter(DAL.DAL.cmd);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource=ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this user", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DAL.DAL.con.Open();
                DAL.DAL.cmd = new MySqlCommand("delete from user where us_name=@a", DAL.DAL.con);
                DAL.DAL.cmd.Parameters.AddWithValue("@a", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                DAL.DAL.cmd.ExecuteNonQuery();
                DAL.DAL.con.Close();
                getuser();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            DAL.DAL.cmd = new MySql.Data.MySqlClient.MySqlCommand("select us_id as'UserName',us_name as'Name',us_pass as'Password',us_type as'Type' from user where us_id like @a or us_name like @a or us_pass like @a or us_type like @a", DAL.DAL.con);
            DAL.DAL.cmd.Parameters.AddWithValue("@a","%"+ textBox5.Text+"%");
            MySqlDataAdapter da = new MySqlDataAdapter(DAL.DAL.cmd);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            da.Update(ds);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FRM_user_print fr = new PL.FRM_user_print();
            RPT.RPT_All_User myrpt = new RPT.RPT_All_User();
            myrpt.Refresh();
            fr.crystalReportViewer1.ReportSource = myrpt;
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            PL.FRM_user_print p = new PL.FRM_user_print();
            RPT.RPT_User_Info myrpt = new RPT.RPT_User_Info();
           // myrpt.SetDatabaseLogon("root", "root");
            myrpt.SetParameterValue("id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            p.crystalReportViewer1.ReportSource = myrpt;
            p.Show();
            this.Cursor = Cursors.Default;
        }
    }
}
