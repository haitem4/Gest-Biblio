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

namespace Gest_Biblio
{
    public partial class Form9 : Form
    {
        SqlConnection cnx = new SqlConnection("server=HTM-HP;database=Gest_biblio;integrated security=true");
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
          
           CrystalReport1 cr = new CrystalReport1();
            cr.SetParameterValue("P1", Form7.I);
            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Refresh();

        }
    }
}
