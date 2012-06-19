using System;
using System.Collections.Generic;
using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;
using Geldverleih.Service.interfaces;

namespace Geldverleih.Service
{
    public class KundenService : IKundenService
    {
        private readonly IKundenRepository _kundenRepository;

        public KundenService(IKundenRepository kundenRepository)
        {
            _kundenRepository = kundenRepository;
        }

        public void KundenAnlegen(Kunde kunde)
        {
            _kundenRepository.KundenAnlegen(kunde);
        }

        public IList<Kunde> AlleKundenAuslesen()
        {
            return _kundenRepository.GetAlleKunden();
        }
    }
}