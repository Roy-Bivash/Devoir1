using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Devoir1ClassesMetier;

namespace Devoir1WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GstBDD gst;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void lstMagazines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstMagazines.SelectedItem != null)
            {
                lstArticles.ItemsSource = gst.GetAllArticleByMagazine((lstMagazines.SelectedItem as Magazine).NumMagazine);
                txtMontantMagazine.Text = gst.GetPrixMagazine((lstMagazines.SelectedItem as Magazine).NumMagazine).ToString();
                lstTotalPigiste.ItemsSource = gst.GetTotalPigisteByMagazine((lstMagazines.SelectedItem as Magazine).NumMagazine);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new GstBDD();
            lstMagazines.ItemsSource = gst.GetAllMagazines();
            cboPigistes.ItemsSource = gst.getAllPigistes();

        }


        private void lstArticles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstArticles.SelectedItem != null)
            {
                txtNomPigiste.Text = (lstArticles.SelectedItem as Article).LePigiste.NomPigiste;
                txtMontantArticle.Text = Math.Round(((lstArticles.SelectedItem as Article).NbFeuillets * (lstArticles.SelectedItem as Article).LePigiste.PrixFeuillet),2).ToString();
            }
        }

        private void btnAjouterArticle_Click(object sender, RoutedEventArgs e)
        {
            if(lstMagazines.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un magazine ", "Erreur de sélection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(txtTitreArticle.Text == "")
            {
                MessageBox.Show("Veuillez saisir un titre ", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(cboPigistes.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un pigiste ", "Erreur de choix", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // On récupère le pigiste dans la ComboBox
                //Pigiste selectedPig = cboPigistes.SelectedItem as Pigiste;

                // Question : est-ce que le pigiste choisi possède la même spécialité que celle du magazine
                // Si oui il peut écrire un article pour ce magazine
                // Si non : on affiche un message

                //if(selectedPig.PossederSpecialite((lstMagazines.SelectedItem as Magazine).LaSpecialite))
                //{
                //    Article art = new Article(txtTitreArticle.Text, Convert.ToInt16(sldNbFeuillets.Value), selectedPig);
                //    (lstMagazines.SelectedItem as Magazine).AjouterArticle(art);
                //    lstArticles.Items.Refresh();
                //    lstTotalPigiste.ItemsSource = (lstMagazines.SelectedItem as Magazine).GetTotalPigistes(lstMagazines.SelectedItem as Magazine);
                //    txtMontantMagazine.Text = (lstMagazines.SelectedItem as Magazine).CalculerMontantMagazine().ToString();
                //}
                //else
                //{
                //    MessageBox.Show("Le pigiste choisi ne possède pas \nla spécialité du magazine ", "Choix du pigiste", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
                Pigiste selectedPig = cboPigistes.SelectedItem as Pigiste;
                if (!gst.PossederSpecialite(selectedPig.NumPigiste, (lstMagazines.SelectedItem as Magazine).NumMagazine))
                {

                    //crerer l'article
                    Article nouvArticle = new Article(txtTitreArticle.Text, Convert.ToInt16(sldNbFeuillets.Value), selectedPig);

                    //l'inserer dans la base
                    gst.AddNouvArticle(txtTitreArticle.Text, Convert.ToInt16(sldNbFeuillets.Value), selectedPig.NumPigiste, (lstMagazines.SelectedItem as Magazine).NumMagazine);

                    //rafraichir
                    lstArticles.Items.Refresh();
                    txtMontantMagazine.Text = gst.GetPrixMagazine((lstMagazines.SelectedItem as Magazine).NumMagazine).ToString();
                    lstTotalPigiste.ItemsSource = gst.GetTotalPigisteByMagazine((lstMagazines.SelectedItem as Magazine).NumMagazine);

                }
                else
                {
                    MessageBox.Show("Le pigiste choisi ne possède pas \nla spécialité du magazine ", "Choix du pigiste", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
