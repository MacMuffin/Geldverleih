using Geldverleih.UI.models;
using Geldverleih.UI.presenters;

namespace Geldverleih.UI.views
{
    public interface IKundeDetailView
    {
        KundenDetailModel KundeDetailAnsicht { get; set; }
        void KundeBearbeitenModus();
        void KundeNochNichtErstelltModus();
        void ModalAnsichtLaden();
        void Initialisieren(KundeDetailPresenter kundeDetailPresenter);
        void AusleihUebersichtAktualisieren();
    }
}