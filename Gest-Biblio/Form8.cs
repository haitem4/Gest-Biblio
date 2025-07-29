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
    public partial class Form8 : Form
    {
        SqlConnection cnx = new SqlConnection("server=HTM-HP;database=Gest_biblio;integrated security=true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader DR;
        string typeUser;
        Form parent = null;
        public Form8(string typeUser,Form p)
        {
            InitializeComponent();
            this.typeUser = typeUser;
            this.parent = p;

        }
        int position;
        public void naviger()
        {
            cmd.Connection = cnx;
            cmd.CommandText = "select *from Examplaire";
            cnx.Open();
            DR = cmd.ExecuteReader();
            for (int i = 0; i <= position; i++)
            {
                DR.Read();
            }
            textBox1.Text = DR[0].ToString();
            textBox2.Text = DR[1].ToString();
            comboBox2.Text = DR[2].ToString();
            comboBox1.Text = DR[0].ToString();
            DR.Close();
            cnx.Close();

        }
        public void actualiser_DataGrid()
        {

            dataGridView1.Rows.Clear();
            // comboBox1.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            //comboBox2.Items.Clear();
           //  comboBox2.Items.Clear();
            cmd.CommandText = "select ex.Id_Examp,ex.ISBN,L.Titre,ex.Etat from  Examplaire ex,Livre L where ex.ISBN=L.ISBN";
            cmd.Connection = cnx;
            cnx.Open();
            DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                dataGridView1.Rows.Add(DR[0], DR[1], DR[2], DR[3]);
            }

            DR.Close();
            cnx.Close();
        }
        public void combobox1()
        {
            comboBox1.Items.Clear();
            SqlCommand cmd = new SqlCommand("select Id_Examp  from  Examplaire ", cnx);
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
        
        private void Form8_Load(object sender, EventArgs e)
        {
            
            actualiser_DataGrid();
            comboBox2.Items.Add("Neuf");
            comboBox2.Items.Add("comme neuf");
            comboBox2.Items.Add("Excellent état");
            comboBox2.Items.Add("Très bon état");
            comboBox2.Items.Add("Bon état");
            comboBox2.Items.Add("Etat acceptable");
            comboBox2.Items.Add("Etat d’usage");
            combobox1();


           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("insert into Examplaire values(" + textBox1.Text + "," + textBox2.Text + ",'" + comboBox2.Text + "')", cnx);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Livre bien  Emprunter ");
            }
            catch
            {
                MessageBox.Show("Livre deja emprunter");
            }
            cnx.Close();
            actualiser_DataGrid();
            combobox1();
            //combobox2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("update Examplaire set ISBN=" + textBox2.Text + ",Etat='" + comboBox2.Text +"'where Id_Examp= " + comboBox1.Text + "", cnx);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Livre bien modifier");
            cnx.Close();
            actualiser_DataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            cnx.Open();
            SqlCommand cmd = new SqlCommand("delete from Examplaire where Id_Examp=" + comboBox1.Text, cnx);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Livre bien supprimer");
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
                SqlCommand cmd = new SqlCommand("select * from Examplaire where Id_Examp=" + comboBox1.Text + "", cnx);
                cmd.Connection = cnx;
                DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    textBox1.Text = DR[0].ToString();
                    textBox2.Text = DR[1].ToString();
                    comboBox2.Text = DR[2].ToString();
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
    }
}
