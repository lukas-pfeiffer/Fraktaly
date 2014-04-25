using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sierpinskeho_fraktal
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int pocetIteraci = 0;
        bool fraktalKoberec = true;
        bool fraktalKrajkovi = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        //algoritmus Sierpinskéno krajkoví (trojuhelníky)
        public void SierpinskehoKrajkovi(int iterace, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            if (iterace == 0)
            {
                return;
            }

            if (Math.Sqrt((double)(Math.Pow(x2 - x1, 2)) + (double)(Math.Pow(y2 - y1, 2))) > 2)
            {
                vykreslit(x1, y1, x2, y2);
                vykreslit(x2, y2, x3, y3);
                vykreslit(x3, y3, x1, y1);
            }

            double xa, ya, xb, yb, xc, yc;
            xa = (x1 + x2) / 2;
            ya = (y1 + y2) / 2;
            xb = (x1 + x3) / 2;
            yb = (y1 + y3) / 2;
            xc = (x2 + x3) / 2;
            yc = (y2 + y3) / 2;


            SierpinskehoKrajkovi(iterace - 1, x1, y1, xa, ya, xb, yb);
            SierpinskehoKrajkovi(iterace - 1, xa, ya, x2, y2, xc, yc);
            SierpinskehoKrajkovi(iterace - 1, xb, yb, xc, yc, x3, y3);
        }

        //vykreslení čár, které vykreslují trojúhelníky
        private void vykreslit(double x1, double y1, double x2, double y2)
        {
            Line cara = new Line();

            cara.Stroke = Brushes.Black;
            cara.StrokeThickness = 1.0;
            
            cara.X1 = x1;
            cara.X2 = x2;
            cara.Y1 = y1;
            cara.Y2 = y2;

            platno.Children.Add(cara);
        }

        //podmínky pro sierpinského krajkoví
        private void krajkovi()
        {
            if (fraktalKrajkovi == true)
            {
                fraktalKoberec = false;

                pocetIteraci++;

                if (pocetIteraci < 10)
                {
                    int troj = 300;
                    platno.Children.Clear();

                    if (rbPravouhliLevy.IsChecked == true)
                    {
                        troj = 0;
                        rbPravouhliPravy.IsEnabled = false;
                        rbRovnoramenny.IsEnabled = false;
                    }

                    if (rbPravouhliPravy.IsChecked == true)
                    {
                        troj = 600;
                        rbRovnoramenny.IsEnabled = false;
                        rbPravouhliLevy.IsEnabled = false;
                    }

                    if (rbRovnoramenny.IsChecked == true)
                    {
                        troj = 300;
                        rbPravouhliLevy.IsEnabled = false;
                        rbPravouhliPravy.IsEnabled = false;
                    }

                    SierpinskehoKrajkovi(pocetIteraci ,troj, 0.0, 600.0, 600.0, 0.0, 600.0);
                    //SierpinskehoKrajkovi(pocetIteraci, 300, 600, 0, 0, 600, 0);
                    
                    lFraktal.Content = "Sierpinského krajkoví";
                    lIterace.Content = "Počet iterací: " + pocetIteraci.ToString();
                }

                else
                {
                    MessageBox.Show("Počet iterací je omezený na 9, protože další iterace jsou náročné na výpočet.", "Omezení iterací", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnKrajkovi.IsEnabled = false;
                }
            }

            else
            {
                MessageBox.Show("Právě vykreslujete fraktál Sierpinského koberec, pokud chcete vykreslovat Sierpinského krajkoví, tak vymažte plochu.", "Vykreslujete Sierpinského koberec", MessageBoxButton.OK, MessageBoxImage.Warning);
                btnKrajkovi.IsEnabled = false;

                rbRovnoramenny.IsEnabled = false;
                rbPravouhliLevy.IsEnabled = false;
                rbPravouhliPravy.IsEnabled = false;
            }
        }

        //tlačíto spouštějící vykreslování krajkoví
        private void btnKrajkovi_Click(object sender, RoutedEventArgs e)
        {
            krajkovi();
        }

        //algoritmus sierpinského koberce (čtverce)
        private void SierpinskehoKoberec(int iterace, int x, int y, int sirka, int vyska)
        {
            if (iterace == 1)
            {
                vykreslitCtverec(x, y, sirka, vyska);
            }

            else
            {
                int s = sirka / 3;
                int v = vyska / 3;
                int x1 = x - 2 * s;
                int y1 = y - 2 * v;
                int x2 = x + s;
                int y2 = y + v;
                int x3 = x + 4 * s;
                int y3 = y + 4 * v;

                SierpinskehoKoberec(iterace - 1, x1, y1, s, v);
                SierpinskehoKoberec(iterace - 1, x2, y1, s, v);
                SierpinskehoKoberec(iterace - 1, x3, y1, s, v);
                SierpinskehoKoberec(iterace - 1, x1, y2, s, v);
                SierpinskehoKoberec(iterace - 1, x1, y3, s, v);
                SierpinskehoKoberec(iterace - 1, x2, y3, s, v);
                SierpinskehoKoberec(iterace - 1, x3, y3, s, v);
                SierpinskehoKoberec(iterace - 1, x3, y2, s, v);

                vykreslitCtverec(x, y, sirka, vyska);
            }
        }

        //vykreslení čtverců
        private void vykreslitCtverec(int x, int y, int sirka, int vyska)
        {
            Rectangle ctverec = new Rectangle();

            if (cbxVyplnit.IsChecked == true)
            {
                ctverec.Fill = Brushes.Black;
            }

            else
            {
                ctverec.Stroke = Brushes.Black;
                ctverec.StrokeThickness = 2.0;
            }

            ctverec.Height = vyska;
            ctverec.Width = sirka;

            Canvas.SetLeft(ctverec, x);
            Canvas.SetTop(ctverec, y);

            platno.Children.Add(ctverec);
        }

        //podmínky pro vykreslení sierpinského koberce
        private void koberec()
        {
            if (fraktalKoberec == true)
            {
                cbxVyplnit.IsEnabled = false;
                fraktalKrajkovi = false;

                pocetIteraci++;

                if (pocetIteraci < 7)
                {
                    platno.Children.Clear();
                    SierpinskehoKoberec(pocetIteraci, 200, 200, 200, 200);

                    lFraktal.Content = "Sierpinského koberec";
                    lIterace.Content = "Počet iterací: " + pocetIteraci.ToString();
                }
                else
                {
                    MessageBox.Show("Počet iterací je omezený na 6, protože další iterace jsou náročné na výpočet.", "Omezení iterací", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnKoberec.IsEnabled = false;
                }
            }

            else
            {
                MessageBox.Show("Právě vykreslujete fraktál Sierpinského krajkoví, pokud chcete vykreslovat Sierpinského koberec, tak vymažte plochu.", "Vykreslujete Sierpinského krajkoví", MessageBoxButton.OK, MessageBoxImage.Warning);
                btnKoberec.IsEnabled = false;
                cbxVyplnit.IsEnabled = false;
            }
        }

        //tlačítko na spuštení vykreslení sierpinského koberce
        private void btnKoberec_Click(object sender, RoutedEventArgs e)
        {
            koberec();
        }

        //tlačítko na vymazáni plochy a vrácení proměnných do půmvodního stavu
        private void btnVymazat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult odpoved = MessageBox.Show("Opravdu chcete vymazat daný fraktál?", "Vymazání", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (odpoved == MessageBoxResult.Yes)
            {
                pocetIteraci = 0;
                platno.Children.Clear();
                lFraktal.Content = "Sierpinského fraktál";
                lIterace.Content = "Počet iterací: " + pocetIteraci.ToString();
                fraktalKoberec = true;
                fraktalKrajkovi = true;
                btnKoberec.IsEnabled = true;
                btnKrajkovi.IsEnabled = true;
                rbPravouhliLevy.IsEnabled = true;
                rbPravouhliPravy.IsEnabled = true;
                rbRovnoramenny.IsEnabled = true;
                cbxVyplnit.IsEnabled = true;
            }
        }

        //tlačítko ukončující program
        private void btnKonec_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult odpoved = MessageBox.Show("Opravdu chcete ukončit program?", "Ukončení", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (odpoved == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}