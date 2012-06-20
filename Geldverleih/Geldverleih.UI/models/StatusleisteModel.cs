using System.Collections.Generic;

namespace Geldverleih.UI.models
{
    public class StatusleisteModel
    {
        public string Bankname { get; set; }
        public IList<string> TickerNachrichten { get; set; }
        public string DatenbankBezeichnung { get; set; }
        public string AktuellerBenutzer { get; set; }
    }
}