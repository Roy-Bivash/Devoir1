using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devoir1ClassesMetier
{
    public class Article
    {
        private string titreArticle;
        private int nbFeuillets;
        private Pigiste lePigiste;

        public Article(string unTitre, int unNb, Pigiste unPigiste)
        {
            TitreArticle = unTitre;
            NbFeuillets = unNb;
            LePigiste = unPigiste;
        }

        public string TitreArticle { get => titreArticle; set => titreArticle = value; }
        public int NbFeuillets { get => nbFeuillets; set => nbFeuillets = value; }
        public Pigiste LePigiste { get => lePigiste; set => lePigiste = value; }
    }
}
