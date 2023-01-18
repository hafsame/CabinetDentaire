using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CabinetDentaire
{
    public partial class MenuCD : Form
    {
        public static SqlConnection Cnx;
        public MenuCD()
        {
            InitializeComponent();
        }

        private void MenuCD_Load(object sender, EventArgs e)
        {
            ConnectToDb();
            GetNbPatientNRdv();
            Form1 r = new Form1();
            r.ShowDialog();
        }

        public void GetNbPatientNRdv()
        {
            SqlCommand cmd = new SqlCommand("select count(*) from rdv", MenuCD.Cnx);
            MenuCD.Cnx.Open();
            int nbRdv = int.Parse(cmd.ExecuteScalar().ToString());

            SqlCommand cmd2 = new SqlCommand("select count(*) from patient", MenuCD.Cnx);
            int nbPatient = int.Parse(cmd2.ExecuteScalar().ToString());

            MenuCD.Cnx.Close();

            label1.Text = "Nombre de patients : " + nbPatient;
            label2.Text = "Nombre de rendez vous : " + nbRdv;
        }

        public void ConnectToDb()
        {
            try
            {
                SqlConnection connexion = new SqlConnection("Data Source=DESKTOP-S9F159B\\SQLEXPRESS;Initial Catalog=cabinetdentaire;Integrated Security=True");
                Cnx = connexion;
            }
            catch
            {
                MessageBox.Show("Impossible de se connecter a la BD.");
            }
        }

        private void ajouterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPatient t = new AddPatient();
            t.ShowDialog();
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePatient t = new UpdatePatient();
            t.ShowDialog();
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeletePatient p = new DeletePatient();
            p.ShowDialog();
        }

        private void ajouterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddRdv r = new AddRdv();
            r.ShowDialog();
        }

        private void annulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiscardRdv y = new DiscardRdv();
            y.ShowDialog();
        }

        private void modifierToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UpdateRdv y = new UpdateRdv();
            y.ShowDialog();
        }

        private void consulterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consulter t = new Consulter();
            t.ShowDialog();
        }

        private void imprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FichePatient t = new FichePatient();
            t.ShowDialog();
        }
    }
}
