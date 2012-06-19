using System;
using System.Collections.Generic;
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

        public IList<AusleihVorgang> GetAlleAusleihVorgaenge()
        {
            return GetAll();
        }
    }
}