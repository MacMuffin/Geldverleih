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
using Geldverleih.Domain;
using Geldverleih.Repository;
using Geldverleih.Repository.interfaces;
using Geldverleih.Service;
using Geldverleih.Service.interfaces;
using Geldverleih.UI.presenters;

namespace Geldverleih.UI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IKundenRepository kundenRepository = new KundenRepository();
            IKundenService kundenService = new KundenService(kundenRepository);
            KundenPresenter kundenPresenter = new KundenPresenter(kundenService);

            IList<Kunde> kunden = kundenPresenter.AlleKundenAuslesen();


            IAusleihRepository ausleihRepository = new AusleihRepository();
            IAusUndVerleihFactory factory = new AusUndVerleihFactory();
            IBankService bankService = new BankService(ausleihRepository, kundenRepository, factory);
            BankPresenter bankPresenter = new BankPresenter(bankService);

            bankPresenter.GeldAusleihen(kunden.First(), new VerleihKondition(), 12.5m);
        }
    }
}
