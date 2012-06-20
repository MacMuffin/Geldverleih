using System;
using System.Collections.Generic;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;
using NHibernate.Criterion;

namespace Geldverleih.Repository
{
    public class RueckzahlRepository : RepositoryBase<RueckzahlVorgang>, IRueckzahlReppository
    {
        public void KundeZahltGeldEin(RueckzahlVorgang rueckzahlVorgang)
        {
            Save(rueckzahlVorgang);
        }

        public IList<RueckzahlVorgang> GetAlleRueckzahlvorgaengeByVorgangsNummer(Guid vorgangsNummer)
        {
            return GetAllByIdAndProperty(vorgangsNummer, "AusleihvorgangNummer");
        }
    }
}