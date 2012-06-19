using System;
using Geldverleih.Domain;

namespace Geldverleih.Service
{
    public interface IAusUndRueckzahlvorgangFactory
    {
        AusleihVorgang CreateAusleihVorgangObject(Guid kundenNummer, VerleihKondition verleihKondition, decimal betrag);
        RueckzahlVorgang CreateRueckzahlVorgangObject(Guid vorgangsNummer, decimal betrag);
    }
}