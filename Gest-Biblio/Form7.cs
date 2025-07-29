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
    public partial class Form7 : Form
    {
        SqlConnection cnx = new SqlConnection("server=HTM-HP;database=Gest_biblio;integrated security=true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader DR;
        public static int I;
        string typeUser;
        Form parent = null;

        public Form7(string typeUser, Form p)
        {
            InitializeComponent();
            this.typeUser = typeUser;
            this.parent = p;

        }
        public Form7()
        {
            InitializeComponent();
           
        }
        int position;
        public void naviger()
        {
            cmd.Connection = cnx;
            cmd.CommandText = "select *from Emprunt";
            cnx.Open();
            DR = cmd.ExecuteReader();
            for (int i = 0; i <= position; i++)
            {
                DR.Read();
            }
            textBox1.Text = DR[0].ToString();
            textBox2.Text = DR[1].ToString();
            textBox3.Text = DR[2].ToString();
            Date_Debut.Text = DR[3].ToString();
            Date_Retour.Text = DR[4].ToString();
            comboBox2.Text = DR[5].ToString();
            DR.Close();
            cnx.Close();

        }
        public void actualiser_DataGrid()
        {

            dataGridView1.Rows.Clear();
            // comboBox1.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            // comboBox2.Items.Clear();
            cmd.CommandText = "select em.Id_Emprunt,em.Id_Adherant,A.Nom_Ad,A.Prenom_Ad,em.Id_Examp,ex.ISBN,L.Titre,em.Date_D,em.Date_F,em.Rendu from Adherant A,Emprunt em,Examplaire ex,Livre L where em.Id_Adherant=A.Id_Adherant and ex.ISBN=L.ISBN and em.Id_Examp=ex.Id_Examp";
            cmd.Connection = cnx;
            cnx.Open();
            DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                dataGridView1.Rows.Add(DR[0], DR[1], DR[2], DR[3], DR[4],DR[5],DR[6],DR[7],DR[8],DR[9]);
            }

            DR.Close();
            cnx.Close();
        }
        public void combobox1()
        {
            comboBox1.Items.Clear();
            SqlCommand cmd = new SqlCommand("select Id_Emprunt from  Emprunt ", cnx);
            cnx.Open();
            SqlDataReader DR = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            while (DR.Read())
            {
                comboBox1.Items.Add(DR[0].ToString());
            }
            DR.Close();
            cnx.Close();

        }
        

        private void Form7_Load(object sender, EventArgs e)
        {
            actualiser_DataGrid();
            combobox1();
            comboBox2.Items.Add("Oui");
            comboBox2.Items.Add("Non");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("select * from Emprunt where Id_Examp=" + textBox3.Text+ " and rendu='Non'", cnx);
                cmd.Connection = cnx;
                DR = cmd.ExecuteReader();

                if (DR.HasRows)
                {
                    MessageBox.Show("Livre déjà sorti");
                }
                else
                {
                    DR.Close();
                    cmd = new SqlCommand("insert into Emprunt values(" + textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + ",'" + Date_Debut.Value + "','" + Date_Retour.Value + "','" + comboBox2.Text + "')", cnx);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Livre bien emprunter");
                }
            }
            catch(SqlException eX)
            {
                MessageBox.Show(eX.Message);
            }
            cnx.Close();
            actualiser_DataGrid();
            combobox1();
           
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            try
            { 
            cnx.Open();
            SqlCommand cmd = new SqlCommand("update Emprunt set Id_Adherant=" + textBox2.Text + ",Id_Examp=" + textBox3.Text + ",Date_D='" + Date_Debut.Value + "', Date_F='" + Date_Retour.Value + "',Rendu='"+comboBox2.Text+"' where Id_Emprunt= " + comboBox1.Text + "", cnx);
            cmd.ExecuteReader();
            MessageBox.Show("Emprunt effectué!");
               cnx.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            actualiser_DataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("delete from Emprunt where Id_Emprunt=" + comboBox1.Text, cnx);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Emprunt bien supprimer");
            cnx.Close();
            //combobox2();
            actualiser_DataGrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
            position = 0;
            this.dataGridView1.Rows[position].Selected = true;
            naviger();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.dataGridView1.ClearSelection();
                position = this.dataGridView1.Rows.Count - 1;
                this.dataGridView1.Rows[position].Selected = true;
                naviger();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                this.dataGridView1.ClearSelection();
                if (position < dataGridView1.RowCount) position++;
                else position = 0; naviger();
                this.dataGridView1.Rows[position].Selected = true;
            }
            catch
            {
                cnx.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                this.dataGridView1.ClearSelection();
                if (position >= 0) position--;
                else position = dataGridView1.RowCount - 1; naviger();
                this.dataGridView1.Rows[position].Selected = true;

            }
            catch
            {
                cnx.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("select * from Emprunt where Id_Emprunt=" + comboBox1.Text + "", cnx);
                cmd.Connection = cnx;
                DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    textBox1.Text = DR[0].ToString();
                    textBox2.Text = DR[1].ToString();
                    textBox3.Text = DR[2].ToString();
                    Date_Debut.Text = DR[3].ToString();
                    Date_Retour.Text = DR[4].ToString();
                    comboBox2.Text = DR[5].ToString();
                    comboBox1.Text = DR[0].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnx.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            parent.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
         //   cnx.Open();
           // SqlCommand cmd = new SqlCommand("select count(*)")
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            I= int.Parse(comboBox1.Text);
            Form9 f = new Form9();
            f.Show();
        }
    }
    
}
