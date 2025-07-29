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
    public partial class Form10 : Form
    {
        SqlConnection cnx = new SqlConnection("server=HTM-HP;database=Gest_biblio;integrated security=true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader DR;
        string typeUser;
        Form parent = null;

        public Form10(string typeUser, Form p)
        {
            InitializeComponent();
            this.typeUser = typeUser;
            this.parent = p;
            textBox2.UseSystemPasswordChar = true;


        }

        int position;
        public void naviger()
        {
            cmd.Connection = cnx;
            cmd.CommandText = "select * from [User]";
            cnx.Open();
            DR = cmd.ExecuteReader();
            for (int i = 0; i <= position; i++)
            {
                DR.Read();
            }
            textBox1.Text = DR[0].ToString();
            textBox2.Text = DR[1].ToString();
            textBox3.Text = DR[2].ToString();
            textBox4.Text = DR[3].ToString();
            Date_Naiss.Text = DR[4].ToString();
            textBox5.Text = DR[5].ToString();
            textBox6.Text = DR[6].ToString();
            comboBox2.Text = DR[7].ToString();
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
            textBox6.Clear();
            cmd.CommandText = "select * from [User]";
            cmd.Connection = cnx;
            cnx.Open();
            DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                dataGridView1.Rows.Add(DR[0], DR[1], DR[2], DR[3], DR[4], DR[5], DR[6],DR[7]);
            }

            DR.Close();
            cnx.Close();
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Password";
                textBox2.ForeColor = Color.Silver;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.ForeColor = Color.Black;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;

            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("insert into [User] values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + Date_Naiss.Value + "','" + textBox5.Text + "',"+textBox6.Text+",'" + comboBox2.Text+ "')", cnx);
                cmd.ExecuteReader();
                MessageBox.Show("User bien ajouter ");
            }
            catch
            {
                MessageBox.Show("L id de ce User existe deja");
            }
            cnx.Close();
            actualiser_DataGrid();
            combobox1();
        }
        public void combobox1()
        {
            comboBox1.Items.Clear();
            SqlCommand cmd = new SqlCommand("select IdUser from [User] ", cnx);
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
            comboBox2.Items.Add("Administrateur");
            comboBox2.Items.Add("User");
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            actualiser_DataGrid();
            combobox1();
            combobox2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("update [User] set IdUser='"+textBox1.Text+"',PasswordUser='"+textBox2.Text+"',NomU='" + textBox3.Text + "',PrenomU='" + textBox4.Text + "',Date_Naiss='" + Date_Naiss.Value + "', Adresse='" + textBox5.Text + "',Nmr_Tel=" + textBox6.Text + ",[Type]='" +comboBox2.Text+ "'where IdUser= '" + comboBox1.Text + "'", cnx);
            cmd.ExecuteReader();
            MessageBox.Show("User bien modifier");
            cnx.Close();
            actualiser_DataGrid();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("select * from [User] where IdUser='" + comboBox1.Text + "'", cnx);
                cmd.Connection = cnx;
                DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    textBox1.Text = DR[0].ToString();
                    textBox2.Text = DR[1].ToString();
                    textBox3.Text = DR[2].ToString();
                    textBox4.Text = DR[3].ToString();
                    Date_Naiss.Text = DR[4].ToString();
                    textBox5.Text = DR[5].ToString();
                    textBox6.Text = DR[6].ToString();
                    comboBox2.Text = DR[7].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            cnx.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("delete from [User] where IdUser='" + comboBox1.Text+"'",cnx);
            cmd.ExecuteNonQuery();
            MessageBox.Show("User bien supprimer");
            cnx.Close();
            combobox1();
            actualiser_DataGrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.dataGridView1.ClearSelection();
                position = 0;
                this.dataGridView1.Rows[position].Selected = true;
                naviger();
            }
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

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            parent.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
