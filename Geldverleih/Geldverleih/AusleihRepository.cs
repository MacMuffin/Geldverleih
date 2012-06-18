using System;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;

namespace Geldverleih.Repository
{
    public class AusleihRepository : IAusleihRepository
    {
        public void GeldAnKundenAusleihen(Kunde kunde, VerleihKondition verleihKondition)
        {
            throw new NotImplementedException();
        }

        public void KundeZahltGeldEin(Kunde kunde, decimal betrag)
        {
            throw new NotImplementedException();
        }
    }
}