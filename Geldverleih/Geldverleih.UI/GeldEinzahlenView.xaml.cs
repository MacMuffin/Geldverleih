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
using System.Windows.Shapes;
using Geldverleih.UI.presenters;
using Geldverleih.UI.views;

namespace Geldverleih.UI
{
    /// <summary>
    /// Interaktionslogik für GeldEinzahlenView.xaml
    /// </summary>
    public partial class GeldEinzahlenView : Window, IEinzahlungsView
    {
        private readonly Guid _vorgangsNummer;
        private BankPresenter _bankPresenter;

        public GeldEinzahlenView(Guid vorgangsNummer)
        {
            InitializeComponent();
            _vorgangsNummer = vorgangsNummer;
        }

        public void Initialisieren(BankPresenter bankPresenter)
        {
            _bankPresenter = bankPresenter;
            VorgangsNummerBarItem.Content += _vorgangsNummer.ToString();
            BereitsEingezahlteVorgaengeGrid.ItemsSource = _bankPresenter.GetAlleEingezahltenVorgaengeZurVorgangsNummer(_vorgangsNummer);
        }

        private void EinzahlenButton_Click(object sender, RoutedEventArgs e)
        {
            _bankPresenter.GeldEinzahlen(_vorgangsNummer, Convert.ToDecimal(EinzahlbetragTextbox.Text));
        }
    }
}
