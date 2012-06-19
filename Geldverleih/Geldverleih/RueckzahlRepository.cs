using Geldverleih.Domain;
using Geldverleih.Repository.interfaces;

namespace Geldverleih.Repository
{
    public class RueckzahlRepository : RepositoryBase<RueckzahlVorgang>, IRueckzahlReppository
    {
        public void KundeZahltGeldEin(RueckzahlVorgang rueckzahlVorgang)
        {
            Save(rueckzahlVorgang);
        }
    }
}