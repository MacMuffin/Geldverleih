using System;
using System.Collections.Generic;
using Geldverleih.Repository.interfaces;
using NHibernate;

namespace Geldverleih.Repository
{
    public class RepositoryBase<T> : IRepository<T>
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public T GetById(Guid id)
        {
            using (var session = GetSession())
            {
                return session.Get<T>(id);
            }
        }

        public IList<T> GetAllById(Guid id)
        {
            using (var session = GetSession())
            {
                IList<T> liste = session.CreateCriteria(typeof(T))
                    .List<T>();
                return liste; //TODO
            }
        }

        public void Save(T objekt)
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

        public void Delete(T objekt)
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

        public IList<T> GetAll()
        {
            using (var session = GetSession())
            {
                IList<T> liste = session.CreateCriteria(typeof(T))
                    .List<T>();
                return liste;
            }
        }
    }
}