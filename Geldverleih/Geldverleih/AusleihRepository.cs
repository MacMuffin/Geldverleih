using System;
using System.Collections.Generic;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;
using NHibernate.Criterion;

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

        public IList<AusleihVorgang> GetAlleAusleihVorgaengeByKundenNummer(Guid kundenNummer)
        {
            using (var session = GetSession())
            {
                IList<AusleihVorgang> liste = session.CreateCriteria(typeof(AusleihVorgang))
                    .Add(Restrictions.Eq("KundenNummer", kundenNummer))
                    .List<AusleihVorgang>();
                return liste;
            }
        }
    }
}