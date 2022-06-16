using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara_Milance_NRT820
{
    internal class Racun
    {
        private int id_racun;
        private double ukupna_cena;
        private DateTime datum;

        public Racun()
        {
            
        }

        public Racun(int id_racun, double ukupna_cena, DateTime datum)
        {
            this.id_racun = id_racun;
            this.ukupna_cena = ukupna_cena;
            this.datum = datum;
        }

        public int Id_racun { get => id_racun; set => id_racun = value; }
        public double Ukupna_cena { get => ukupna_cena; set => ukupna_cena = value; }
        public DateTime Datum { get => datum; set => datum = value; }
    }
}
