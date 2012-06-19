using System;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;

namespace Geldverleih.Repository
{
    public class AusleihRepository : RepositoryBase<AusleihVorgang>, IAusleihRepository
    {
        public void GeldAnKundenAusleihen(AusleihVorgang ausleihVorgang)
        {
            Save(ausleihVorgang);
        }

        public void KundeZahltGeldEin(Guid vorgangsNummer, decimal betrag)
        {
            throw new NotImplementedException();
        }
    }
}