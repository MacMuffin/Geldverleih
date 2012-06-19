using System;
using Geldverleih.Domain;

namespace Geldverleih.Service.interfaces
{
    public interface IBankService
    {
        void GeldAusleihen(Guid kundenNummer, VerleihKondition verleihKondition, decimal betrag);
        void GeldEinzahlen(Guid vorgangsNummer, decimal betrag);
    }
}
