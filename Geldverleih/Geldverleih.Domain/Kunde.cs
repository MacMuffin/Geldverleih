using System;

namespace Geldverleih.Domain
{
    public class Kunde
    {
        public virtual Guid Kundennummer { get; set; }
        public virtual string Name { get; set; }
        public virtual string Vorname { get; set; }
        public virtual string Adresse { get; set; }
        public virtual int PLZ { get; set; }
        public virtual string Wohnort { get; set; }
        public virtual float Kontostand { get; set; }

        public Kunde()
        {
            Kundennummer = Guid.NewGuid();
            Name = "";
            Vorname = "";
            Adresse = "";
            Wohnort = "";
            Kontostand = 0.0f;
        }
    }
}
