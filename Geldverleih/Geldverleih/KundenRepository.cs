using System;
using System.Collections.Generic;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;

namespace Geldverleih.Repository
{
    public class KundenRepository : IKundenRepository
    {
        public void KundenAnlegen(Kunde kunde)
        {
            throw new NotImplementedException();
        }

        public IList<Kunde> GetAlleKunden()
        {
            throw new NotImplementedException();
        }
    }
}