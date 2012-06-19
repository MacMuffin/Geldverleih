using System;

namespace Geldverleih.Domain
{
    public class RueckzahlVorgang
    {
        public virtual Guid RueckzahlvorgangNummer { get; set; }
        public virtual Guid AusleihvorgangNummer { get; set; }
        public virtual decimal Betrag { get; set; }
        public virtual DateTime Datum { get; set; }
    }
}