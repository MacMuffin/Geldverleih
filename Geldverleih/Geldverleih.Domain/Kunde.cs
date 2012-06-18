using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geldverleih.Domain
{
    public class Kunde
    {
        public Guid Kundennummer { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Adresse { get; set; }
        public int PLZ { get; set; }
        public string Wohnort { get; set; }
        public float Kontostand { get; set; }

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
