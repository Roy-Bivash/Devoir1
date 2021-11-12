using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devoir1ClassesMetier
{
    public class Pigiste
    {
        private int numPigiste;
        private string nomPigiste;
        private double prixFeuillet;
        private List<Specialite> lesSpecialites;

        public Pigiste(int unNum, string unNom, double unPrix)
        {
            NumPigiste = unNum;
            NomPigiste = unNom;
            PrixFeuillet = unPrix;
            LesSpecialites = new List<Specialite>();
        }

        public int NumPigiste { get => numPigiste; set => numPigiste = value; }
        public string NomPigiste { get => nomPigiste; set => nomPigiste = value; }
        public double PrixFeuillet { get => prixFeuillet; set => prixFeuillet = value; }
        public List<Specialite> LesSpecialites { get => lesSpecialites; set => lesSpecialites = value; }

        public void AjouterSpecialite(Specialite uneSpecialite)
        {
            lesSpecialites.Add(uneSpecialite);
        }

        //public bool PossederSpecialite(Specialite uneSpe)
        //{
        //    bool trouve = false;

        //    foreach(Specialite spe in lesSpecialites)
        //    {
        //        if(spe.NumSpe == uneSpe.NumSpe)
        //        {
        //            trouve = true;
        //            break;
        //        }
        //    }

        //    return trouve;
        //}
    }
}
