using System;

namespace Geldverleih.UI.Logik
{
    public interface IZinsRechner
    {
        decimal BetragMitZinsenFuerZeitraumBerechnen(decimal betrag, decimal zinsSatz, Guid vorgangsNummer, ZeitSpanne zeitSpanne);
        decimal GetAusgezahltesGeldFuerZeitraum(ZeitSpanne zeitSpanne);
        decimal GetEingenommeneZinsenImZeitraum(ZeitSpanne zeitSpanne);
        decimal GetRueckgezahlteBetraegeFuerZeitraum(ZeitSpanne zeitSpanne);
    }
}