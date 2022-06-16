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
    public partial class Form4 : Form
    {

        int idKnjige = -1;
        public Form4(int id_knjige)
        {
            InitializeComponent();
            idKnjige = id_knjige;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
            Paint += crtaj;
            Invalidate();

        }

        Random rnd = new Random();
        private void crtaj(object sender, PaintEventArgs e)
        {
            uzmiPodatke();
            float brojProdateKnjige = 0.0f;
            float ukupnoProdato = 0.0f;

            foreach (Stavka_Racun s in ukupno_prodatih)
            {
                ukupnoProdato += s.Kolicina;
                if (s.Id_knjiga == idKnjige)
                {
                    brojProdateKnjige = s.Kolicina;
                    label1.Text = s.Naziv + "\n" + "Broj prodatih primeraka: " + s.Kolicina;
                }
            }

            label2.Text = "Ostale knjige" + "\n" + "Broj ostalih prodatih knjiga: " + (int)(ukupnoProdato - brojProdateKnjige);

            System.Diagnostics.Debug.WriteLine("ukupno prodato u mesecu " + ukupnoProdato);
            System.Diagnostics.Debug.WriteLine("br prodate u mesecu " + brojProdateKnjige);
            System.Diagnostics.Debug.WriteLine("procenat prodate u mesecu " + (brojProdateKnjige / ukupnoProdato));
            System.Diagnostics.Debug.WriteLine("procenat ostalih u mesecu " + (ukupnoProdato - brojProdateKnjige) / ukupnoProdato);


            Rectangle r = new Rectangle(50, 50, 250, 250);
            Rectangle legend1 = new Rectangle(315, 52, 10, 10);
            Rectangle legend2 = new Rectangle(315, 102, 10, 10);
            float startUgao = -60;

            float x = 360.0f * (brojProdateKnjige / ukupnoProdato);

            Color boja = Color.RoyalBlue;
            SolidBrush cetka = new SolidBrush(boja);

            e.Graphics.FillPie(cetka, r, startUgao, x);
            e.Graphics.FillRectangle(cetka, legend1);
            startUgao += x;

            float y = 360.0f * (ukupnoProdato - brojProdateKnjige) / ukupnoProdato;
            boja = Color.OrangeRed;
            cetka = new SolidBrush(boja);

            e.Graphics.FillPie(cetka, r, startUgao, y);
            e.Graphics.FillRectangle(cetka, legend2);

        }

        List<Knjiga> knjige = new List<Knjiga>();
        List<Stavka_Racun> stavke = new List<Stavka_Racun>();

        List<Stavka_Racun> ukupno_prodatih = new List<Stavka_Racun>();
        private void uzmiPodatke()
        {
            try
            {
                DataBase db = new DataBase();
                db.OpenConnection();

                OleDbCommand command = new OleDbCommand();
                command.Connection = db.Connection;
                command.CommandText = "SELECT * FROM Knjiga;";

                OleDbDataReader reader = command.ExecuteReader();
                knjige = new List<Knjiga>();

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

                command.CommandText = "SELECT * FROM Stavka_racuna WHERE id_racun IN (SELECT id_racun FROM Racun WHERE YEAR(datum) = " + dateTimePicker1.Value.Year + " AND MONTH(datum) = " + dateTimePicker1.Value.Month + " );";
                System.Diagnostics.Debug.WriteLine(command.CommandText);
                reader = command.ExecuteReader();
                stavke = new List<Stavka_Racun>();

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

                ukupno_prodatih = new List<Stavka_Racun>();
                foreach (Knjiga k in knjige)
                {
                    int zbir = 0;
                    foreach (Stavka_Racun s in stavke)
                    {
                        if (s.Id_knjiga == k.Id_knjiga)
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

                for (int i = 0; i < ukupno_prodatih.Count - 1; i++)
                {
                    for (int j = i; j < ukupno_prodatih.Count; j++)
                    {
                        if (ukupno_prodatih[i].Kolicina < ukupno_prodatih[j].Kolicina)
                        {
                            Stavka_Racun temp = ukupno_prodatih[i];
                            ukupno_prodatih[i] = ukupno_prodatih[j];
                            ukupno_prodatih[j] = temp;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine("Exception caught");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            uzmiPodatke();
            Invalidate();
        }
    }
}
