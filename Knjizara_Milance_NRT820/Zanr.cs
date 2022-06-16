using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara_Milance_NRT820
{
    internal class Zanr
    {
        private int id;
        private string naziv;

        public Zanr(int id, string naziv)
        {
            this.id = id;
            this.naziv = naziv;
        }

        public int Id { get => id; }
        public string Naziv { get => naziv; }
    }
}
