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

namespace snehova_vlocka_kocha
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double[] x = { 0.0, 300.0, 150.0 };
        double[] y = { Math.Sqrt(300.0 * 300.0 - 150.0 * 150.0), Math.Sqrt(300.0 * 300.0 - 150.0 * 150.0), 0.0 };

        bool pom = false; //pomocná proměná, aby bylo zajištěno, že se jako první vykreslí trojúhelník         

        int pocet = -1;

        private void snehovaVlockaKocha()
        {
            if (mrizka.Children.Count >= 1) //podmínka pro smazaní předchozí vločky
            {
                mrizka.Children.Clear(); //smazání předchozí vločky
            }

            if (pom == true)
            {
                double[] novyX = new double[x.Length * 4];
                double[] novyY = new double[y.Length * 4];

                for (int i = 0; i < x.Length; i++)
                {
                    novyX[4 * i] = x[i];
                    novyY[4 * i] = y[i];

                    double dx = x[(i + 1) % x.Length] - x[i];
                    double dy = y[(i + 1) % y.Length] - y[i];

                    novyX[4 * i + 1] = x[i] + dx / 3.0;
                    novyY[4 * i + 1] = y[i] + dy / 3.0;
                    novyX[4 * i + 3] = x[i] + 2.0 * dx / 3.0;
                    novyY[4 * i + 3] = y[i] + 2.0 * dy / 3.0;

                    double a = dx / 6.0 - Math.Sqrt(3.0) / 6 * dy;
                    double b = dy / 6.0 + Math.Sqrt(3.0) / 6 * dx;
                    novyX[4 * i + 2] = novyX[4 * i + 1] + a;
                    novyY[4 * i + 2] = novyY[4 * i + 1] + b;
                }

                x = novyX;
                y = novyY;
            }

            Polygon vlocka = new Polygon();
            vlocka.Stroke = Brushes.Black;
            vlocka.StrokeThickness = 0.5;

            for (int i = 0; i < x.Length; i++)
            {
                vlocka.Points.Add(new Point(x[i], y[i]));
            }

            mrizka.Children.Add(vlocka);
            pom = true;
        }

        private void btnIterace_Click(object sender, RoutedEventArgs e)
        {
            if (pocet < 8)
            {
                pocet += 1;
                snehovaVlockaKocha();
                lPocet.Content = pocet.ToString();
            }
            else
            {
                MessageBox.Show("Počet iterací je omezený na 8, protože další iterace jsou náročné na výpočet.", "Omezení iterací", MessageBoxButton.OK, MessageBoxImage.Information);
                btnIterace.IsEnabled = false;
            }
        }

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