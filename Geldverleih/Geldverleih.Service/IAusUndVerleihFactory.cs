using System;
using Geldverleih.Domain;

namespace Geldverleih.Service
{
    public interface IAusUndVerleihFactory
    {
        AusleihVorgang CreateAusleihVorgangObject(Guid kundenNummer, VerleihKondition verleihKondition, decimal betrag);
    }
}