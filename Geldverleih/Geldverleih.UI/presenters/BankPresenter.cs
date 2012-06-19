using System;
using Geldverleih.Domain;
using Geldverleih.Service.interfaces;

namespace Geldverleih.UI.presenters
{
    public class BankPresenter
    {
        private readonly IBankService _bankService;

        public BankPresenter(IBankService bankService)
        {
            _bankService = bankService;
        }

        public void GeldEinzahlen(Guid vorgangsNummer, decimal betrag)
        {
            _bankService.GeldEinzahlen(vorgangsNummer, betrag);
        }

        public void GeldAusleihen(Kunde kunde, VerleihKondition kondition, decimal betrag)
        {
            _bankService.GeldAusleihen(kunde.Kundennummer, kondition, betrag);
        }
    }
}