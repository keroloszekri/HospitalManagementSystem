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
    public partial class Main : Form
    {
        public static Color color;
        private static Main frm;
        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }

        public static Main getmainform
        {
            get
            {
                if (frm == null)
                {
                    frm = new Main();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
        }

        public Main()
        {
            InitializeComponent();
            if (frm == null) { frm = this; }
            this.skinEngine1.SkinFile = "Skins/WaveColor2.ssk";

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
             PL.Login l = new Login();
              l.ShowDialog();
           
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bacToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PL.AddUser add = new AddUser();
            add.Show();
        }

        private void userMangementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PL.PatientMangement add = new PatientMangement();
            add.Show();
        }

        private void makeCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 d = new Form5();
                d.Show();
        }

        private void makeExaminationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PL.Examination ex = new Examination();
            ex.Show();
        }

        private void registerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new ADB.Form1();
            f.Show();
        }

        private void patientMangemenrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PL.patientmangementForReceptionist p = new PL.patientmangementForReceptionist();
            p.Show();
        }

        private void form5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cogulation_profile c = new cogulation_profile();
            c.Show();

        }

        private void liverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            liver l = new PL.liver();
            l.Show();
        }

        private void completeBloodPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 ff = new ADB.Form6();
            ff.Show();
        }

        private void electrolytesAndKidneyFunctionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            electrolytes el = new PL.electrolytes();
            el.Show();
        }

        private void ohersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 ff = new ADB.Form7();
            ff.Show();
        }

        private void imagingStudiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fff = new ADB.Form2();
            fff.Show();
        }

        private void backUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_CREATE_BACKUP c = new PL.FRM_CREATE_BACKUP();
            c.Show();
        }

        private void restoreBackUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_RESTORE r = new PL.FRM_RESTORE();
            r.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // DAL.DAL.con.Open();
            // MessageBox.Show(DAL.DAL.con.State.ToString());
            DAL.DAL.cmd = new MySqlCommand("select * from user", DAL.DAL.con);
            MySqlDataAdapter da = new MySqlDataAdapter(DAL.DAL.cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                adminToolStripMenuItem.Enabled = false;
                fileToolStripMenuItem.Enabled = false;
                AddUser r = new AddUser();
                r.ShowDialog();
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.getmainform.adminToolStripMenuItem.Enabled = false;
            Main.getmainform.fileToolStripMenuItem.Enabled = true;
            Main.getmainform.doctorToolStripMenuItem.Enabled = false;
            Main.getmainform.receptionistToolStripMenuItem.Enabled = false;
            Main.getmainform.radiologyOfficerToolStripMenuItem.Enabled = false;
            Main.getmainform.labOfficerToolStripMenuItem.Enabled = false;
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            user_management um = new user_management();
            um.Show();
        }

        private void makeBackUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_CREATE_BACKUP c = new PL.FRM_CREATE_BACKUP();
            c.Show();
        }

        private void restoreBackUpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FRM_RESTORE c = new PL.FRM_RESTORE();
            c.Show();
        }

        private void labOfficerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void labToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 asd = new Form6();
            asd.Show();
        }

        private void temperatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 asd = new Form3();
            asd.Show();
        }

        private void fluidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 asd = new Form4();
            asd.Show();
        }

        private void patientManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Doctor_patient_manag doc = new PL.Doctor_patient_manag();
            doc.Show();
        }

        private void showAllLabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lab_managment lab = new PL.Lab_managment();
            lab.Show();
        }

        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImagingStudies_mang m = new PL.ImagingStudies_mang();
            m.Show();
        }

        private void liverToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Liver_manage l = new PL.Liver_manage();
            l.Show();
        }

        private void electrolytesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            electrolytes_mang m = new electrolytes_mang();
            m.Show();
        }

        private void cogulationProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cogulation_profile_mang c = new cogulation_profile_mang();
            c.Show();
        }

        private void showExaminationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Examination_mange exam = new PL.Examination_mange();
            exam.Show();
        }
    }
}
