using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara_Milance_NRT820
{
    internal class Stavka_Racun
    {
        private string naziv;
        private int id_racun;
        private int id_knjiga;
        private double cena;
        private double popust;
        private int kolicina;

        public Stavka_Racun(int id_racun, int id_knjiga, double cena, double popust, int kolicina)
        {
            this.id_racun = id_racun;
            this.id_knjiga = id_knjiga;
            this.cena = cena;
            this.popust = popust;
            this.kolicina = kolicina;
        }

        public Stavka_Racun()
        {
            
        }
        public string Naziv { get => naziv; set => naziv = value; }
        public int Id_racun { get => id_racun; set => id_racun = value; }
        public int Id_knjiga { get => id_knjiga; set => id_knjiga = value; }
        public double Cena { get => cena; set => cena = value; }
        public double Popust { get => popust; set => popust = value; }
        public int Kolicina { get => kolicina; set => kolicina = value; }
       
    }
}
