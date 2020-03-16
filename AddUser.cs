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
using System.Text.RegularExpressions;

namespace ADB.PL
{
    public partial class AddUser : Form
    {
        bool flag;
               
        public AddUser()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("fill all texts", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                DAL.DAL.cmd = new MySqlCommand("insert into user(us_id,us_name,us_pass,us_type) values(@a,@b,@c,@e)", DAL.DAL.con);
                DAL.DAL.cmd.Parameters.AddWithValue("@a", textBox2.Text);
                DAL.DAL.cmd.Parameters.AddWithValue("@b", textBox1.Text);
                DAL.DAL.cmd.Parameters.AddWithValue("@c", textBox3.Text);
                DAL.DAL.cmd.Parameters.AddWithValue("@e", comboBox1.Text);
                DAL.DAL.con.Open();
                DAL.DAL.cmd.ExecuteNonQuery();
                MessageBox.Show("Added successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DAL.DAL.con.Close();
                //Main.getmainform.adminToolStripMenuItem.Enabled = true;
                Main.getmainform.fileToolStripMenuItem.Enabled = true;
            } 
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        /////////////////////////////////////////////////////////////////////////////////
        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than 8 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one special case characters";
                return false;
            }
            else
            {
                return true;
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            try {
                    if (textBox2.Text.Length <=5)
                    {
                        MessageBox.Show("Username should not be less 5 characters");
                        textBox2.Clear();
                        textBox2.Focus();

                    }
                    else
                    {
                    DAL.DAL.cmd = new MySqlCommand("select * from user where us_id=@a", DAL.DAL.con);
                    DAL.DAL.cmd.Parameters.AddWithValue("@a", textBox2.Text);
                    DAL.DAL.con.Open();
                    MySqlDataReader dr = DAL.DAL.cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        MessageBox.Show("This user is already exists");
                        textBox2.Text = "";
                        textBox2.Focus();
                    }
                    DAL.DAL.con.Close();
               }
            }
            catch { return; }
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            try { 
            string message = "";
          flag= ValidatePassword(textBox3.Text, out message);
            if(message != string.Empty)
            {
             MessageBox.Show(message);
                textBox3.Clear();
                textBox3.Focus();
            }
            }
            catch { return; }
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            try { 
            if(textBox4.Text != textBox3.Text)
            {
                MessageBox.Show("Password must be the same");
                textBox4.Clear();
                    textBox4.Focus();
            }
            }
            catch { return; }
        }
    }
}
