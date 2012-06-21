using System;
using System.Collections.Generic;
using Geldverleih.Domain;

namespace Geldverleih.Repository.interfaces
{
    public interface IAusleihRepository
    {
        void GeldAnKundenAusleihen(AusleihVorgang ausleihVorgang);
        IList<AusleihVorgang> GetAlleAusleihVorgaenge();
        IList<AusleihVorgang> GetAlleAusleihVorgaengeByKundenNummer(Guid kundenNummer);
        AusleihVorgang GetAusleihvorgangByVorgangsnummer(Guid vorgangsnummer);
    }
}