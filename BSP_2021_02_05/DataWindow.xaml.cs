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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace BSP_2021_02_05
{
    /// <summary>
    /// Interaktionslogik für DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        public DataWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string pattern = "[0-9]+";
            using (LustigeDatenContext ctx = new LustigeDatenContext())
            {
                List<LustigeDaten> list = ctx.LustigeDaten.ToList();

                //Lustiges RegEx-Bubblesort Chaos um die bescheuerten TEXT Primary Keys zu sortieren....Grüße an Nina...>.>

                int regI, regJ;
                bool changed;
                int count = list.Count;
                do
                {
                    changed = false;
                    count--;
                    for (int i = 0; i < count; i++)
                    {
                        if (!Regex.IsMatch(list[i].Nummer, pattern))
                        {
                            regI = 0;
                        }
                        else
                        {
                            regI = Convert.ToInt32(Regex.Match(list[i].Nummer, pattern).Value);
                        }

                        if (!Regex.IsMatch(list[i + 1].Nummer, pattern))
                        {
                            regJ = 0;
                        }
                        else
                        {
                            regJ = Convert.ToInt32(Regex.Match(list[i + 1].Nummer, pattern).Value);
                        }

                        if (regI > regJ)
                        {
                            LustigeDaten temp = list[i + 1];
                            list[i + 1] = list[i];
                            list[i] = temp;
                            changed = true;
                        }
                    }
                } while (changed);

                dg.ItemsSource = list;
            }
        }
    }
}
