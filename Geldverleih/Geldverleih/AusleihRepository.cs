using System;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;

namespace Geldverleih.Repository
{
    public class AusleihRepository : IAusleihRepository
    {
        public void GeldAnKundenAusleihen(Guid kundenNummer, VerleihKondition verleihKondition)
        {
            throw new NotImplementedException();
        }

        public void KundeZahltGeldEin(Guid vorgangsNummer, decimal betrag)
        {
            throw new NotImplementedException();
        }
    }
}