using System;
using Geldverleih.Domain;

namespace Geldverleih.Service
{
    public class AusUndRueckzahlvorgangFactory : IAusUndRueckzahlvorgangFactory
    {
        public AusleihVorgang CreateAusleihVorgangObject(Guid kundenNummer, VerleihKondition verleihKondition, decimal betrag)
        {
            return new AusleihVorgang
                       {
                           Betrag = betrag,
                           Datum = DateTime.Now,
                           KundenNummer = kundenNummer,
                           ZinsSatz = verleihKondition.Zinssatz
                       };
        }

        public RueckzahlVorgang CreateRueckzahlVorgangObject(Guid vorgangsNummer, decimal betrag)
        {
            return new RueckzahlVorgang
                       {
                           AusleihvorgangNummer = vorgangsNummer,
                           Betrag = betrag,
                           Datum = DateTime.Now,
                           RueckzahlvorgangNummer = Guid.NewGuid()
                       };
        }
    }
}