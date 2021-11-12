using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Devoir1ClassesMetier
{
    public class GstBDD
    {
        private MySqlConnection cnx;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        // Constructeur
        public GstBDD()
        {
            string chaine = "Server=localhost;Database=projet_pigiste;Uid=root;Pwd=;SslMode=none";
            cnx = new MySqlConnection(chaine);
            cnx.Open();
        }

        public List<Pigiste> getAllPigistes()
        {
            List<Pigiste> tousLesPigiste = new List<Pigiste>();
            cmd = new MySqlCommand("SELECT pigiste.id_pigiste, pigiste.nom_pigiste, pigiste.prix_feuillets FROM pigiste;", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Pigiste unNouvPigist = new Pigiste(Convert.ToInt16(dr[0]), dr[1].ToString(), Convert.ToDouble(dr[2]));
                tousLesPigiste.Add(unNouvPigist);
            }
            dr.Close();

            return tousLesPigiste;
        }

        public List<Magazine> GetAllMagazines()
        {
            List<Magazine> tousLesMagazines = new List<Magazine>();
            cmd = new MySqlCommand("SELECT magazine.id_magazine, magazine.nom_magazine, magazine.num_specialite, specialite.nom_specialite FROM magazine INNER JOIN specialite ON magazine.num_specialite = specialite.id_specialite; ", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Specialite spe = new Specialite(Convert.ToInt16(dr[2]), dr[3].ToString() );
                Magazine unNouvMagazine = new Magazine(Convert.ToInt16(dr[0]), dr[1].ToString(), spe);
                tousLesMagazines.Add(unNouvMagazine);
            }
            dr.Close();

            return tousLesMagazines;
        }

        public List<Article> GetAllArticleByMagazine(int numMagazine)
        {
            List<Article> articleByMagazine = new List<Article>();
            cmd = new MySqlCommand("SELECT article.titre, article.nb_feuillets, pigiste.id_pigiste, pigiste.nom_pigiste, pigiste.prix_feuillets FROM article INNER JOIN pigiste ON article.num_pigiste = pigiste.id_pigiste WHERE article.num_magazine = " + numMagazine, cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Pigiste pig = new Pigiste(Convert.ToInt16(dr[2]), dr[3].ToString(), Convert.ToDouble(dr[4]));
                Article unNouvArticle = new Article(dr[0].ToString(), Convert.ToUInt16(dr[1]), pig);
                articleByMagazine.Add(unNouvArticle);
            }
            dr.Close();

            return articleByMagazine;
        }


        public Double GetPrixMagazine(int numMagazine)
        {
            Double prixMagazine = 0;
            cmd = new MySqlCommand("SELECT article.nb_feuillets, pigiste.prix_feuillets FROM article INNER JOIN pigiste ON article.num_pigiste = pigiste.id_pigiste WHERE article.num_magazine = " + numMagazine, cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                prixMagazine = prixMagazine + Convert.ToDouble(dr[0]) * Convert.ToDouble(dr[1]);
            }
            dr.Close();

            return Math.Round(prixMagazine, 2);
        }

        public List<TotalPigiste> GetTotalPigisteByMagazine(int numMagazine)
        {
            List<TotalPigiste> leTotalPigiste = new List<TotalPigiste>();
            cmd = new MySqlCommand("SELECT pigiste.nom_pigiste, pigiste.prix_feuillets, SUM(article.nb_feuillets) FROM pigiste INNER JOIN article ON pigiste.id_pigiste = article.num_pigiste INNER JOIN magazine on article.num_magazine = magazine.id_magazine WHERE magazine.id_magazine = " + numMagazine + " GROUP BY(nom_pigiste) ", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                double leTotal = Convert.ToDouble(dr[1]) * Convert.ToDouble(dr[2]);
                TotalPigiste unNouvTotalPigiste = new TotalPigiste(dr[0].ToString(), Math.Round(leTotal, 2));
                leTotalPigiste.Add(unNouvTotalPigiste);
            }
            dr.Close();

            return leTotalPigiste;
        }

        public bool PossederSpecialite(int numPigiste, int numMagazine)
        {
            bool existe = false;
            cmd = new MySqlCommand("SELECT id_magazine FROM avoir INNER JOIN magazine ON avoir.num_specialite = magazine.num_specialite WHERE avoir.num_pigiste = " + numPigiste + " AND avoir.num_specialite = " + numMagazine, cnx);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                existe = true;
                break;
            }
            dr.Close();
            return existe;

        }

        public void AddNouvArticle(string titre, int nbFeuillets, int numPigiste, int numMagazine)
        {
            //insertion dans BDD
            cmd = new MySqlCommand("INSERT INTO `article`(`id_article`, `titre`, `nb_feuillets`, `num_pigiste`, `num_magazine`) VALUES (null, '" + titre + "'," + nbFeuillets + "," + numPigiste +"," + numMagazine+  ")", cnx);
            cmd.ExecuteNonQuery();
        }



    }
}
