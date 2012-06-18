using Geldverleih.Domain;

namespace Geldverleih.Repository.interfaces
{
    public interface IAusleihRepository
    {
        void GeldAnKundenAusleihen(Kunde kunde, VerleihKondition verleihKondition);
    }
}