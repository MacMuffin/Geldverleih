using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geldverleih.Domain;

namespace Geldverleih.Service
{
    public interface IBankService
    {
        void GeldAusleihen(Kunde kunde, VerleihKondition verleihKondition);
        void GeldEinzahlen(Kunde kunde, decimal betrag);
    }
}
