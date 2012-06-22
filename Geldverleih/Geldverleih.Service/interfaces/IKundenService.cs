using System.Collections.Generic;
using Geldverleih.Domain;

namespace Geldverleih.Service.interfaces
{
    public interface IKundenService
    {
        void KundenSpeichern(Kunde kunde);
        IList<Kunde> AlleKundenAuslesen();
    }
}