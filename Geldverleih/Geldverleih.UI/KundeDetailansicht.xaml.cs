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

namespace Geldverleih.UI
{
    /// <summary>
    /// Interaktionslogik für KundeDetailansicht.xaml
    /// </summary>
    public partial class KundeDetailansicht : Window
    {
        private readonly KundenPresenter _kundenPresenter;

        public KundeDetailansicht(KundenPresenter kundenPresenter)
        {
            _kundenPresenter = kundenPresenter;
            InitializeComponent();
        }

        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            _kundenPresenter.KundenAnlegen(NameTextbox.Text, VornameTextbox.Text, WohnortTextbox.Text, AdresseTextbox.Text, Convert.ToInt32(PLZTextbox.Text));
            Close();
        }
    }
}
