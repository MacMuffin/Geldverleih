using System;

namespace Geldverleih.Domain
{
    public class AusleihVorgang
    {
        public virtual Guid KundenNummer { get; set; }
        public virtual DateTime Datum { get; set; }
        public virtual Guid VorgangsNummer { get; set; }
        public virtual decimal Betrag { get; set; }
        public virtual decimal ZinsSatz { get; set; }

        public AusleihVorgang()
        {
            VorgangsNummer = Guid.NewGuid();
        }
    }
}