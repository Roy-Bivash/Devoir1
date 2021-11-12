using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devoir1ClassesMetier
{
    public class TotalPigiste
    {
        private string nomPigiste;
        private double total;

        public TotalPigiste(string unNom, double unTotal)
        {
            NomPigiste = unNom;
            Total = unTotal;
        }

        public string NomPigiste { get => nomPigiste; set => nomPigiste = value; }
        public double Total { get => total; set => total = value; }
    }
}
