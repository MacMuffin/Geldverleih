using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geldverleih.Domain
{
    public class Kunde
    {
        public Guid Kundennummer { get; private set; }
        public string Name { get; private set; }
        public string Vorname { get; private set; }
        public string Adresse { get; private set; }
        public int PLZ { get; private set; }
        public string Wohnort { get; private set; }
        public float Kontostand { get; private set; }

        public Kunde(Guid kundennummer, string name, string vorname, string adresse, int plz, string wohnort, float kontostand)
        {
            Kundennummer = kundennummer;
            Name = name;
            Vorname = vorname;
            Adresse = adresse;
            PLZ = plz;
            Wohnort = wohnort;
            Kontostand = kontostand;
        }
    }
}
