using Geldverleih.Domain;

namespace Geldverleih.Repository
{
    public interface IAusleihRepository
    {
        void GeldAnKundenAusleihen(Kunde kunde, VerleihKondition verleihKondition);
    }
}