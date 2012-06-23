using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Geldverleih.Domain;
using Geldverleih.UI.models;
using Geldverleih.UI.presenters;
using Geldverleih.UI.views;

namespace Geldverleih.UI
{
    /// <summary>
    /// Interaktionslogik für KundeDetailansicht.xaml
    /// </summary>
    public partial class KundeDetailansicht : Window, IKundeDetailView
    {
        private KundeDetailPresenter _kundeDetailPresenter;
        public KundenDetailModel KundeDetailAnsicht { get; set; }

        public KundeDetailansicht()
        {
            InitializeComponent();
        }

        public void Initialisieren(KundeDetailPresenter kundeDetailPresenter)
        {
            _kundeDetailPresenter = kundeDetailPresenter;
        }

        public void AusleihUebersichtAktualisieren()
        {
            AusleihvorgaengeKundeGrid.ItemsSource = _kundeDetailPresenter.GetAlleAusleihvorgaengeZumKunden();
        }

        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            if (KundeDetailAnsicht.Kunde == null)
                KundeDetailAnsicht.Kunde = new Kunde();

            KundeDetailAnsicht.Kunde.Adresse = AdresseTextbox.Text;
            KundeDetailAnsicht.Kunde.Name = NameTextbox.Text;
            KundeDetailAnsicht.Kunde.Vorname = VornameTextbox.Text;
            KundeDetailAnsicht.Kunde.Wohnort = WohnortTextbox.Text;
            KundeDetailAnsicht.Kunde.PLZ = Convert.ToInt32(PLZTextbox.Text);

            _kundeDetailPresenter.KundenSpeichern();
            Close();
        }

        public void KundeNochNichtErstelltModus()
        {
            AusleihvorgaengeTab.IsEnabled = false;
        }

        public void ModalAnsichtLaden()
        {
            ShowDialog();
        }

        public void KundeBearbeitenModus()
        {
            VornameTextbox.Text = KundeDetailAnsicht.Kunde.Vorname;
            PLZTextbox.Text = KundeDetailAnsicht.Kunde.PLZ.ToString();
            NameTextbox.Text = KundeDetailAnsicht.Kunde.Name;
            AdresseTextbox.Text = KundeDetailAnsicht.Kunde.Adresse;
            WohnortTextbox.Text = KundeDetailAnsicht.Kunde.Wohnort;
        }

        private void AusleihvorgaengeTab_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            if (ZinssatzComboBox.ItemsSource == null)
            {
                ZinssatzComboBox.ItemsSource = _kundeDetailPresenter.GetAlleZinssaetze();
                AusleihUebersichtAktualisieren();
            }
        }

        private void AusleihenButton_Click(object sender, RoutedEventArgs e)
        {
            _kundeDetailPresenter.KundeLeihtGeld(Convert.ToDecimal(LeihbetragTextbox.Text), new VerleihKondition(Convert.ToDecimal(ZinssatzComboBox.Text)));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GeldEinzahlenButton_Click(object sender, RoutedEventArgs e)
        {
            AusleihVorgang ausleihVorgang = (AusleihVorgang)AusleihvorgaengeKundeGrid.SelectedItem;

            if (ausleihVorgang == null)
            {
                MessageBox.Show(Properties.Resources.KundeDetailansicht_GeldEinzahlenButton_Click_KeinVorgangAusgewaehlt_Message, 
                    Properties.Resources.KundeDetailansicht_GeldEinzahlenButton_Click_KeinVorgangAusgewaehlt_Caption);
                return;
            }
            _kundeDetailPresenter.GeldEinzahlenViewLaden(ausleihVorgang.VorgangsNummer);
        }

        private void PLZTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsNumber(e.Text.ToCharArray().First());
        }
    }
}
