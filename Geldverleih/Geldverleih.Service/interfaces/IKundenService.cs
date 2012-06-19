using System.Collections.Generic;
using Geldverleih.Domain;

namespace Geldverleih.Service.interfaces
{
    public interface IKundenService
    {
        void KundenAnlegen(Kunde kunde);
        IList<Kunde> AlleKundenAuslesen();
    }
}