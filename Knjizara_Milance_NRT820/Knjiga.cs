using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara_Milance_NRT820
{
    internal class Knjiga
    {
        private int id_knjiga;
        private string autor;
        private string naziv;
        private int broj_strana;
        private double cena;
        private double popust;

        public Knjiga()
        {
            this.id_knjiga = -1;
            this.autor = "";
            this.naziv = "";
            this.broj_strana = -1;
            this.cena = 0.0;
            this.popust = 0.0;
        }

        public Knjiga(int id_knjiga, string autor, string naziv, int broj_strana, double cena, double popust)
        {
            this.id_knjiga = id_knjiga;
            this.autor = autor;
            this.naziv = naziv;
            this.broj_strana = broj_strana;
            this.cena = cena;
            this.popust = popust;
        }

        public int Id_knjiga { get => id_knjiga; set => id_knjiga = value; }
        public string Autor { get => autor; set => autor = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public int Broj_strana { get => broj_strana; set => broj_strana = value; }
        public double Cena { get => cena; set => cena = value; }
        public double Popust { get => popust; set => popust = value; }
    }
}
