using System;
using Geldverleih.Domain;

namespace Geldverleih.Repository.interfaces
{
    public interface IAusleihRepository
    {
        void GeldAnKundenAusleihen(Guid kundenNummer, VerleihKondition verleihKondition);
        void KundeZahltGeldEin(Guid vorgangsNummer, decimal betrag);
    }
}