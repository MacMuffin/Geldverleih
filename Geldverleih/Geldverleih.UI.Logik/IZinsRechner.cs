using System;

namespace Geldverleih.UI.Logik
{
    public interface IZinsRechner
    {
        decimal BetragMitZinsenFuerZeitraumBerechnen(decimal betrag, decimal zinsSatz, Guid vorgangsNummer, ZeitSpanne zeitSpanne);

        decimal GetEingenommeneZinsenImZeitraum(decimal betrag, decimal zinsSatz, Guid vorgangsNummer,
                                                ZeitSpanne zeitSpanne);
    }
}