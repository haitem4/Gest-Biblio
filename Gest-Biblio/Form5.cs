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
    public partial class Form5 : Form
    {
        SqlConnection cnx = new SqlConnection("server=HTM-HP;database=Gest_biblio;integrated security=true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader DR;
        string typeUser;
        Form parent = null;
        public Form5(string typeUser, Form p)
        {
            InitializeComponent();
            this.typeUser = typeUser;
            this.parent = p;

        }

        int position;
        public void naviger()
        {
            cmd.Connection = cnx;
            cmd.CommandText = "select *from Theme";
            cnx.Open();
            DR = cmd.ExecuteReader();
            for (int i = 0; i <= position; i++)
            {
                DR.Read();
            }
            textBox1.Text = DR[0].ToString();
            textBox2.Text = DR[1].ToString();
            textBox3.Text = DR[2].ToString();
          
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
            cmd.CommandText = "select th.Id_Theme,th.NomTh,th.Libelle from Theme th";
            cmd.Connection = cnx;
            cnx.Open();
            DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                dataGridView1.Rows.Add(DR[0], DR[1], DR[2]);
            }

            DR.Close();
            cnx.Close();
        }
       
        public void combobox2()
        {
            comboBox2.Items.Clear();
            SqlCommand cmd = new SqlCommand("select Id_Theme from Theme ", cnx);
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
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            actualiser_DataGrid();
            combobox2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("insert into Theme values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')", cnx);
                cmd.ExecuteReader();
                MessageBox.Show("Livre bien ajouter ");
            }
            catch
            {
                MessageBox.Show("L id de ce livre  existe deja");
            }
            cnx.Close();
            actualiser_DataGrid();
            combobox2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("update Theme set NomTh='" + textBox2.Text + "',Libelle='" + textBox3.Text + "'where Id_Theme= " + textBox1.Text + "", cnx);
            cmd.ExecuteReader();
            MessageBox.Show("Livre bien modifier");
            cnx.Close();
            actualiser_DataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("delete from Theme where Id_Theme=" + comboBox2.Text, cnx);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Livre bien supprimer");
            cnx.Close();
            combobox2();
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
                SqlCommand cmd = new SqlCommand("select * from Theme where Id_Theme=" + comboBox2.Text + "", cnx);
                cmd.Connection = cnx;
                DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    textBox1.Text = DR[0].ToString();
                    textBox2.Text = DR[1].ToString();
                    textBox3.Text = DR[2].ToString();
                  
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
