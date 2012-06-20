using Geldverleih.UI.models;

namespace Geldverleih.UI.views
{
    public interface IKundenUebersichtView
    {
        KundenModel KundenModel { get; set; }
        void AnsichtAktualisieren();
    }
}