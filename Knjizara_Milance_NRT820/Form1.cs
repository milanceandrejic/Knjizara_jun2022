using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Knjizara_Milance_NRT820
{
    //DODAJ STATIC/ ENVIREMNT VARIABLES
    public partial class Form1 : Form
    {
        public static string CONNECTION_STRING = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\milan\source\repos\Knjizara.accdb";
        List<Stavka_Racun> racun;
        Thread t;
        Point p;
        Size v;
        public Form1()
        {
            InitializeComponent();
            racun = new List<Stavka_Racun>();

            initVariables();

            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(label2.Left + label2.Width > 0)
            {
                label2.Left = p.X;

            }
            else
            {
                label2.Left = ClientSize.Width;
            }
        }

        private void initVariables()
        {
            label2.Left = ClientSize.Width;
            p = new Point(ClientSize.Width, label2.Top);
            v = label2.Size;
        }

        private void knjigaBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.knjigaBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.knjizaraDataSet);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'knjizaraDataSet.Zanr' table. You can move, or remove it, as needed.
            this.zanrTableAdapter.Fill(this.knjizaraDataSet.Zanr);
            // TODO: This line of code loads data into the 'knjizaraDataSet.Stavka_racuna' table. You can move, or remove it, as needed.
            this.stavka_racunaTableAdapter.Fill(this.knjizaraDataSet.Stavka_racuna);
            // TODO: This line of code loads data into the 'knjizaraDataSet.Racun' table. You can move, or remove it, as needed.
            this.racunTableAdapter.Fill(this.knjizaraDataSet.Racun);
            // TODO: This line of code loads data into the 'knjizaraDataSet.Pripadnost' table. You can move, or remove it, as needed.
            this.pripadnostTableAdapter.Fill(this.knjizaraDataSet.Pripadnost);
            // TODO: This line of code loads data into the 'knjizaraDataSet.Knjiga' table. You can move, or remove it, as needed.
            this.knjigaTableAdapter.Fill(this.knjizaraDataSet.Knjiga);

            dataGridView1.Columns[0].ReadOnly = true;
            
            RefreshRacun();

            if(dataGridView1.Rows.Count!=0)
                dataGridView1.CurrentRow.Selected = false;

            ucitajNajprodavanijeKnjige();
            v = label2.Size;

            t = new Thread(animacija);
            t.IsBackground = true;
            t.Start();
        }

        //UCITAVANJE NAJPRODAVANIJIH
        string najprodavanije;
        private void ucitajNajprodavanijeKnjige()
        {
            try
            {
                DataBase db = new DataBase();
                db.OpenConnection();

                OleDbCommand command = new OleDbCommand();
                command.Connection = db.Connection;
                command.CommandText = "SELECT * FROM Knjiga;";

                OleDbDataReader reader = command.ExecuteReader();
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
                reader.Close();

                command.CommandText = "SELECT * FROM Stavka_racuna WHERE id_racun IN (SELECT id_racun FROM Racun WHERE datum BETWEEN #" + DateTime.Today + "# AND #" + DateTime.Today.AddDays(1) +"#);";
                System.Diagnostics.Debug.WriteLine(command.CommandText);
                reader = command.ExecuteReader();
                List<Stavka_Racun> stavke = new List<Stavka_Racun>();

                while (reader.Read())
                {
                    Stavka_Racun s = new Stavka_Racun();

                    s.Id_knjiga = int.Parse(reader["id_knjiga"].ToString());
                    s.Cena = double.Parse(reader["cena"].ToString());
                    s.Popust = double.Parse(reader["popust"].ToString());
                    s.Kolicina = int.Parse(reader["kolicina"].ToString());

                    stavke.Add(s);
                    System.Diagnostics.Debug.WriteLine(s.Naziv);
                }

                List<Stavka_Racun> ukupno_prodatih = new List<Stavka_Racun>();
                foreach(Knjiga k in knjige)
                {
                    int zbir = 0;
                    foreach(Stavka_Racun s in stavke)
                    {
                        if(s.Id_knjiga == k.Id_knjiga)
                        {
                            zbir += s.Kolicina;
                        }
                        System.Diagnostics.Debug.WriteLine(s.Kolicina);
                    }
                    Stavka_Racun st = new Stavka_Racun();
                    st.Naziv = k.Naziv;
                    st.Id_knjiga = k.Id_knjiga;
                    st.Kolicina = zbir;
                    ukupno_prodatih.Add(st);
                }

                for(int i = 0; i < ukupno_prodatih.Count-1; i++)
                {
                    for (int j = i; j < ukupno_prodatih.Count; j++)
                    {
                        if(ukupno_prodatih[i].Kolicina < ukupno_prodatih[j].Kolicina)
                        {
                            Stavka_Racun temp = ukupno_prodatih[i];
                            ukupno_prodatih[i] = ukupno_prodatih[j];
                            ukupno_prodatih[j] = temp;
                        }
                    }
                }

                najprodavanije = "Najprodavanije knjige danas:          ";
                for(int i = 0; i < 3; i++)
                {
                    najprodavanije += ukupno_prodatih[i].Naziv + "          ";
                }

                //najprodavanije += najprodavanije + najprodavanije;

                label2.Text = najprodavanije;

                System.Diagnostics.Debug.WriteLine(label2.Size);
                label2.Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(this.comboBox1.SelectedValue);
                System.Diagnostics.Debug.WriteLine("Exception caught");
            }
        }

        //OVO JE VISAK
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        //Primena filtera na bazu knjiga prema selektovanoj vrednosti u cmb1
        private void primeniFilter()
        {
            try
            {
                DataBase db = new DataBase();
                db.OpenConnection();

                OleDbCommand command = new OleDbCommand();
                command.Connection = db.Connection;
                command.CommandText = "SELECT * FROM Knjiga WHERE id_knjiga IN (SELECT id_knjiga FROM Pripadnost WHERE id_zanr = " + comboBox1.SelectedValue + ");";

                OleDbDataReader reader = command.ExecuteReader();
                List<Knjiga> filtrirane = new List<Knjiga>();

                while (reader.Read())
                {
                    Knjiga k = new Knjiga();

                    k.Id_knjiga = int.Parse(reader["id_knjiga"].ToString());
                    k.Broj_strana = int.Parse(reader["broj_strana"].ToString());
                    k.Cena = double.Parse(reader["cena"].ToString());
                    k.Popust = double.Parse(reader["popust"].ToString());
                    k.Naziv = reader["naziv"].ToString();
                    k.Autor = reader["autor"].ToString();

                    filtrirane.Add(k);
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = filtrirane;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(this.comboBox1.SelectedValue);
                System.Diagnostics.Debug.WriteLine("Exception caught");
            }
        }

        //Prikaz svih knjiga na klik
        private void button2_Click(object sender, EventArgs e)
        {
            PrikaziSveKnjige();
            if (dataGridView1.Rows.Count != 0)
                dataGridView1.CurrentRow.Selected = false;
        }
        //BindingSource
        private void PrikaziSveKnjige()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = knjigaBindingSource;
        }

        //Primena filtera
        private void button1_Click(object sender, EventArgs e)
        {
            primeniFilter();
            if (dataGridView1.Rows.Count != 0)
                dataGridView1.CurrentRow.Selected = false;
        }

        //Dodavanje knjige na racun
        private void button3_Click(object sender, EventArgs e)
        {
            bool exist = false;
            foreach(Stavka_Racun s in racun)
            {
                if (s.Id_knjiga == getSelectedBook())
                {
                    s.Kolicina += int.Parse(numericUpDown1.Value.ToString());
                    exist = true;
                    break;
                }
            }
            if(!exist)
                dodajNaRacun(getSelectedBook());

            textBox1.Text = UkupnaCena().ToString();
            RefreshRacun();
            numericUpDown1.Value = 1;
            if (dataGridView1.Rows.Count != 0)
                dataGridView1.CurrentRow.Selected = false;
            if (dataGridView2.Rows.Count != 0)
                dataGridView2.CurrentRow.Selected = false;
        }

        private void dodajNaRacun(int id_knjige)
        {
            try
            {
                DataBase db = new DataBase();
                db.OpenConnection();

                OleDbCommand command = new OleDbCommand();
                command.Connection = db.Connection;
                command.CommandText = "SELECT * FROM Knjiga WHERE id_knjiga = " + id_knjige + ";";

                OleDbDataReader reader = command.ExecuteReader();
                

                while (reader.Read())
                {

                    Stavka_Racun s = new Stavka_Racun();
                    s.Naziv = reader["naziv"].ToString();
                    s.Id_racun = 0;
                    s.Id_knjiga = id_knjige;
                    s.Kolicina = int.Parse(numericUpDown1.Value.ToString());
                    s.Cena = double.Parse(reader["cena"].ToString());
                    s.Popust = double.Parse(reader["popust"].ToString());

                    racun.Add(s);
                }

                RefreshRacun();


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(this.comboBox1.SelectedValue);
                System.Diagnostics.Debug.WriteLine("Exception caught");
            }
        }

        //Getteri za selektovane iteme koji vracaju id_selektovane knjige
        private int getSelectedBook()
        {
            int rowInd = dataGridView1.CurrentRow.Index;
            int colInd = 0;
            return int.Parse(dataGridView1.Rows[rowInd].Cells[colInd].Value.ToString());
        }
        private int getSelectedItem()
        {
            int rowInd = dataGridView2.CurrentRow.Index;
            int colInd = 2;
            System.Diagnostics.Debug.WriteLine(dataGridView2.Rows[rowInd].Cells[colInd].Value.ToString());
            return int.Parse(dataGridView2.Rows[rowInd].Cells[colInd].Value.ToString());
        }

        //Vraca ukupnu cenu
        private double UkupnaCena()
        {
            double uk = 0.0;
            foreach(Stavka_Racun s in racun)
            {
                uk += (s.Cena - (s.Cena * s.Popust / 100.0))*s.Kolicina;
            }
            return uk;
        }

        //Visak
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            
        }

        //Povecavanje kolicine za selektovan item
        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Stavka_Racun s in racun)
            {
                if (s.Id_knjiga == getSelectedItem())
                {
                    s.Kolicina++;
                    
                    break;
                }
            }
            textBox1.Text = UkupnaCena().ToString();
            
            dataGridView2.Refresh();
        }
        //Smanjivanje kolicine za selektovan item
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Stavka_Racun s in racun)
            {
                if (s.Id_knjiga == getSelectedItem())
                {
                    if (s.Kolicina > 1)
                        s.Kolicina--;
                    else
                        Storniraj();

                    break;
                }
            }
            textBox1.Text = UkupnaCena().ToString();

            dataGridView2.Refresh();
        }

        //Storniranje stavke iz liste
        private void Storniraj()
        {
            
            for (int i = 0; i < racun.Count; i++)
            {
                if (racun.ElementAt(i).Id_knjiga == getSelectedItem())
                {
                    racun.RemoveAt(i);
                    break;
                }
            }
            textBox1.Text = UkupnaCena().ToString();
            RefreshRacun();
        }
        //Uklanjanje itema iz liste
        private void button6_Click(object sender, EventArgs e)
        {
            Storniraj();
            RefreshRacun();
        }

        //Refreshuje datagrid
        private void RefreshRacun()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = racun.ToList();
            dataGridView2.Columns["Kolicina"].ValueType = typeof(NumericUpDown);
            dataGridView2.Columns["id_racun"].Visible = false;
            dataGridView2.Columns["popust"].Visible = false;
        }

        //Snimanje racuna u bazu podataka
        private void button7_Click(object sender, EventArgs e)
        {
            if (racun.Count > 0)
            {
                if (MessageBox.Show("ODSTAMPAJ RACUN?", "ODSTAMPAJ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    upisStavkiUbazu();
                    racun.Clear();
                    RefreshRacun();
                    ucitajNajprodavanijeKnjige();
                    textBox1.Text = "0";
                }
                MessageBox.Show("Odstampan racun!");
            }
            else
                MessageBox.Show("Nema stavki za izdavanje racuna!","Greska!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void animacija()
        {
            int speed = 1;
            while (true)
            {
                if (p.X + v.Width > 0)
                {
                    p.X -= speed;
                }
                else
                {
                    p.X = ClientSize.Width;
                }

                Thread.Sleep(10);
                this.Invalidate();

            }
        }

        private void upisStavkiUbazu()
        {
            /*
             * void funkcija za stampanje racuna u bazu
             * Usteda vremena jer samo jednom otvaramo konekciju
             * Izvrasava se transakcija koja podrazumeva dodavanje Novog racuna,
             * uzimanje poslednjeg broja kreiranog racuna,
             * dodavanje stavki u racun
             */

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
                
                //Prvo u transakciji dodavanje racuna
                val = "('" + DateTime.Now + "', " + UkupnaCena() + ")";
                sql = "INSERT INTO Racun(datum, ukupna_cena) VALUES" + val + ";";
                command.CommandText = sql;
                command.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("Dodat racun");

                //Popunjavanje Id racuna u stavkama na racunu
                int idRacuna = -1;
                command.CommandText = "SELECT MAX(id_racun) FROM Racun";
                idRacuna = (int)command.ExecuteScalar();
                System.Diagnostics.Debug.WriteLine("id racuna " + idRacuna);
                foreach (Stavka_Racun s in racun)
                    s.Id_racun = idRacuna;

                //Drugo u transakciji dodavanje stavki na racun
                foreach (Stavka_Racun s in racun)
                {
                    val = "(" + s.Id_racun + ", " + s.Id_knjiga + ", " + (s.Cena - s.Cena*s.Popust/100) + ", " + s.Popust + ", " + s.Kolicina + ")";
                    sql = "INSERT INTO Stavka_Racuna(id_racun, id_knjiga, cena, popust, kolicina) VALUES " + val + ";";

                    command.CommandText = sql;
                    command.ExecuteNonQuery();
  
                }
                transaction.Commit();
                System.Diagnostics.Debug.WriteLine("Successfull write to database");
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3(this);
            f.ShowDialog();
            osveziData1();
            if(dataGridView1.Rows.Count != 0)
                dataGridView1.CurrentRow.Selected = false;
        }

        public void osveziData1()
        {
            this.knjigaTableAdapter.Fill(this.knjizaraDataSet.Knjiga);
            knjigaBindingSource.DataSource = knjizaraDataSet.Knjiga;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = knjigaBindingSource;
            dataGridView1.Invalidate();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da zelite da uklonite sve stavke sa racuna?", "OPREZ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < racun.Count; i++)
                {
                    racun.RemoveAt(i);
                    i--;
                }
                textBox1.Text = UkupnaCena().ToString();
                RefreshRacun();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4(getSelectedBook());
            f.ShowDialog();
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
