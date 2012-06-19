using System;
using System.Collections.Generic;
using System.Linq;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;
using Geldverleih.Service.interfaces;

namespace Geldverleih.Service
{
    public class BankService : IBankService
    {
        private readonly IAusleihRepository _ausleihRepository;
        private readonly IKundenRepository _kundenRepository;

        public BankService(IAusleihRepository ausleihRepository, IKundenRepository kundenRepository)
        {
            _ausleihRepository = ausleihRepository;
            _kundenRepository = kundenRepository;
        }

        public void GeldAusleihen(Kunde kunde, VerleihKondition verleihKondition)
        {
            KundenAufVerfuegbarkeitPruefen(kunde);

            _ausleihRepository.GeldAnKundenAusleihen(kunde, verleihKondition);
        }

        public void GeldEinzahlen(Kunde kunde, Guid vorgangsNummer, decimal betrag)
        {
            KundenAufVerfuegbarkeitPruefen(kunde);

            _ausleihRepository.KundeZahltGeldEin(kunde, vorgangsNummer, betrag);
        }

        private void KundenAufVerfuegbarkeitPruefen(Kunde kunde)
        {
            IList<Kunde> alleKunden = _kundenRepository.GetAlleKunden();

            if (!IstKundeInDerDatenbank(alleKunden, kunde))
                throw new ApplicationException(
                    string.Format("Der kunde {0} existiert nicht in der Datenbank! Bitte vorher anlegen",
                                  kunde.Kundennummer));
        }

        private bool IstKundeInDerDatenbank(IList<Kunde> alleKunden, Kunde kunde)
        {
            return alleKunden.Any(x => x.Kundennummer == kunde.Kundennummer);
        }
    }
}