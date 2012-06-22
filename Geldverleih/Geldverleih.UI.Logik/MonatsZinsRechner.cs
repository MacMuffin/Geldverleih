using System;

namespace Geldverleih.UI.Logik
{
    public class MonatsZinsRechner : IZinsRechner
    {
        public decimal BetragMitZinsenFuerZeitraumBerechnen(decimal betrag, decimal zinsSatz, Guid vorgangsNummer, ZeitSpanne zeitSpanne)
        {
            throw new NotImplementedException();
        }

        public decimal GetEingenommeneZinsenImZeitraum(decimal betrag, decimal zinsSatz, Guid vorgangsNummer, ZeitSpanne zeitSpanne)
        {
            throw new NotImplementedException();
        }
    }
}