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
using System.Text.RegularExpressions;

namespace RadSaBazom
{
    public partial class Form1 : Form
    {
        string connection = @"Data Source=DESKTOP-P6KBOAK\VUKADINOVIC;Initial Catalog=Kontakt;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            prikazi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    if (textBox3.Text != "")
                    {
                        if (textBox4.Text != "")
                        {
                            if (textBox5.Text != "")
                            {
                                try
                                {
                                    SqlConnection con = new SqlConnection(connection);
                                    con.Open();
                                    SqlCommand sc = new SqlCommand("INSERT INTO Kontakt(Id,Ime,Prezime,Email,Kontakt_Broj) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')", con);
                                    int i = sc.ExecuteNonQuery();
                                    prikazi();
                                    if (i >= 1)
                                        MessageBox.Show(" Uspesno !!!");
                                    else
                                        MessageBox.Show(" Neuspesno ");

                                    con.Close();

                                }
                                catch (System.Exception exp)
                                {
                                    MessageBox.Show(" Error is  " + exp.ToString());

                                }
                            }
                            else
                                MessageBox.Show("Popunite sva polja !!!");
                        }
                        else
                            MessageBox.Show("Popunite sva polja !!!");
                    }
                    else
                        MessageBox.Show("Popunite sva polja !!!");
                }
                else
                    MessageBox.Show("Popunite sva polja !!!");
            }
            else
                MessageBox.Show("Popunite sva polja !!!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection(connection);
                    con.Open();
                    SqlCommand cmdD = new SqlCommand("delete from Kontakt where Id='" + textBox1.Text + "'", con);
                    int i = cmdD.ExecuteNonQuery();
                    prikazi();
                    
                    if (i >= 1)
                        MessageBox.Show(" Kontakt je obrisan, ID= " + textBox1.Text);
                    else
                        MessageBox.Show(" Nije uspesno brisanje!!! ");
                    dataGridView1.Refresh();
                    dataGridView1.Update();
                    textBox1.Text = "";
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
                MessageBox.Show("Unesite Id kontakta !!!");

        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand sc = new SqlCommand("Update Kontakt set Ime='"+textBox2.Text+"',Prezime='"+textBox3.Text+"',Email='"+textBox4.Text+"',Kontakt_Broj='"+textBox5.Text+"' where Id='"+textBox1.Text+"'", con);
                int i = sc.ExecuteNonQuery();
                prikazi();
                if (i >= 1)
                    MessageBox.Show("Uspesno !!!");
                else
                    MessageBox.Show("Neuspesno");
                con.Close();
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(" Error is  " + exp.ToString());

            }
        }
        private void textBox4_Leave(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(textBox4.Text, pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(this.textBox4, "Unesite pravilnu email adresu");
            }
        }
        void prikazi()
        {
            SqlConnection sqlcon = new SqlConnection(connection);
            sqlcon.Open();
            SqlDataAdapter sqld = new SqlDataAdapter("Select * From Kontakt", sqlcon);
            DataTable dtbl = new DataTable();
            sqld.Fill(dtbl);

            dataGridView1.DataSource = dtbl;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }
        void prikazi2()
        {
            SqlConnection sqlcon = new SqlConnection(connection);
            sqlcon.Open();
            SqlDataAdapter sqld = new SqlDataAdapter("Select * From Kontakt where Id='"+textBox1.Text+"'", sqlcon);
            DataTable dtbl = new DataTable();
            sqld.Fill(dtbl);

            dataGridView1.DataSource = dtbl;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            prikazi2();
        }
    }
}
