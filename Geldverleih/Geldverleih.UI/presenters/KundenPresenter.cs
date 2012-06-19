using System.Collections.Generic;
using Geldverleih.Domain;
using Geldverleih.Service.interfaces;

namespace Geldverleih.UI.presenters
{
    public class KundenPresenter
    {
        private readonly IKundenService _kundenService;

        public KundenPresenter(IKundenService kundenService)
        {
            _kundenService = kundenService;
        }

        public void KundenAnlegen(string name, string vorname, string wohnort, string adresse, int plz)
        {
            Kunde kunde = new Kunde
                              {
                                  Name = name,
                                  Vorname = vorname,
                                  Wohnort = wohnort,
                                  Adresse = adresse,
                                  PLZ = plz
                              }; 

            _kundenService.KundenAnlegen(kunde);
        }

        public IList<Kunde> AlleKundenAuslesen()
        {
            return _kundenService.AlleKundenAuslesen();
        }
    }
}