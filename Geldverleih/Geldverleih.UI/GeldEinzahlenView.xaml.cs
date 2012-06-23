using System;
using System.Windows;
using Geldverleih.UI.presenters;
using Geldverleih.UI.views;

namespace Geldverleih.UI
{
    /// <summary>
    /// Interaktionslogik für GeldEinzahlenView.xaml
    /// </summary>
    public partial class GeldEinzahlenView : Window, IEinzahlungsView
    {
        private Guid _vorgangsNummer;
        private BankPresenter _bankPresenter;

        public GeldEinzahlenView()
        {
            InitializeComponent();
            
        }

        public void Initialisieren(BankPresenter bankPresenter, Guid vorgangsNummer)
        {
            _bankPresenter = bankPresenter;
            _vorgangsNummer = vorgangsNummer;
            VorgangsNummerBarItem.Content += _vorgangsNummer.ToString();
            Aktualisieren();
        }

        private void EinzahlenButton_Click(object sender, RoutedEventArgs e)
        {
            _bankPresenter.GeldEinzahlen(_vorgangsNummer, Convert.ToDecimal(EinzahlbetragTextbox.Text));
        }

        public void EinzahlungAbgeschlossenResult(string result)
        {
            MessageBox.Show(result, "Einzahlung");
        }

        public void Aktualisieren()
        {
            BereitsEingezahlteVorgaengeGrid.ItemsSource = _bankPresenter.GetAlleEingezahltenVorgaengeZurVorgangsNummer(_vorgangsNummer);
        }
    }
}
