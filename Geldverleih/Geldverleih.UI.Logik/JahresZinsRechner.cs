using System;

namespace Geldverleih.UI.Logik
{
    public class JahresZinsRechner : IZinsRechner
    {
        public decimal BetragMitZinsenFuerZeitraumBerechnen(decimal betrag, decimal zinsSatz, Guid vorgangsNummer, ZeitSpanne zeitSpanne)
        {
            throw new NotImplementedException();
        }

        public decimal GetAusgezahltesGeldFuerZeitraum(ZeitSpanne zeitSpanne)
        {
            throw new NotImplementedException();
        }

        public decimal GetEingenommeneZinsenImZeitraum(ZeitSpanne zeitSpanne)
        {
            throw new NotImplementedException();
        }

        public decimal GetRueckgezahlteBetraegeFuerZeitraum(ZeitSpanne zeitSpanne)
        {
            throw new NotImplementedException();
        }
    }
}