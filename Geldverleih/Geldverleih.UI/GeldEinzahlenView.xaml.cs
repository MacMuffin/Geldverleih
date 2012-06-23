using System;
using System.Windows;
using Geldverleih.UI.presenters;
using Geldverleih.UI.views;
using log4net;

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

        protected static readonly ILog Log = LogManager.GetLogger(typeof(GeldEinzahlenView));

        public void FehlerLoggen(string message)
        {
            log4net.Config.XmlConfigurator.Configure();

            Log.Error(message);
        }

        public void Initialisieren(BankPresenter bankPresenter, Guid vorgangsNummer)
        {
            try
            {
                _bankPresenter = bankPresenter;
                _vorgangsNummer = vorgangsNummer;
                VorgangsNummerBarItem.Content += _vorgangsNummer.ToString();
                Aktualisieren();
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }

        private void EinzahlenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _bankPresenter.GeldEinzahlen(_vorgangsNummer, Convert.ToDecimal(EinzahlbetragTextbox.Text));
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }

        public void EinzahlungAbgeschlossenResult(string result)
        {
            MessageBox.Show(result, "Einzahlung");
        }

        public void Aktualisieren()
        {
            try
            {
                BereitsEingezahlteVorgaengeGrid.ItemsSource = _bankPresenter.GetAlleEingezahltenVorgaengeZurVorgangsNummer(_vorgangsNummer);
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }
    }
}
