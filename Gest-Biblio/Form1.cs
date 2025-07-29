using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Gest_Biblio
{
    public partial class Form1 : Form
    {
        SqlConnection cnx = new SqlConnection(@"server=HTM-HP;database=Gest_biblio;integrated security=true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public static Boolean isUserAdmin = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Id User";
                textBox1.ForeColor = Color.Silver;

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.ForeColor = Color.Black;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Password";
                textBox2.ForeColor = Color.Silver;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = true;
                textBox2.PasswordChar = '*';
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool tr = false;
            string pwd,type;
            SqlCommand cmd = new SqlCommand("select * from [User] where IdUser='"+textBox1.Text+"'", cnx);
            cnx.Open();
            cmd.Connection = cnx;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                pwd = dr[1].ToString();
                type = dr[7].ToString();
                if (pwd == textBox2.Text)
                {
                   
                    this.Hide();
                    Form2 f = new Form2(type);
                    f.Show();
                }
                else
                {
                    MessageBox.Show("Le mot de passe est incorrect!");
                }

            } else
            {
                MessageBox.Show("L'indeifiant est incorrecte!");
            }
            cnx.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Etes-vous certain de vouloir quitter ?", "Quitter", MessageBoxButtons.YesNo);
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            Application.Exit();
        }
    }
}
