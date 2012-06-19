namespace Geldverleih.UI.presenters
{
    public static class EinzahlResult
    {
        public static bool EinzahlvorgangErfolgreich { get; set; }
        public static EinzahlError Error { get; set; }
        public static decimal Restbetrag { get; set; }
    }
}