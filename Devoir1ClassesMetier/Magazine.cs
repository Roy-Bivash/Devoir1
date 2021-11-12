using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devoir1ClassesMetier
{
    public class Magazine
    {
        private int numMagazine;
        private string nomMagazine;
        private Specialite laSpecialite;
        private List<Article> lesArticles;

        public Magazine(int unNum, string unNom, Specialite uneSpecialite)
        {
            NumMagazine = unNum;
            NomMagazine = unNom;
            LaSpecialite = uneSpecialite;
            LesArticles = new List<Article>();
        }

        public int NumMagazine { get => numMagazine; set => numMagazine = value; }
        public string NomMagazine { get => nomMagazine; set => nomMagazine = value; }
        public Specialite LaSpecialite { get => laSpecialite; set => laSpecialite = value; }
        public List<Article> LesArticles { get => lesArticles; set => lesArticles = value; }

        public void AjouterArticle(Article unArticle)
        {
            lesArticles.Add(unArticle);
        }

        public double CalculerMontantMagazine()
        {
            double montant = 0;

            foreach(Article art in lesArticles)
            {
                montant = montant + art.NbFeuillets * art.LePigiste.PrixFeuillet;
            }

            return montant;
        }

        public List<TotalPigiste> GetTotalPigistes(Magazine mag)
        {
            List<TotalPigiste> lesTotaux = new List<TotalPigiste>();

            foreach(Article art in mag.LesArticles)
            {
                TotalPigiste tp = PigisteExiste(lesTotaux, art.LePigiste.NomPigiste);
                if (tp == null)
                {
                    tp = new TotalPigiste(art.LePigiste.NomPigiste, art.LePigiste.PrixFeuillet * art.NbFeuillets);
                    lesTotaux.Add(tp);
                }
                else
                {
                    tp.Total += art.LePigiste.PrixFeuillet * art.NbFeuillets;
                }
            }

            return lesTotaux;
        }

        public TotalPigiste PigisteExiste(List<TotalPigiste> desTotaux, string unNom)
        {
            TotalPigiste trouve = null;

            foreach(TotalPigiste tp in desTotaux)
            {
                if(tp.NomPigiste.CompareTo(unNom)==0)
                {
                    trouve = tp;
                }
            }

            return trouve;
        }
    }
}
