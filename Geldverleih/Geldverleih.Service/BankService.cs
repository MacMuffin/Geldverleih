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
        private readonly IAusUndVerleihFactory _factory;

        public BankService(IAusleihRepository ausleihRepository, IKundenRepository kundenRepository, IAusUndVerleihFactory factory)
        {
            _ausleihRepository = ausleihRepository;
            _kundenRepository = kundenRepository;
            _factory = factory;
        }

        public void GeldAusleihen(Guid kundenNummer, VerleihKondition verleihKondition, decimal betrag)
        {
            KundenAufVerfuegbarkeitPruefen(kundenNummer);

            AusleihVorgang ausleihVorgang = _factory.CreateAusleihVorgangObject(kundenNummer, verleihKondition, betrag);

            _ausleihRepository.GeldAnKundenAusleihen(ausleihVorgang);
        }

        public void GeldEinzahlen(Guid vorgangsNummer, decimal betrag)
        {
            _ausleihRepository.KundeZahltGeldEin(vorgangsNummer, betrag);
        }

        private void KundenAufVerfuegbarkeitPruefen(Guid kundenNummer)
        {
            IList<Kunde> alleKunden = _kundenRepository.GetAlleKunden();

            if (!IstKundeInDerDatenbank(alleKunden, kundenNummer))
                throw new ApplicationException(
                    string.Format("Der kunde {0} existiert nicht in der Datenbank! Bitte vorher anlegen",
                                  kundenNummer));
        }

        private bool IstKundeInDerDatenbank(IList<Kunde> alleKunden, Guid kundenNummer)
        {
            return alleKunden.Any(x => x.Kundennummer == kundenNummer);
        }

        
    }
}