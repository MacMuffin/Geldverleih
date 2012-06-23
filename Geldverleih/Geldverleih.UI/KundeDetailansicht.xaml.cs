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
            {
                KundeDetailAnsicht.Kunde = new Kunde
                                               {
                                                   Adresse = AdresseTextbox.Text,
                                                   Name = NameTextbox.Text,
                                                   Vorname = VornameTextbox.Text,
                                                   PLZ = Convert.ToInt32(PLZTextbox.Text),
                                                   Wohnort = WohnortTextbox.Text
                                               };
            }
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
                MessageBox.Show("Es wurde kein Vorgang ausgewaehlt, zu dem Geld zurückgezahlt werden kann.");
                return;
            }
            _kundeDetailPresenter.GeldEinzahlenViewLaden(ausleihVorgang.VorgangsNummer);
        }
    }
}
