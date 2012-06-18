using Geldverleih.Domain;

namespace Geldverleih.Service.interfaces
{
    public interface IBankService
    {
        void GeldAusleihen(Kunde kunde, VerleihKondition verleihKondition);
        void GeldEinzahlen(Kunde kunde, decimal betrag);
    }
}
