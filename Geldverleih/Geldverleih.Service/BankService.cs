using System;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;
using Geldverleih.Service.interfaces;

namespace Geldverleih.Service
{
    public class BankService : IBankService
    {
        private readonly IAusleihRepository _ausleihRepository;

        public BankService(IAusleihRepository ausleihRepository)
        {
            _ausleihRepository = ausleihRepository;
        }

        public void GeldAusleihen(Kunde kunde, VerleihKondition verleihKondition)
        {
            _ausleihRepository.GeldAnKundenAusleihen(kunde, verleihKondition);
        }

        public void GeldEinzahlen(Kunde kunde, decimal betrag)
        {
            _ausleihRepository.KundeZahltGeldEin(kunde, betrag);
        }
    }
}