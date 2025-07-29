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
    public partial class Form3 : Form
    {
        SqlConnection cnx = new SqlConnection("server=HTM-HP;database=Gest_biblio;integrated security=true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader DR;
        string typeUser;
        Form parent = null;
        public Form3(string typeUser, Form p)
        {
            InitializeComponent();
            this.typeUser = typeUser;
            this.parent = p;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("insert into  Auteur values(" + comboBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','" + date_naiss.Value + "','" + textBox4.Text + "')", cnx);
                cmd.Connection = cnx;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Auteur bien ajouter ");
            }
            catch
            {
                MessageBox.Show("L id de cet auteur existe deja");
            }
            cnx.Close();
            actualiser_DataGrid();
            combobox1();
            combobox2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand(" update Auteur set NomA='" + textBox2.Text + "',PrenomA='" + textBox3.Text + "',Date_Naiss='" + date_naiss.Value + "',Nationalite='" + textBox4.Text + "'where Id_Auteur=" + comboBox2.Text + "", cnx);
            cmd.Connection = cnx;
            cmd.ExecuteNonQuery();
            MessageBox.Show("auteur bien modifier");
            cnx.Close();
            actualiser_DataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete from Auteur where Id_Auteur=" + comboBox2.Text, cnx);
            cnx.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Auteur bien Supprimer");
            cnx.Close();
            actualiser_DataGrid();
            combobox1();
            combobox2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
            position = 0;
            this.dataGridView1.Rows[position].Selected = true;
            naviger();
        }

        public void combobox1()
        {
            SqlCommand cmd = new SqlCommand("select* from Auteur", cnx);
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
        public void combobox2()
        {
            SqlCommand cmd = new SqlCommand("select Id_Auteur from Auteur", cnx);
            cnx.Open();
            SqlDataReader DR = cmd.ExecuteReader();
            comboBox2.Items.Clear();
            while (DR.Read())
            {
                comboBox2.Items.Add(DR[0].ToString());
            }
            DR.Close();
            cnx.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            actualiser_DataGrid();
            //====================================Remplissage du combobox=======================================
            combobox1();
            //====================================Remplissage du combobox=======================================
            combobox2();


        }
        int position;
        public void naviger()
        {
            cmd.Connection = cnx;
            cmd.CommandText = "select *from Auteur";
            cnx.Open();
            DR = cmd.ExecuteReader();
            for (int i = 0; i <= position; i++)
            {
                DR.Read();
            }
            comboBox1.Text = DR[0].ToString();
            textBox2.Text = DR[1].ToString();
            textBox3.Text = DR[2].ToString();
            date_naiss.Text = DR[3].ToString();
            textBox4.Text = DR[4].ToString();
            DR.Close();
            cnx.Close();

        }
        public void actualiser_DataGrid()
        {
            dataGridView1.Rows.Clear();
            // comboBox1.Items.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            //comboBox2.Items.Clear();
            cmd.CommandText = "select * from Auteur";
            cmd.Connection = cnx;
            cnx.Open();
            DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                dataGridView1.Rows.Add(DR[0], DR[1], DR[2], DR[3], DR[4]);
            }

            DR.Close();
            cnx.Close();
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
                SqlCommand cmd = new SqlCommand("select * from Auteur where Id_Auteur=" + comboBox2.Text + "", cnx);
                cmd.Connection = cnx;
                DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    comboBox1.Text = DR[0].ToString();
                    textBox2.Text = DR[1].ToString();
                    textBox3.Text = DR[2].ToString();
                    date_naiss.Text = DR[3].ToString();
                    textBox4.Text = DR[4].ToString();
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
