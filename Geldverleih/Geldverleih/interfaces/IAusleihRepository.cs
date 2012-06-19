using System;
using Geldverleih.Domain;

namespace Geldverleih.Repository.interfaces
{
    public interface IAusleihRepository
    {
        void GeldAnKundenAusleihen(AusleihVorgang ausleihVorgang);
        void KundeZahltGeldEin(Guid vorgangsNummer, decimal betrag);
    }
}