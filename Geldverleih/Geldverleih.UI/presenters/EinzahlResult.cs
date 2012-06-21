using System;

namespace Geldverleih.UI.presenters
{
    public class EinzahlResult
    {
        public bool EinzahlvorgangErfolgreich { get; set; }
        public EinzahlError Error { get; set; }
        public decimal Restbetrag { get; set; }

        public static EinzahlResult EinzahlungenErfolgreich(decimal restbetrag)
        {
            EinzahlResult einzahlResult = new EinzahlResult
                                              {
                                                  EinzahlvorgangErfolgreich = true,
                                                  Restbetrag = restbetrag
                                              };
            return einzahlResult;
        }

        public static EinzahlResult EinzahlvorgangFehlerhaft(EinzahlError error)
        {
            EinzahlResult einzahlResult = new EinzahlResult {Error = error, EinzahlvorgangErfolgreich = false};
            return einzahlResult;
        }
    }
}