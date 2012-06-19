using System;
using Geldverleih.Domain;

namespace Geldverleih.Service
{
    public class AusUndVerleihFactory : IAusUndVerleihFactory
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
    }
}