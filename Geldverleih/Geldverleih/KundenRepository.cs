using System;
using System.Collections.Generic;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;
using NHibernate;

namespace Geldverleih.Repository
{
    public class KundenRepository : IKundenRepository, IRepository<Kunde>
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public void KundenAnlegen(Kunde kunde)
        {
            Save(kunde);
        }

        public IList<Kunde> GetAlleKunden()
        {
            return GetAll();
        }

        public Kunde GetById(Guid id)
        {
            using (var session = GetSession())
            {
                return session.Get<Kunde>(id);
            }
        }

        public void Save(Kunde objekt)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(objekt);
                    
                    transaction.Commit();
                }
            }
        }

        public void Delete(Kunde objekt)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(objekt);
                    transaction.Commit();
                }
            }
        }

        public IList<Kunde> GetAll()
        {
            using (var session = GetSession())
            {
                IList<Kunde> kunden = session.CreateCriteria(typeof (Kunde))
                    .List<Kunde>();
                return kunden;
            }
        }
    }
}