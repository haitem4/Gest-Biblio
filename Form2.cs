using System;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Gest_Biblio
{
    public partial class Form2 : Form
    {
        SqlConnection cnx = new SqlConnection(@"server=HTM-HP;database=Gest_biblio;integrated security=true");
        SqlCommand cmd = new SqlCommand();
        string typeUser="User";
        public Form2()
        {
            InitializeComponent();
            if (Form1.isUserAdmin) button8.Visible = true;
        }
        public Form2(string typeUser)
        {
            InitializeComponent();
            if (typeUser=="Administrateur") button8.Visible = true;
            this.typeUser = typeUser;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f = new Form4(this.typeUser,this);
            f.Show();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f = new Form3(this.typeUser, this);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f = new Form5(this.typeUser, this);
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f = new Form6(this.typeUser, this);
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f = new Form7(this.typeUser,this);
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
            this.Hide();
            Form1 fo = new Form1();
            fo.Show();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
    
            Form8 f = new Form8(this.typeUser, this);
            f.Show();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

           
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f = new Form10(this.typeUser, this);
            f.Show();

        }
    }
}
