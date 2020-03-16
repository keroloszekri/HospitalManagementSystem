using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ADB.PL
{
    public partial class FRM_CREATE_BACKUP : Form
    {
        MySqlConnection mysqlconnection=new MySqlConnection("server=localhost;user=root;pwd=root;database=clinic_sheet;");
        MySqlCommand cmd;
        public FRM_CREATE_BACKUP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {try { 

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;

                }
            }
            catch { return; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { 
            if (textBox1.Text != string.Empty)
            {
                string filename = textBox1.Text + "\\Clinic_db" + DateTime.Now.ToShortDateString().Replace('/', '-')
                      + " - " + DateTime.Now.ToShortTimeString().Replace(':', '-')+".sql";

                cmd = new MySqlCommand();
                using (MySqlBackup mb = new MySqlBackup(cmd))
                {
                    cmd.Connection = mysqlconnection;
                    mysqlconnection.Open();
                    mb.ExportToFile(filename);
                    mysqlconnection.Close();
                }
                   MessageBox.Show("The backup created successfully", "Create backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
            else
                MessageBox.Show("Please select the backup path  ", "Create backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { return; }
        }



        private void FRM_CREATE_BACKUP_Load(object sender, EventArgs e)
        {
            
        }
    }
    }

