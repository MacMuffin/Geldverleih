using System;
using System.Collections.Generic;
using Geldverleih.Domain;

namespace Geldverleih.Repository.interfaces
{
    public interface IRueckzahlReppository
    {
        void KundeZahltGeldEin(RueckzahlVorgang rueckzahlVorgang);
        IList<RueckzahlVorgang> GetAlleRueckzahlvorgaengeByVorgangsNummer(Guid vorgangsNummer);
    }
}