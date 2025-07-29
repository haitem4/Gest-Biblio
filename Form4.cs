using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Gest_Biblio
{
    public partial class Form4 : Form
    {
        SqlConnection cnx = new SqlConnection("server=HTM-HP;database=Gest_biblio;integrated security=true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader DR;
        string typeUser;
        Form parent = null;

        public Form4(string typeUser, Form p)
        {
            InitializeComponent();
            this.typeUser = typeUser;
            this.parent = p;
        }
        int position;
        public void naviger()
        {
            cmd.Connection = cnx;
            cmd.CommandText = "select *from Livre";
            cnx.Open();
            DR = cmd.ExecuteReader();
            for (int i = 0; i <= position; i++)
            {
                DR.Read();
            }
            comboBox1.Text = DR[0].ToString();
            textBox1.Text = DR[1].ToString();
            textBox2.Text = DR[2].ToString();
            textBox3.Text = DR[3].ToString();
            textBox4.Text = DR[4].ToString();
            Date_achat.Text = DR[5].ToString();
            Date_Parution.Text = DR[6].ToString();
            comboBox3.Text = DR[7].ToString();
            DR.Close();
            cnx.Close();

        }
        public void actualiser_DataGrid()
        {

            dataGridView1.Rows.Clear();
          
            cmd.CommandText = "select ISBN,Titre,Langue_Livre,Annee_Pub,Nbr_Pages,Date_achat,Date_Parution,A.NomA,A.PrenomA,T.NomTh from Livre L,Auteur A,Theme T where L.Id_Auteur=A.Id_Auteur and T.Id_Theme=L.Id_Theme ";
            cmd.Connection = cnx;
            cnx.Open();
            DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                dataGridView1.Rows.Add(DR[0], DR[1], DR[2], DR[3], DR[4], DR[5], DR[6],DR[7]+" "+DR[8], DR[9]);
            }

            DR.Close();
            cnx.Close();
        }
       
        private void label7_Click(object sender, EventArgs e)
        {

        }
        public void combobox()
        {
            SqlCommand cmd = new SqlCommand("select * from Livre", cnx);
            cnx.Open();
            SqlDataReader DR = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            while (DR.Read())

                comboBox1.Items.Add(DR[0].ToString());

            DR.Close();
            cnx.Close();
        }
        public void combobox2()
        {
            SqlCommand cmd = new SqlCommand("select ISBN from Livre", cnx);
            cnx.Open();
            SqlDataReader DR = cmd.ExecuteReader();
            comboBox2.Items.Clear();
            while (DR.Read())

                comboBox2.Items.Add(DR[0].ToString());

            DR.Close();
            cnx.Close();
        }
        public void combobox3()
        {
            SqlCommand cmd = new SqlCommand("select * from Auteur", cnx);
            cnx.Open();
            SqlDataReader DR = cmd.ExecuteReader();
            comboBox3.Items.Clear();
            
            while (DR.Read())

                comboBox3.Items.Add(DR[0].ToString());
      

            DR.Close();
            cnx.Close();
        }
        public void combobox4()
        {
            SqlCommand cmd = new SqlCommand("select * from theme", cnx);
            cnx.Open();
            SqlDataReader DR = cmd.ExecuteReader();
            comboBox4.Items.Clear();
            while (DR.Read())

                comboBox4.Items.Add(DR[0].ToString());

            DR.Close();
            cnx.Close();
        }
        private void Form4_Load(object sender, EventArgs e)
        {

            //====================================Remplissage datagrid =======================================
            //comboBox1.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            //comboBox3.Items.Clear();
            //  comboBox2.Items.Clear();
            actualiser_DataGrid();
            //====================================Remplissage du combobox=======================================
            combobox();
            combobox2();
            combobox3();
            combobox4();


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("insert into  Livre (ISBN,Titre,Langue_Livre,Annee_Pub,Nbr_Pages,Date_achat,Date_Parution,Id_Auteur) values(" + comboBox1.Text + ",'" + textBox1.Text + "','" + textBox2.Text + "'," + textBox3.Text + "," + textBox4.Text + ",'" + Date_achat.Value + "','" + Date_Parution.Value + "'," + comboBox3.Text + ")", cnx);
                cmd.Connection = cnx;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Livre bien ajouter ");
            }
            catch
            {
                MessageBox.Show("L id de ce livre  existe deja");
            }
            cnx.Close();
            actualiser_DataGrid();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cnx.Open();
           
            SqlCommand cmd = new SqlCommand("update Livre set Titre='" + textBox1.Text + "',Langue_Livre='" + textBox2.Text + "',Annee_Pub=" + textBox3.Text + ",Nbr_Pages=" + textBox4.Text + ",Date_achat='" + Date_achat.Value + "',Date_Parution='" + Date_Parution.Value + "',Id_Auteur=" + comboBox3.Text + " , id_theme="+comboBox4.Text+" where ISBN=" + comboBox2.Text + "", cnx);
            cmd.ExecuteReader();
            MessageBox.Show("Livre bien modifier");
            cnx.Close();
            actualiser_DataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("delete from Livre where ISBN=" + comboBox2.Text, cnx);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Livre bien supprimer");
            cnx.Close();
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
                else position = 0;
                naviger();
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
                else position = dataGridView1.RowCount - 1;
                naviger();
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
                SqlCommand cmd = new SqlCommand("select * from livre where ISBN=" + comboBox2.Text + "", cnx);
                cmd.Connection = cnx;
                DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    comboBox1.Text = DR[0].ToString();
                    textBox1.Text = DR[1].ToString();
                    textBox2.Text = DR[2].ToString();
                    textBox3.Text = DR[3].ToString();
                    textBox4.Text = DR[4].ToString();
                    Date_achat.Text = DR[5].ToString();
                    Date_Parution.Text = DR[6].ToString();
                    comboBox3.Text = DR[7].ToString();
                    comboBox4.Text = DR[8].ToString();

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

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
