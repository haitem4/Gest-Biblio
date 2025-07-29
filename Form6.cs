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
    public partial class Form6 : Form
    {
        SqlConnection cnx = new SqlConnection("server=HTM-HP;database=Gest_biblio;integrated security=true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader DR;
        string typeUser;
        Form parent = null;
        public Form6(string typeUser, Form p)
        {
            InitializeComponent();
            this.typeUser = typeUser;
            this.parent = p;

        }
        public Form6()
        {
            InitializeComponent();
          

        }
        int position;
        public void naviger()
        {
            cmd.Connection = cnx;
            cmd.CommandText = "select * from Adherant";
            cnx.Open();
            DR = cmd.ExecuteReader();
            for (int i = 0; i <= position; i++)
            {
                DR.Read();
            }
            textBox1.Text = DR[0].ToString();
            textBox2.Text = DR[1].ToString();
            textBox3.Text = DR[2].ToString();
            Date_Naiss.Text = DR[3].ToString();
            textBox4.Text = DR[4].ToString();
            textBox5.Text = DR[5].ToString();
            Date_Inscri.Text = DR[6].ToString();
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
            textBox4.Clear();
            textBox5.Clear();
            cmd.CommandText = "select * from Adherant";
            cmd.Connection = cnx;
            cnx.Open();
            DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                dataGridView1.Rows.Add(DR[0], DR[1], DR[2], DR[3], DR[4],DR[5],DR[6]);
            }

            DR.Close();
            cnx.Close();
        }
        public void combobox1()
        {
            comboBox1.Items.Clear();
            SqlCommand cmd = new SqlCommand("select Id_Adherant from  Adherant ", cnx);
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
        private void Form6_Load(object sender, System.EventArgs e)
        {
            actualiser_DataGrid();
            combobox1();

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("insert into Adherant values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','"+Date_Naiss.Value+"','" + textBox4.Text+ "',"+textBox5.Text+",'"+Date_Inscri.Value+"')", cnx);
                cmd.ExecuteReader();
                MessageBox.Show("Adherent bien ajouter ");
            }
            catch
            {
                MessageBox.Show("L id de cet Adherent existe deja");
            }
            cnx.Close();
            actualiser_DataGrid();
            combobox1();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("update Adherant set Nom_Ad='" + textBox2.Text + "',Prenom_Ad='" + textBox3.Text + "',Date_Naiss='" + Date_Naiss.Value +"', Adresse='"+textBox4.Text+ "',Nmr_Tel="+textBox5.Text+ ",Date_inscri='"+Date_Inscri.Value+"'where Id_Adherant= " + comboBox1.Text + "", cnx);
            cmd.ExecuteReader();
            MessageBox.Show("Adherent bien modifier");
            cnx.Close();
            actualiser_DataGrid();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("delete from Adherant where Id_Adherant=" + comboBox1.Text, cnx);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Adherent bien supprimer");
            cnx.Close();
            combobox1();
            actualiser_DataGrid();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            
                this.dataGridView1.ClearSelection();
                position = 0;
                this.dataGridView1.Rows[position].Selected = true;
                naviger();
            
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.dataGridView1.ClearSelection();
                position = this.dataGridView1.Rows.Count - 1;
                this.dataGridView1.Rows[position].Selected = true;
                naviger();
            }
        }

        private void button6_Click(object sender, System.EventArgs e)
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

        private void button7_Click(object sender, System.EventArgs e)
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

        private void button8_Click(object sender, System.EventArgs e)
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("select * from Adherant where Id_Adherant=" + comboBox1.Text + "", cnx);
                cmd.Connection = cnx;
                DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    textBox1.Text = DR[0].ToString();
                    textBox2.Text = DR[1].ToString();
                    textBox3.Text = DR[2].ToString();
                    Date_Naiss.Text = DR[3].ToString();
                    textBox4.Text = DR[4].ToString();
                    textBox5.Text = DR[5].ToString();
                    Date_Inscri.Text = DR[6].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            cnx.Close();
        }

        private void button9_Click(object sender, System.EventArgs e)
        {

            this.Hide();
            if (parent!=null)
            parent.Show();
        }
    }
}
