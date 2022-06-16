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
    public partial class Form3 : Form
    {
        Form1 caller;
        public Form3(Form1 form)
        {
            InitializeComponent();
            caller = form;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'knjizaraDataSet.Zanr' table. You can move, or remove it, as needed.
            this.zanrTableAdapter.Fill(this.knjizaraDataSet.Zanr);
            // TODO: This line of code loads data into the 'knjizaraDataSet.Knjiga' table. You can move, or remove it, as needed.
            this.knjigaTableAdapter.Fill(this.knjizaraDataSet.Knjiga);
            napuniZanrove(listBox1);
            listBox1.ValueMember = "id";
            listBox1.DisplayMember = "naziv";
            listBox1.SelectedItem = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0)
                MessageBox.Show("Izaberite barem jedan zanr","Greska!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            else
            {
                if(textBox1.Text.Trim() != "" &&
                    textBox2.Text.Trim() != "" &&
                    double.TryParse(textBox3.Text.Trim(),out double cena) &&
                    double.TryParse(textBox4.Text.Trim(), out double popust) &&
                    int.TryParse(textBox5.Text.Trim(), out int br_strana)
                    )
                {
                    dodajKnjigu(textBox1.Text.Trim(), textBox2.Text.Trim(), cena, popust, br_strana);
                }
                else
                    MessageBox.Show("Unesite sve podatke validno!", "Greska!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dodajKnjigu(string autor, string naziv, double cena, double popust, int brStrana)
        {
            DataBase db = new DataBase();
            OleDbTransaction transaction = null;

            try
            {
                string val = "";
                string sql = "";

                db.OpenConnection();
                transaction = db.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
                OleDbCommand command = new OleDbCommand();
                command.Connection = db.Connection;
                command.Transaction = transaction;

                //Prvo u transakciji dodavanje knjige
                val = "('" + autor + "', '" + naziv + "', " + cena + "," + popust + "," + brStrana + ")";
                sql = "INSERT INTO Knjiga(autor, naziv, cena, popust, broj_strana) VALUES" + val + ";";
                command.CommandText = sql;
                command.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("Dodata knjiga");

                //Prihvatanje Id knjige
                int idKnjiga = -1;
                command.CommandText = "SELECT MAX(id_knjiga) FROM Knjiga";
                idKnjiga = (int)command.ExecuteScalar();
                System.Diagnostics.Debug.WriteLine("id knjiga " + idKnjiga);

                //Drugo u transakciji dodavanje pripadnosti
                foreach (var id in listBox1.SelectedItems)
                {
                    Zanr z = (Zanr)id;

                    System.Diagnostics.Debug.WriteLine("id evi " + z.Id.ToString());
                    val = "(" + idKnjiga + ", " + z.Id + ")";
                    sql = "INSERT INTO Pripadnost(id_knjiga, id_zanr) VALUES " + val + ";";

                    command.CommandText = sql;
                    command.ExecuteNonQuery();

                }
                transaction.Commit();
                System.Diagnostics.Debug.WriteLine("Successfull write to database");
                this.Close();
                MessageBox.Show("Uspesno dodata knjiga!");
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine("Uradjen Rollback");
                }
                catch
                { }
            }
            finally
            {
                db.CloseConnection();
            }
        }
        
        private void napuniZanrove(ListBox lb)
        {
            List<Zanr> zanrovi = new List<Zanr>();

            DataBase db = new DataBase();

            try
            {
                string sql = "";

                db.OpenConnection();
                OleDbCommand command = new OleDbCommand();
                command.Connection = db.Connection;

                //Prvo u transakciji dodavanje knjige          
                sql = "SELECT * from Zanr;";
                command.CommandText = sql;
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Zanr zanr;

                    String naziv = reader["naziv"].ToString();
                    int id = int.Parse(reader["id_zanr"].ToString());

                    zanr = new Zanr(id, naziv);
                    zanrovi.Add(zanr);
                }

            }
            catch (Exception ex)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                catch
                { }
            }
            finally
            {
                db.CloseConnection();
            }

            lb.DataSource = null;
            lb.DataSource = zanrovi;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            caller.osveziData1();
        }
    }
}
