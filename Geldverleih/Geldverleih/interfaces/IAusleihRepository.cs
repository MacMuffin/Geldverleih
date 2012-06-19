using System;
using Geldverleih.Domain;

namespace Geldverleih.Repository.interfaces
{
    public interface IAusleihRepository
    {
        void GeldAnKundenAusleihen(Kunde kunde, VerleihKondition verleihKondition);
        void KundeZahltGeldEin(Kunde kunde, Guid vorgangsNummer, decimal betrag);
    }
}