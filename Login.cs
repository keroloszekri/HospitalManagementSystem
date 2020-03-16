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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                if (com_type.Text == "" || txt_password.Text == "" || txt_userName.Text == "")
                {
                    MessageBox.Show("please fill all data", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DAL.DAL.cmd = new MySqlCommand("select us_name,us_pass,us_type from user where us_id=@a and us_pass=@b and us_type=@c", DAL.DAL.con);
                    DAL.DAL.cmd.Parameters.AddWithValue("@a", txt_userName.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@b", txt_password.Text);
                    DAL.DAL.cmd.Parameters.AddWithValue("@c", com_type.Text);

                    DAL.DAL.con.Open();
                    MySqlDataReader dr = DAL.DAL.cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        MessageBox.Show("login successfully", "login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string type = dr[2].ToString();

                        if (type == "Admin")
                        {
                            Main.getmainform.adminToolStripMenuItem.Enabled = true;
                            Main.getmainform.fileToolStripMenuItem.Enabled = true;
                            Main.getmainform.doctorToolStripMenuItem.Enabled = true;
                            Main.getmainform.receptionistToolStripMenuItem.Enabled = true;
                            Main.getmainform.radiologyOfficerToolStripMenuItem.Enabled = true;
                            Main.getmainform.labOfficerToolStripMenuItem.Enabled = true;
                        }
                        else if (type == "Receptionist")
                        {
                            Main.getmainform.receptionistToolStripMenuItem.Enabled = true;

                        }
                        else if (type == "Doctor")

                        {
                            Main.getmainform.adminToolStripMenuItem.Enabled = false;
                            Main.getmainform.fileToolStripMenuItem.Enabled = true;
                            Main.getmainform.doctorToolStripMenuItem.Enabled = true;
                            Main.getmainform.receptionistToolStripMenuItem.Enabled = false;
                            Main.getmainform.radiologyOfficerToolStripMenuItem.Enabled = true;
                            Main.getmainform.labOfficerToolStripMenuItem.Enabled = true;

                        }
                        else if (type == "Radiology Officer")
                        {
                            Main.getmainform.adminToolStripMenuItem.Enabled = false;
                            Main.getmainform.fileToolStripMenuItem.Enabled = true;
                            Main.getmainform.doctorToolStripMenuItem.Enabled = false;
                            Main.getmainform.receptionistToolStripMenuItem.Enabled = false;
                            Main.getmainform.labOfficerToolStripMenuItem.Enabled = false;
                            Main.getmainform.radiologyOfficerToolStripMenuItem.Enabled = true;
                        }
                        else
                        {
                            Main.getmainform.adminToolStripMenuItem.Enabled = false;
                            Main.getmainform.fileToolStripMenuItem.Enabled = true;
                            Main.getmainform.doctorToolStripMenuItem.Enabled = false;
                            Main.getmainform.receptionistToolStripMenuItem.Enabled = false;
                            Main.getmainform.labOfficerToolStripMenuItem.Enabled = false;
                            Main.getmainform.radiologyOfficerToolStripMenuItem.Enabled = false;
                            Main.getmainform.labOfficerToolStripMenuItem.Enabled = true;
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("login error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    DAL.DAL.con.Close();
                }
            }

            catch (Exception )
            {
                return;
            }
        }
    }
}
