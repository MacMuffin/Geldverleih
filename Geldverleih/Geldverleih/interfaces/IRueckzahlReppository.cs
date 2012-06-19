using Geldverleih.Domain;

namespace Geldverleih.Repository.interfaces
{
    public interface IRueckzahlReppository
    {
        void KundeZahltGeldEin(RueckzahlVorgang rueckzahlVorgang);
    }
}