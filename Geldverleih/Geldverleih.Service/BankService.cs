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
        private readonly IRueckzahlReppository _rueckzahlReppository;
        private readonly IKundenRepository _kundenRepository;
        private readonly IAusUndRueckzahlvorgangFactory _factory;

        public BankService(IAusleihRepository ausleihRepository, IRueckzahlReppository rueckzahlReppository, IKundenRepository kundenRepository, IAusUndRueckzahlvorgangFactory factory)
        {
            _ausleihRepository = ausleihRepository;
            _rueckzahlReppository = rueckzahlReppository;
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
            RueckzahlVorgang rueckzahlVorgang = _factory.CreateRueckzahlVorgangObject(vorgangsNummer, betrag);
            _rueckzahlReppository.KundeZahltGeldEin(rueckzahlVorgang);
        }

        public IList<AusleihVorgang> GetAlleAusleihvorgaenge()
        {
            return _ausleihRepository.GetAlleAusleihVorgaenge();
        }

        public IList<AusleihVorgang> GetAlleAusleihvorgaengeByKundenNummer(Guid kundenNummer)
        {
            return _ausleihRepository.GetAlleAusleihVorgaengeByKundenNummer(kundenNummer);
        }

        public AusleihVorgang GetAusleihvorgangByVorgangsnummer(Guid vorgangsnummer)
        {
            return _ausleihRepository.GetAusleihvorgangByVorgangsnummer(vorgangsnummer);
        }

        public IList<RueckzahlVorgang> GetAlleRueckzahlvorgaengeByVorgangsNummer(Guid vorgangsNummer)
        {
            return _rueckzahlReppository.GetAlleRueckzahlvorgaengeByVorgangsNummer(vorgangsNummer);
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