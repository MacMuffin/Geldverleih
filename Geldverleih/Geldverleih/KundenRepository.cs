using System.Collections.Generic;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;

namespace Geldverleih.Repository
{
    public class KundenRepository : RepositoryBase<Kunde>, IKundenRepository
    {

        public void KundenAnlegen(Kunde kunde)
        {
            Save(kunde);
        }

        public IList<Kunde> GetAlleKunden()
        {
            return GetAll();
        }
    }
}