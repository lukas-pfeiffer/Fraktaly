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

namespace Cantorova_mnozina
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

        private void btnVykreslit_Click(object sender, RoutedEventArgs e)
        {
            cantorovaMnozina(0, 0, 700, 0);
        }

        private void vykresleni(double ax, double ay, double bx, double by)
        {
            Line cara = new Line();

            cara.Stroke = Brushes.Black;
            cara.StrokeThickness = 2.0;
            cara.X1 = ax;
            cara.X2 = bx;
            cara.Y1 = ay;
            cara.Y2 = by;

            mrizka.Children.Add(cara);
        }

        public void cantorovaMnozina(double ax, double ay, double bx, double by)
        {

            double c = 1;
            if ((bx - ax) < c)
            {
                vykresleni((int)ax, (int)ay, (int)bx, (int)by);
            }

            else
            {
                double cx = 0;
                double cy = 0;
                double dx = 0;
                double dy = 0;

                vykresleni((int)ax, (int)ay, (int)bx, (int)by);

                cx = ax + (bx - ax) / 3;

                cy = ay + 20;

                dx = bx - (bx - ax) / 3;

                dy = by + 20;
                ay = ay + 20;
                by = by + 20;

                cantorovaMnozina(ax, ay, cx, cy);
                cantorovaMnozina(dx, dy, bx, by);
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

        private void btnVymazat_Click(object sender, RoutedEventArgs e)
        {
            mrizka.Children.Clear();
        }
    }
}