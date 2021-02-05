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

//Erste Abfrage ist ultra langsam.....Entity Framework halt aber ab der zweiten rennt's :)
namespace BSP_2021_02_05 {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            cmbErgebnis.Items.Add("Bestanden");
            cmbErgebnis.Items.Add("Durchgefallen");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (allesEingegeben())
            {
                using (LustigeDatenContext ctx = new LustigeDatenContext())
                {
                    var daten = new LustigeDaten
                    {
                        Nummer = txtNummer.Text,
                        Datum = dpDatum.DisplayDate,
                        Bezeichnung = txtBezeichnung.Text,
                        Ergebnis = cmbErgebnis.Text
                    };
                    ctx.LustigeDaten.Add(daten);
                    ctx.SaveChangesAsync();

                    txtNummer.Text = "";
                    dpDatum.SelectedDate = default;
                    txtBezeichnung.Text = "";
                    cmbErgebnis.SelectedItem = default;
                    MessageBox.Show("Daten gespeichert!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Nicht alle Felder angegeben!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool allesEingegeben()
        {
            bool retVal = true;

            if (String.IsNullOrEmpty(txtNummer.Text))
                retVal = false;
            if (String.IsNullOrEmpty(dpDatum.SelectedDate.ToString()))
                retVal = false;
            if (String.IsNullOrEmpty(txtBezeichnung.Text))
                retVal = false;
            if (String.IsNullOrEmpty(cmbErgebnis.Text))
                retVal = false;

            return retVal;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            DataWindow dw = new DataWindow();
            dw.Show();
        }

        private void txtNummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (LustigeDatenContext ctx = new LustigeDatenContext())
            {
                var q = from d in ctx.LustigeDaten select d.Nummer;
                if (q.ToList().Contains(txtNummer.Text))
                {
                    lblWarnung.Visibility = Visibility.Visible;
                    imgWarnung.Visibility = Visibility.Visible;
                    txtNummer.Background = Brushes.IndianRed;
                    btnSave.IsEnabled = false;
                }
                else
                {
                    txtNummer.Background = default;
                    lblWarnung.Visibility = Visibility.Hidden;
                    imgWarnung.Visibility = Visibility.Hidden;
                    btnSave.IsEnabled = true;
                }
            }

        }
    }
}
