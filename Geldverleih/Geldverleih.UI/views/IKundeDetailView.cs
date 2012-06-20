using Geldverleih.UI.models;

namespace Geldverleih.UI.views
{
    public interface IKundeDetailView
    {
        KundenDetailModel KundeDetailAnsicht { get; set; }
        void AnsichtAktualisieren();
    }
}