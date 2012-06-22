using System;
using System.Collections.Generic;
using Geldverleih.Domain;
using Geldverleih.Service.interfaces;

namespace Geldverleih.UI.presenters
{
    public class KundenUebersichtPresenter
    {
        private readonly IKundenService _kundenService;

        public KundenUebersichtPresenter(IKundenService kundenService)
        {
            _kundenService = kundenService;
            
        }

        public IList<Kunde> AlleKundenAuslesen()
        {
            return _kundenService.AlleKundenAuslesen();
        }
    }
}