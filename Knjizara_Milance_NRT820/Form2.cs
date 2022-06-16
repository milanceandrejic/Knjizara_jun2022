using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knjizara_Milance_NRT820
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'knjizaraDataSet.Racun' table. You can move, or remove it, as needed.
            this.racunTableAdapter.Fill(this.knjizaraDataSet.Racun);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = racunBindingSource;
            if (dataGridView1.Rows.Count != 0)
                dataGridView1.CurrentRow.Selected = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = racunBindingSource;
            if (dataGridView1.Rows.Count != 0)
                dataGridView1.CurrentRow.Selected = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
                
            try
            {
                db.OpenConnection();
                string sql = "SELECT * FROM Racun WHERE datum BETWEEN #" + dateTimePicker1.Value.Date + "# AND #" + dateTimePicker2.Value.Date + "#;";
                System.Diagnostics.Debug.WriteLine(sql);

                OleDbCommand command = new OleDbCommand();
                command.Connection = db.Connection;
                command.CommandText = sql;

                OleDbDataReader reader = command.ExecuteReader();
                List<Racun> filtrirani = new List<Racun>();

                while (reader.Read())
                {
                    Racun k = new Racun();

                    k.Id_racun = int.Parse(reader["id_racun"].ToString());
                    k.Ukupna_cena = double.Parse(reader["ukupna_cena"].ToString());
                    k.Datum = DateTime.Parse(reader["datum"].ToString());

                    filtrirani.Add(k);
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = filtrirani;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine("Exception caught");
            }
            finally
            {
                db.CloseConnection();
            }
            if(dataGridView1.Rows.Count!=0)
                dataGridView1.CurrentRow.Selected = false;

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddDays(1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            prikaziRacun();
            if (dataGridView1.Rows.Count != 0)
                dataGridView1.CurrentRow.Selected = true;
            if (dataGridView2.Rows.Count != 0)
                dataGridView2.CurrentRow.Selected = false;
        }

        private void prikaziRacun()
        {
            DataBase db = new DataBase();
            int izabranID = -1;
            try
            {
                int ri = dataGridView1.CurrentRow.Index;
                izabranID = int.Parse(dataGridView1.Rows[ri].Cells[0].Value.ToString());
                db.OpenConnection();
                string sql = "SELECT * FROM Stavka_racuna WHERE id_racun = " + izabranID + ";";
                System.Diagnostics.Debug.WriteLine(sql);

                OleDbCommand command = new OleDbCommand();
                command.Connection = db.Connection;
                command.CommandText = sql;

                OleDbDataReader reader = command.ExecuteReader();
                List<Stavka_Racun> izabrani = new List<Stavka_Racun>();

                while (reader.Read())
                {
                    Stavka_Racun s = new Stavka_Racun();

                    //s.Naziv = reader["naziv"].ToString();
                    s.Id_knjiga = int.Parse(reader["id_knjiga"].ToString());
                    s.Id_racun = int.Parse(reader["id_racun"].ToString());
                    s.Cena = double.Parse(reader["cena"].ToString());
                    //s.Kolicina = s.Cena/;
                    s.Popust = double.Parse(reader["popust"].ToString());
                    s.Kolicina = int.Parse(reader["kolicina"].ToString());

                    izabrani.Add(s);
                }
                reader.Close();

                //Ucitavanje knjiga
                sql = "SELECT * FROM Knjiga;";

                command.CommandText = sql;

                reader = command.ExecuteReader();
                List<Knjiga> knjige = new List<Knjiga>();

                while (reader.Read())
                {
                    Knjiga k = new Knjiga();

                    k.Id_knjiga = int.Parse(reader["id_knjiga"].ToString());
                    k.Broj_strana = int.Parse(reader["broj_strana"].ToString());
                    k.Cena = double.Parse(reader["cena"].ToString());
                    k.Popust = double.Parse(reader["popust"].ToString());
                    k.Naziv = reader["naziv"].ToString();
                    k.Autor = reader["autor"].ToString();

                    knjige.Add(k);
                }

                foreach (Stavka_Racun s in izabrani)
                {
                    foreach(Knjiga k in knjige)
                    {
                        if(s.Id_knjiga==k.Id_knjiga)
                        {
                            s.Naziv = k.Naziv;
                            break;
                        }
                    }
                }

                dataGridView2.DataSource = null;
                dataGridView2.DataSource = izabrani;
                dataGridView2.Columns[1].Visible = false;
                dataGridView2.Columns[2].Visible = false;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine("Exception caught");
            }
            finally
            {
                db.CloseConnection();
            }
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CurrentRow.Selected = true;
        }
    }
}
