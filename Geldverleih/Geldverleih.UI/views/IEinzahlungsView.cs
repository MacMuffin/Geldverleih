using System;
using Geldverleih.UI.presenters;

namespace Geldverleih.UI.views
{
    public interface IEinzahlungsView
    {
        void EinzahlungAbgeschlossenResult(string result);
        void Aktualisieren();
        void Initialisieren(BankPresenter bankPresenter, Guid vorgangsNummer);
    }
}